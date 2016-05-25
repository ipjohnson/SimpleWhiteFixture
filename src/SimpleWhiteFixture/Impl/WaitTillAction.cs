using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.WindowItems;

namespace SimpleWhiteFixture.Impl
{
    public interface IWaitTillAction
    {
        IWindowFixture WaitTill(Window.WaitTillDelegate till);
    }

    public class WaitTillAction : IWaitTillAction
    {
        private IWindowFixture _fixture;

        public WaitTillAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IWindowFixture WaitTill(Window.WaitTillDelegate till)
        {
            _fixture.Instance.WaitTill(till);

            return _fixture;
        }
    }
}
