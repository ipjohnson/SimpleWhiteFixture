using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IIntoAction
    {
        IWindowFixture Into(string id);

        IWindowFixture Into(SearchCriteria by);
    }

    public class IntoAction : IIntoAction
    {
        private IWindowFixture _fixture;
        private Action<IUIItem> _action;

        public IntoAction(IWindowFixture fixture, Action<IUIItem> action)
        {
            _action = action;
            _fixture = fixture;
        }

        public IWindowFixture Into(SearchCriteria by)
        {
            var items = _fixture.Instance.GetMultiple(by);

            foreach(var item in items)
            {
                _action(item);
            }

            return _fixture;
        }

        public IWindowFixture Into(string id)
        {
            return Into(SearchCriteria.ByAutomationId(id));
        }
    }
}
