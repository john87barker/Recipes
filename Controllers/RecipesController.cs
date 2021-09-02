using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recipes.Services;
using Recipes.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class RecipesController : ControllerBase
    {
    private readonly RecipesService _recipesService;

    public RecipesController(RecipesService recipesService)
    {
      _recipesService = recipesService;
    }

    [HttpGet]

    public ActionResult<List<Recipe>> Get()
    {
      try
      {
        List<Recipe> recipes = _recipesService.Get();
        return Ok(recipes);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]

    public ActionResult<Recipe> Get(int id)
    {
      try
      {
        Recipe recipe = _recipesService.Get(id);
        if(recipe == null)
        {
          throw new Exception("invalido id-o");
        }
        return Ok(recipe);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
  {
    try
    {
        Account foundAccount = await HttpContext.GetUserInfoAsync<Account>();
        newRecipe.CreatorId = foundAccount.Id;
        Recipe recipe = _recipesService.Create(newRecipe);
        return Ok(newRecipe);
      }
    catch (Exception err)
      {
        return BadRequest(err.Message);
      }
  }
    [HttpDelete("{id}")]
    // [Authorize]

    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
           Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _recipesService.Delete(id, userInfo.Id);
        return Ok("Gone for good");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


  }

  
}