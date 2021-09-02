using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Recipes.Models;

namespace Recipes.Repositories
{
    
  public class StepsRepository
  {
      private readonly IDbConnection _db;

    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Step> Get()
    {
      string sql = @"
      SELECT 
        s.*,
        a.*
      FROM steps s
      JOIN accounts a ON s.creatorId = a.id 
      ";
      return _db.Query<Step, Account, Step> (sql, (s, a)=> 
      {
        s.Creator = a;
        return s;
      }, splitOn: "id").ToList();
    }

    
    internal Step Get(int id)
    {
    string sql = @"
      SELECT 
        s.*,
        a.*
      FROM steps s 
      JOIN accounts a ON s.creatorId = a.id
      WHERE
        s.id = @id
      ";
      return _db.Query<Step, Account, Step> (sql,  (s, a)=> 
      {
        s.Creator = a;
        return s;
      }, new {id}, splitOn: "id").FirstOrDefault();
          }


    internal Step Create(Step newStep)
    {
     string sql = @"
      INSERT INTO
        step
      (body, creatorId)
      VALUES
      (
        @title, @creatorId
      );
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newStep);
      return Get(id);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}