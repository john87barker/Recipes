using System;
using System.Collections.Generic;
using Recipes.Models;
using Recipes.Repositories;

namespace Recipes.Services
{
  public class StepsService
  {
    private readonly StepsRepository _repo;

    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Step> Get()
    {
      List<Step> steps = _repo.Get();
      return steps;
    }
    
    internal Step Get(int id)
    {
      Step step = _repo.Get(id);
      return step;
    }

    internal Step Create(Step newStep)
    {
      Step madeStep = _repo.Create(newStep);
      return madeStep;
    }

    internal void Delete(int id, string userId)
    {
      Step stepDelete = Get(id);
      if( stepDelete.CreatorId != userId)
      {
        throw new Exception("YOu've been caught getting in the cookie jar!");
      }
      _repo.Delete(id);
    }
  }
}