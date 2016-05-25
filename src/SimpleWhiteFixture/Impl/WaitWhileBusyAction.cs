using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWhiteFixture.Impl
{
    public interface IWaitWhileBusyAction
    {
        IWindowFixture WaitWhileBusy();
    }

    public class WaitWhileBusyAction : IWaitWhileBusyAction
    {
        private IWindowFixture _fixture;

        public WaitWhileBusyAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IWindowFixture WaitWhileBusy()
        {
            _fixture.Instance.WaitWhileBusy();

            return _fixture;
        }
    }
}
