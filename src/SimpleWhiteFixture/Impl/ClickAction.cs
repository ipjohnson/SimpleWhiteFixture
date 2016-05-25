using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IClickAction
    {
        IWindowFixture Click(string id);

        IWindowFixture Click(SearchCriteria by, ClickMode clickMode = ClickMode.ClickFirst);
    }

    public class ClickAction : IClickAction
    {
        protected readonly IWindowFixture _window;

        public ClickAction(IWindowFixture window)
        {
            _window = window;            
        }

        public IWindowFixture Click(SearchCriteria by, ClickMode clickMode = ClickMode.ClickFirst)
        {
            _window.ApplyClickAction(by, c => c.Click(), clickMode);

            return _window;
        }

        public IWindowFixture Click(string id)
        {
            var element = _window.Instance.Get(SearchCriteria.ByAutomationId(id));

            element.Click();

            return _window;
        }
    }
}
