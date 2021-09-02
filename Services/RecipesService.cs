using System;
using System.Collections.Generic;
using Recipes.Models;
using Recipes.Repositories;

namespace Recipes.Services
{
  public class RecipesService
  {

    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }

    internal  List<Recipe> Get()
    {
      List<Recipe> recipes = _repo.Get();
      return recipes;
    }

    internal Recipe Get(int id)
    {
      Recipe recipe = _repo.Get(id);
      return recipe;
    }

    internal Recipe Create(Recipe newRecipe)
    {
      Recipe madeRecipe = _repo.Create(newRecipe);
      return madeRecipe;
    }

    internal void Delete(int id, string userId)
    {
      Recipe recipeDelete = Get(id);
      if( recipeDelete.CreatorId != userId)
      {
        throw new Exception("YOu've been caught getting in the cookie jar!");
      }
      _repo.Delete(id);
    }
  }
}