using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.WindowsAPI;

namespace SimpleWhiteFixture.Impl
{
    public interface IKeyInAction
    {
        IIntoAction Key(KeyboardInput.SpecialKeys key);
    }

    public class KeyInAction : IKeyInAction
    {
        private IWindowFixture _fixture;

        public KeyInAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IIntoAction Key(KeyboardInput.SpecialKeys key)
        {
            var action = new Action<IUIItem>(i => i.KeyIn(key));

            return _fixture.Data.Locate<IIntoAction>(constraints: new { _Values = new object[] { _fixture, action } });
        }

    }
}
