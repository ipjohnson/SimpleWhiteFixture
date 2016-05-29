using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;

namespace SimpleWhiteFixture.Impl
{
    public interface IAutoFillAction
    {
        IWindowFixture AutoFill(string startingPoint = null, object seed = null);

        IWindowFixture AutoFill(SearchCriteria by, object seed = null);
    }
    public class AutoFillAction : IAutoFillAction
    {
        private IWindowFixture _fixture;

        public AutoFillAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }
                
        protected virtual void ProcessUIItems(IEnumerable<IUIItem> items, object seed)
        {
            foreach(var item in items)
            {
                if(item is TextBox)
                {
                    string value = _fixture.Data.Generate<string>(item.Id, seed);

                    item.SetValue(value);
                }
                else if(item is CheckBox)
                {
                    CheckBox checkBox = item as CheckBox;

                    checkBox.Checked = _fixture.Data.Generate<bool>(checkBox.Id, seed);
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
                
        public IWindowFixture AutoFill(string startingPoint = null, object seed = null)
        {
            if(!string.IsNullOrEmpty(startingPoint))
            {
                var item = _fixture.Instance.Get(SearchCriteria.ByAutomationId(startingPoint));

                var container = item as IUIItemContainer;

                if(container != null)
                {
                    var items = container.GetMultiple(SearchCriteria.All);

                    ProcessUIItems(items, seed);                    
                }
                else
                {
                    ProcessUIItems(new[] { item }, seed);
                }
            }
            else
            {
                ProcessUIItems(_fixture.Instance.Items, seed);
            }

            return _fixture;
        }

        public IWindowFixture AutoFill(SearchCriteria by, object seed = null)
        {
            var searchItems = _fixture.Instance.GetMultiple(by);
            List<IUIItem> items = new List<IUIItem>();

            foreach(var item in searchItems)
            {
                IUIItemContainer container = item as IUIItemContainer;

                if(container != null)
                {
                    items.AddRange(container.GetMultiple(SearchCriteria.All));
                }
                else
                {
                    items.Add(item);
                }
            }

            ProcessUIItems(items, seed);

            return _fixture;
        }        

        private Dictionary<string,object> GetSeedValues(object seed)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            if(seed != null)
            {
                foreach(var property in seed.GetType().GetProperties())
                {
                    if(property.CanRead && 
                       property.GetMethod.IsPublic && 
                      !property.GetMethod.IsStatic && 
                      !property.GetMethod.GetParameters().Any())
                    {
                        values[property.Name] = property.GetValue(seed);
                    }                    
                }
            }

            return values;
        }
    }
}
