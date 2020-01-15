using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EFViewAndManyToMany {
  public class UnitTest1 {
    [Fact]
    public void Test1() {
      var context = new EFViewAndManyToManyDb();
      Console.WriteLine(context.PostView.Include(x => x.Author).Single().Author.Name); ;
    }
  }
}
