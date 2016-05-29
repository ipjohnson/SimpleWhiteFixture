using SimpleFixture.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowStripControls;

namespace SimpleWhiteFixture.Impl
{
    public interface IGetAction
    {
        T Item<T>(string id) where T : IUIItem;

        T Item<T>(SearchCriteria by) where T : IUIItem;

        IEnumerable<IUIItem> Items(SearchCriteria by);

        MenuBar MenuBar(string id);

        MenuBar MenuBar(SearchCriteria by);

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

        public T Item<T>(SearchCriteria by) where T : IUIItem
        {
            return _fixture.Instance.Get<T>(by);
        }

        public T Item<T>(string id) where T : IUIItem
        {
            return _fixture.Instance.Get<T>(id);
        }

        public IEnumerable<IUIItem> Items(SearchCriteria by)
        {
            return _fixture.Instance.GetMultiple(by).ToList();
        }

        public IFromAction<string> Text
        {
            get
            {
                return _fixture.Data.Locate<IFromAction<string>>(constraints:
                    new { _Values = new object[] { _fixture, (Func<IEnumerable<IUIItem>, string>)GetTextAction } });
            }
        }

        private string GetTextAction(IEnumerable<IUIItem> items)
        {
            StringBuilder response = new StringBuilder();

            foreach (var item in items)
            {
                object value = GetValueFromItem(item);

                if (value != null)
                {
                    if (response.Length > 0)
                    {
                        response.Append(", ");
                    }

                    response.Append(value.ToString());
                }
            }

            return response.ToString();
        }

        public IFromAction<T> ValuesAs<T>()
        {
            return new FromAction<T>(_fixture, GetTValueFromItems<T>);
        }

        public MenuBar MenuBar(string id)
        {
            return MenuBar(SearchCriteria.ByAutomationId(id));
        }

        public MenuBar MenuBar(SearchCriteria by)
        {
            return _fixture.Instance.GetMenuBar(by);
        }

        protected virtual object GetTValueFromItems(Type t, IEnumerable<IUIItem> items)
        {
            Type genericTypeDef = null;

            if (t.IsGenericType)
            {
                genericTypeDef = t.GetGenericTypeDefinition();
            }

            if (t.IsPrimitive)
            {
                double value = GetAggregatePrimitive(items);

                return Convert.ChangeType(value, t);
            }
            else if (t == typeof(string))
            {
                return GetTextAction(items);
            }
            else if (genericTypeDef == typeof(Dictionary<,>) ||
                    genericTypeDef == typeof(IDictionary<,>) ||
                    genericTypeDef == typeof(IReadOnlyDictionary<,>))
            {
                var genericParameters = t.GetGenericArguments();

                if (genericParameters[0] != typeof(string))
                {
                    throw new Exception("when converting to dictionary the key must be a string");
                }

                IDictionary dictionary = (IDictionary)Activator.CreateInstance(t);

                if (genericParameters[1].IsPrimitive ||
                   genericParameters[1] == typeof(string) ||
                   genericParameters[1] == typeof(DateTime))
                {
                    foreach (var item in items)
                    {
                        var value = GetValueFromItem(item);

                        if (value != null)
                        {
                            dictionary.Add(item.Id, Convert.ChangeType(value, genericParameters[1]));
                        }
                    }
                }
                else if (genericParameters[1].IsEnum)
                {
                    // TODO enum
                }
                else
                {
                    foreach (var item in items)
                    {
                        var container = item as IUIItemContainer;

                        if(container != null)
                        {
                            var childItems = container.GetMultiple(SearchCriteria.All);

                            dictionary.Add(item.Id, GetTValueFromItems(genericParameters[1], childItems));
                        }
                    }
                }

                return dictionary;
            }
            else if (genericTypeDef == typeof(List<>) ||
                    genericTypeDef == typeof(IReadOnlyList<>) ||
                    genericTypeDef == typeof(IEnumerable<>))
            {
                var genericParameter = t.GetGenericArguments().First();

                IList list = (IList)Activator.CreateInstance(t);

                if (genericParameter.IsPrimitive ||
                    genericParameter == typeof(string) ||
                    genericParameter == typeof(DateTime))
                {
                    foreach (var item in items)
                    {
                        var value = GetValueFromItem(item);

                        if (value != null)
                        {
                            list.Add(Convert.ChangeType(value, genericParameter));
                        }
                    }
                }
                else if (genericParameter.IsEnum)
                {
                    // TODO enum
                }
                else
                {
                    foreach (var item in items)
                    {
                        var container = item as IUIItemContainer;

                        if (container != null)
                        {
                            var childItems = container.GetMultiple(SearchCriteria.All);

                            list.Add(GetTValueFromItems(genericParameter, childItems));
                        }
                    }
                }

                return list;
            }
            else
            {
                var valueProvider = new ConstraintValueProvider(new List<IUIItem>(items), GetValueFromItem);

                return _fixture.Data.Generate(t, constraints: valueProvider);
            }
        }

        protected virtual T GetTValueFromItems<T>(IEnumerable<IUIItem> items)
        {
            return (T)GetTValueFromItems(typeof(T), items);
        }

        protected virtual double GetAggregatePrimitive(IEnumerable<IUIItem> items)
        {
            double finalValue = 0.0;

            foreach (var item in items)
            {
                object value = GetValueFromItem(item);

                if (value != null)
                {
                    double doubleValue = (double)Convert.ChangeType(value, typeof(double));

                    finalValue += doubleValue;
                }
            }

            return finalValue;
        }

        protected virtual object GetValueFromItem(IUIItem item)
        {
            var textBox = item as TextBox;

            if (textBox != null)
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

        public class ConstraintValueProvider : IConstraintValueProvider
        {
            private List<IUIItem> _items;
            private Func<IUIItem, object> _valueFunc;

            public ConstraintValueProvider(List<IUIItem> items, Func<IUIItem, object> valueFunc)
            {
                _items = items;
                _valueFunc = valueFunc;
            }

            public object ProvideValue(Type valueType, object defaultValue, params string[] propertyNames)
            {
                object returnValue = null;

                foreach (var propertyName in propertyNames)
                {
                    var lowerCase = propertyName;

                    if (char.IsUpper(lowerCase[0]))
                    {
                        lowerCase = char.ToLower(lowerCase[0]).ToString() + lowerCase.Substring(1);
                    }

                    foreach (var item in _items)
                    {
                        if (item.Id == lowerCase || item.Id == propertyName)
                        {
                            returnValue = _valueFunc(item);
                        }

                        if (returnValue != null)
                        {
                            returnValue = Convert.ChangeType(returnValue, valueType);
                            break;
                        }
                    }

                    if (returnValue != null)
                    {
                        break;
                    }
                }

                return returnValue;
            }
        }
    }
}
