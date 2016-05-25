using SimpleWhiteFixture.xUnit.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace SimpleWhiteFixture.xUnit
{
    [XunitTestCaseDiscoverer(UITheoryDiscoverer.ClassName, UITheoryDiscoverer.AssemblyName)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UITheoryAttribute : FactAttribute
    {
    }
}
