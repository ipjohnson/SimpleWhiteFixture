using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    public interface IGetAction
    {
        T Element<T>(string id) where T : IUIItem;

        T Element<T>(SearchCriteria by) where T : IUIItem;

        IEnumerable<IUIItem> Elements(SearchCriteria by);

        IFromAction<T> ValuesAs<T>();

        IFromAction<string> Text { get; }
    }

    public class GetAction : IGetAction
    {
        private IWindowFixture _fixture;

        public GetAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public T Element<T>(SearchCriteria by) where T : IUIItem
        {
            return _fixture.Instance.Get<T>(by);
        }

        public T Element<T>(string id) where T : IUIItem
        {
            return _fixture.Instance.Get<T>(id);
        }

        public IEnumerable<IUIItem> Elements(SearchCriteria by)
        {
            return _fixture.Instance.GetMultiple(by).ToList();
        }

        public IFromAction<string> Text
        {
            get
            {
                return _fixture.Data.Locate<IFromAction<string>>(constraints:
                    new { _Values = new object[] { _fixture, (Func<IUIItem,string>)GetTextAction } });
            }
        }

        private string GetTextAction(IUIItem arg)
        {
            return (GetValueFromItem(arg) ?? "").ToString();
        }

        public IFromAction<T> ValuesAs<T>()
        {
            throw new NotImplementedException();
        }

        protected virtual object GetValueFromItem(IUIItem item)
        {
            var textBox = item as TextBox;

            if(textBox != null)
            {
                return textBox.Text;
            }
            
            var label = item as Label;

            if (label != null)
            {
                return label.Text;
            }

            return null;
        }
    }
}
