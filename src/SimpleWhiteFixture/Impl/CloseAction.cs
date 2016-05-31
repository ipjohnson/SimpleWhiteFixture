using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWhiteFixture.Impl
{
    public interface ICloseAction
    {
        void Close();
    }

    public class CloseAction : ICloseAction
    {
        private IWindowFixture _fixture;

        public CloseAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public void Close()
        {
            _fixture.Instance.Close();
        }
    }
}
