using System;
using System.Linq;
using Xunit;

namespace EFViewAndManyToMany {
  public class UnitTest1 {
    [Fact]
    public void Test1() {
      var context = new EFViewAndManyToManyDb();
      context.PostView.ToList();
    }
  }
}
