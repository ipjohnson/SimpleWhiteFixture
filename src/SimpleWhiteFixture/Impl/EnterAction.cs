using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;

namespace SimpleWhiteFixture.Impl
{
    public interface IEnterAction
    {
        IIntoAction Enter(string value);
    }

    public class EnterAction : IEnterAction
    {
        private IWindowFixture _fixture;

        public EnterAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IIntoAction Enter(string value)
        {
            var action = new Action<IUIItem>(i => i.Enter(value));

            return _fixture.Data.Locate<IIntoAction>(constraints: new { _Values = new object[] { _fixture, action } });
        }
    }
}
