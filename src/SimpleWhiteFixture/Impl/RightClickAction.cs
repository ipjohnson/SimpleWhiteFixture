using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IRightClickAction
    {
        IWindowFixture RightClick(string id);

        IWindowFixture RightClick(SearchCriteria by, ClickMode clickMode);
    }

    public class RightClickAction : IRightClickAction
    {
        private IWindowFixture _fixture;

        public RightClickAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IWindowFixture RightClick(string id)
        {
            var element = _fixture.Instance.Get(SearchCriteria.ByAutomationId(id));

            element.DoubleClick();

            return _fixture;
        }

        public IWindowFixture RightClick(SearchCriteria by, ClickMode clickMode)
        {
            _fixture.ApplyClickAction(by, c => c.DoubleClick(), clickMode);

            return _fixture;
        }
    }
}
