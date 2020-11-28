using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public class TestElement
    {
        public string TestCode;
        public string TestName;

        public TestElement(string name, string code)
        {
            TestCode = code;
            TestName = name;
        }
    }
}
