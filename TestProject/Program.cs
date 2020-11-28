using System;
using System.Threading.Tasks;

namespace TestProject
{
    class Program
    {
        public static async Task Main()
        {
            await new Pipeline().Generate(2, @"../../../../GeneratedTests", @"../../../TestClass.cs", @"../../../AnotherTestClass.cs");
        }
    }
}
