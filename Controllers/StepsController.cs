using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Models;
using Recipes.Services;

namespace Recipes.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StepsController : ControllerBase
    {
    private readonly StepsService _stepsService;

    public StepsController(StepsService stepsService)
    {
      _stepsService = stepsService;
    }

    [HttpGet]

    public ActionResult<List<Step>> Get()
    {
        try
        {
        List<Step> steps = _stepsService.Get();
        return Ok(steps);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
[HttpGet("{id}")]

    public ActionResult<Step> Get(int id)
    {
      try
      {
        Step step = _stepsService.Get(id);
        if(step == null)
        {
          throw new Exception("invalido id-o");
        }
        return Ok(step);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
  [Authorize]
  public async Task<ActionResult<Step>> Create([FromBody] Step newStep)
  {
    try
    {
        Account foundAccount = await HttpContext.GetUserInfoAsync<Account>();
        newStep.CreatorId = foundAccount.Id;
        Step step = _stepsService.Create(newStep);
        return Ok(newStep);
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
        _stepsService.Delete(id, userInfo.Id);
        return Ok("Gone for good");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}