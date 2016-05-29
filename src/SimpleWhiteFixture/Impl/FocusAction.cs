using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IFocusAction
    {
        IWindowFixture Focus(string id);

        IWindowFixture Focus(SearchCriteria by);
    }

    public class FocusAction : IFocusAction
    {
        private IWindowFixture _fixture;

        public FocusAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IWindowFixture Focus(SearchCriteria by)
        {
            var item = _fixture.Instance.Get(by);

            item.Focus();

            return _fixture;
        }

        public IWindowFixture Focus(string id)
        {
            return Focus(SearchCriteria.ByAutomationId(id));
        }
    }
}
