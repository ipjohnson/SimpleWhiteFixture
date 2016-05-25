using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public T From(SearchCriteria by)
        {
            throw new NotImplementedException();
        }

        public T From(string id)
        {
            throw new NotImplementedException();
        }
    }
}
