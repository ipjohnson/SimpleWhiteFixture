using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IFromAction<T>
    {
        T From(string id = null);

        T From(SearchCriteria by);
    }

    public class FromAction<T> : IFromAction<T>
    {
        private IWindowFixture _fixture;
        private Func<IEnumerable<IUIItem>, T> _func;

        public FromAction(IWindowFixture fixture, Func<IEnumerable< IUIItem>, T> func)
        {
            _fixture = fixture;
            _func = func;
        }

        public T From(SearchCriteria by)
        {
            var element = _fixture.Instance.GetMultiple(by);

            return _func(element);
        }

        public T From(string id)
        {
            if(id == null)
            {
                return _func(_fixture.Instance.Items);
            }

            var element = _fixture.Instance.GetMultiple(SearchCriteria.ByAutomationId(id));

            return _func(element);
        }
    }
}
