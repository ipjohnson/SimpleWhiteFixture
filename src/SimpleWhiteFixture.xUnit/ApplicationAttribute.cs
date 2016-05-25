using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;

namespace SimpleWhiteFixture.xUnit
{
    public interface IApplicationAttribute
    {
        string Application { get; }

        string Window { get; }

        Application ProvideApplication(MethodInfo method);
    }

    public class ApplicationAttribute : Attribute, IApplicationAttribute
    {
        public ApplicationAttribute(string application)
        {
            Application = application;
        }

        public string Application { get; private set; }

        public string Window { get; set; }

        public Application ProvideApplication(MethodInfo method)
        {
            return TestStack.White.Application.Launch(Application);
        }
    }
}
