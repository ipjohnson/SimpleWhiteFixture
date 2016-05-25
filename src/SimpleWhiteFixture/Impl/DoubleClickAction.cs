using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IDoubleClickAction
    {
        IWindowFixture DoubleClick(string id);

        IWindowFixture DoubleClick(SearchCriteria by, ClickMode clickMode);
    }

    public class DoubleClickAction : IDoubleClickAction
    {
        private IWindowFixture _fixture;

        public DoubleClickAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IWindowFixture DoubleClick(string id)
        {
            var element = _fixture.Instance.Get(SearchCriteria.ByAutomationId(id));

            element.DoubleClick();

            return _fixture;
        }

        public IWindowFixture DoubleClick(SearchCriteria by, ClickMode clickMode)
        {
            _fixture.ApplyClickAction(by, c => c.DoubleClick(), clickMode);

            return _fixture;
        }
    }
}
