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

    /// <summary>
    /// attribute that specifies an application to run 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class ApplicationAttribute : Attribute, IApplicationAttribute
    {
        public ApplicationAttribute(string application)
        {
            Application = application;
        }

        public string Application { get; private set; }

        public string Window { get; set; }

        public virtual Application ProvideApplication(MethodInfo method)
        {
            return TestStack.White.Application.Launch(Application);
        }
    }
}
