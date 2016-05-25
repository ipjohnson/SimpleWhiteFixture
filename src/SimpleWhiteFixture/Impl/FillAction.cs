using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IFillAction
    {
        IFillWithAction Fill(string id);

        IFillWithAction Fill(SearchCriteria by);
    }

    public interface IFillWithAction
    {
        IWindowFixture With(object fillValue);
    }
}
