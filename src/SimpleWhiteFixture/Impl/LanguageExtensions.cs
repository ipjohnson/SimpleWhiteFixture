using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace SimpleWhiteFixture.Impl
{
    internal static class LanguageExtensions
    {
        public static void ApplyClickAction(this IWindowFixture fixture, SearchCriteria by, Action<IUIItem> action, ClickMode clickMode)
        {
            var elements = fixture.Instance.GetMultiple(by);
            IUIItem element = null;

            switch (clickMode)
            {
                case ClickMode.ClickAll:
                    elements.Apply(action);
                    break;

                case ClickMode.ClickAny:
                    element = elements.FirstOrDefault();
                    if (element != null)
                        action(element);
                    break;

                case ClickMode.ClickFirst:
                    element = elements.FirstOrDefault();
                    if (element == null)
                    {
                        throw new Exception("No elements found using criteria " + by);
                    }
                    action(element);
                    break;
                case ClickMode.ClickOne:
                    foreach (var o in elements)
                    {
                        if (element == null)
                        {
                            element = o;
                        }
                        else
                        {
                            throw new Exception("Found to many elements using criteria " + by);
                        }
                    }
                    if (element == null)
                    {
                        throw new Exception("No elements found using criteria " + by);
                    }
                    action(element);
                    break;
            }
        }

        public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T t in enumerable)
            {
                action(t);
            }
        }    
    }
}
