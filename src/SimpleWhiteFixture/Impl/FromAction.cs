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
        T From(string id);

        T From(SearchCriteria by);
    }

    public class FromAction<T> : IFromAction<T>
    {
        private IWindowFixture _fixture;
        private Func<IUIItem, T> _func;

        public FromAction(IWindowFixture fixture, Func<IUIItem, T> func)
        {
            _fixture = fixture;
            _func = func;
        }

        public T From(SearchCriteria by)
        {
            throw new NotImplementedException();
        }

        public T From(string id)
        {
            var element = _fixture.Instance.Get(SearchCriteria.ByAutomationId(id));

            return _func(element);
        }
    }
}
