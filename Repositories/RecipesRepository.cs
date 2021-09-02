using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Recipes.Models;

namespace Recipes.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> Get()
    {
      string sql = @"
      SELECT 
        r.*,
        a.*
      FROM recipes r 
      JOIN accounts a ON r.creatorId = a.id
      ";
      return _db.Query<Recipe, Account, Recipe> (sql, (r, a)=> 
      {
        r.Creator = a;
        return r;
      }, splitOn: "id").ToList();
    }

    

    internal Recipe Get(int id)
    {
  string sql = @"
      SELECT 
        r.*,
        a.*
      FROM recipes r 
      JOIN accounts a ON r.creatorId = a.id
      WHERE
        r.id = @id
      ";
      return _db.Query<Recipe, Account, Recipe> (sql,  (r, a)=> 
      {
        r.Creator = a;
        return r;
      }, new {id}, splitOn: "id").FirstOrDefault();
          }

    

    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"
      INSERT INTO
        recipes
      (title, description, cookTime, prepTime, creatorId)
      VALUES
      (
        @title, @description, @cookTime, @prepTime, @creatorId
      );
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newRecipe);
      return Get(id);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}