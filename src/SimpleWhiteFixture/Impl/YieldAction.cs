using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWhiteFixture.Impl
{
    public interface IYieldAction
    {
        T Yields<T>();
    }

    public class YieldAction : IYieldAction
    {
        private IWindowFixture _fixture;

        public YieldAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public T Yields<T>()
        {
            return _fixture.Data.Locate<T>(constraints: new { _Values = new[] { _fixture } });
        }
    }
}
