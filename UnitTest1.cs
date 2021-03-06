using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace EFViewAndManyToMany {

  public static class Helper {
    public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class {
      var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
      var relationalCommandCache = enumerator.Private("_relationalCommandCache");
      var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
      var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

      var sqlGenerator = factory.Create();
      var command = sqlGenerator.GetCommand(selectExpression);

      string sql = command.CommandText;
      return sql;
    }

    private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
    private static T Private<T>(this object obj, string privateField) => (T) obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
  }

  /*
   * 
   * Before running tests, first run InitializeDatabase.sql
   * 
   */

  public class UnitTest1 {

    [Fact]
    public void Test1() {
      var context = new EFViewAndManyToManyDb();
      Console.WriteLine(
        context.PostView
          .Include(x => x.Author)
        .Single().Author.Name
      );
    }

    [Fact]
    public void Test2() {
      var context = new EFViewAndManyToManyDb();
      var query = context.PostView.Include(x => x.Post_Tag);

      Console.WriteLine(query.ToSql());
      Console.WriteLine(query.Single());
    }

    [Fact]
    public void Test3() {
      var context = new EFViewAndManyToManyDb();
      var query = context.PostView.Include(x => x.Author_Tag);
      
      Console.WriteLine(query.ToSql());
      Console.WriteLine(query.Single());
    }

  }
}
