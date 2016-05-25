using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;

namespace SimpleWhiteFixture.Impl
{
    public interface IAutoFillAction
    {
        IAutoFillSelectionAction AutoFill(object seed);
    }

    public interface IAutoFillSelectionAction
    {
        IWindowFixture All(string startingWith = null);

        IWindowFixture All(SearchCriteria criteria);

        IWindowFixture Matching(SearchCriteria criteria);
    }

    public class AutoFillAction : IAutoFillAction
    {
        private IWindowFixture _fixture;

        public AutoFillAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IAutoFillSelectionAction AutoFill(object seed)
        {
            return _fixture.Data.Locate<IAutoFillSelectionAction>(constraints: 
                new
                {
                    seed,
                    _Values = new object[] { _fixture, (Action<IEnumerable<IUIItem>>)ProcessUIItems }
                });
        }        

        protected virtual void ProcessUIItems(IEnumerable<IUIItem> items)
        {
            foreach(var item in GetInputControls(items))
            {
                if(item is TextBox)
                {
                    string value = _fixture.Data.Generate<string>(item.Id);

                    item.SetValue(value);
                }
                else if(item is CheckBox)
                {
                    CheckBox checkBox = item as CheckBox;

                    checkBox.Checked = _fixture.Data.Generate<bool>(checkBox.Id);
                }
                else if(item is ComboBox)
                {
                    ComboBox comboBox = item as ComboBox;

                    int selectItem = _fixture.Data.Generate<int>(comboBox.Id, new { min = 0, max = comboBox.Items.Count });

                    comboBox.Select(selectItem);
                }
                else if(item is ListBox)
                {
                    ListBox comboBox = item as ListBox;

                    int selectItem = _fixture.Data.Generate<int>(comboBox.Id, new { min = 0, max = comboBox.Items.Count });

                    comboBox.Select(selectItem);
                }
            }
        }

        protected virtual IEnumerable<IUIItem> GetInputControls(IEnumerable<IUIItem> items)
        {
            foreach (var item in items)
            {
                if (item is TextBox ||
                   item is CheckBox ||
                   item is ComboBox ||
                   item is ListBox)
                {
                    yield return item;
                }
            }
        }
    }

    public class AutoFillSelectionAction : IAutoFillSelectionAction
    {
        private IWindowFixture _fixture;
        private Action<IEnumerable<IUIItem>> _applyAction;
        private object seed;

        public AutoFillSelectionAction(object seed, IWindowFixture fixture, Action<IEnumerable<IUIItem>> applyAction)
        {
            _fixture = fixture;
            _applyAction = applyAction;
        }

        public IWindowFixture All(SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IWindowFixture All(string startingWith = null)
        {
            IEnumerable<IUIItem> items = null;

            if (startingWith != null)
            {
                var element = _fixture.Instance.Get(SearchCriteria.ByAutomationId(startingWith));

                IUIItemContainer container = element as IUIItemContainer;

                if (container != null)
                {
                    items = container.GetMultiple(SearchCriteria.All);
                }
                else
                {
                    items = new[] { element };
                }
            }
            else
            {
                items = _fixture.Instance.Items;
            }

            _applyAction(items);

            return _fixture;
        }

        public IWindowFixture Matching(SearchCriteria criteria)
        {
            var elements = _fixture.Instance.GetMultiple(criteria);

            _applyAction(elements);

            return _fixture;
        }

    }
}
