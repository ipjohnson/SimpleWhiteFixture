using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
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

    public class FillAction : IFillAction
    {
        private IWindowFixture _fixture;

        public FillAction(IWindowFixture fixture)
        {
            _fixture = fixture;
        }

        public IFillWithAction Fill(SearchCriteria by)
        {
            List<IUIItem> list = new List<IUIItem>(_fixture.Instance.GetMultiple(by));

            return _fixture.Data.Locate<IFillWithAction>(constraints: new { _Values = new object[] { _fixture, list } });
        }

        public IFillWithAction Fill(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                List<IUIItem> list = new List<IUIItem>(_fixture.Instance.Items);

                return _fixture.Data.Locate<IFillWithAction>(constraints: new { _Values = new object[] { _fixture, list } });
            }

            return Fill(SearchCriteria.ByAutomationId(id));
        }
    }

    public class FillWithAction : IFillWithAction
    {
        private IWindowFixture _fixture;
        private List<IUIItem> _elements;

        public FillWithAction(IWindowFixture fixture, List<IUIItem> elements)
        {
            _fixture = fixture;
            _elements = elements;
        }

        public IWindowFixture With(object fillValue)
        {
            FillFields(fillValue);

            return _fixture;
        }

        protected virtual void FillFields(object fillValue)
        {
            if(fillValue.GetType().IsPrimitive)
            {
                FillFieldsWithPrimitive(fillValue);
            }
            else
            {
                FillFieldsWithComplex(fillValue);
            }
        }

        protected virtual void FillFieldsWithComplex(object fillValue)
        {
            foreach(var properties in fillValue.GetType().GetProperties())
            {
                if(properties.CanRead && 
                   properties.GetMethod.IsPublic &&
                  !properties.GetMethod.GetParameters().Any() && 
                  !properties.GetMethod.IsStatic)
                {
                    var value = properties.GetValue(fillValue);
                    
                    foreach(var element in _elements)
                    {
                        if(element.Id == properties.Name)
                        {
                            element.SetValue(value.ToString());
                        }
                        else if(element is IUIItemContainer)
                        {
                            var container = element as IUIItemContainer;

                            var childElement = container.GetMultiple(SearchCriteria.ByAutomationId(properties.Name)).FirstOrDefault();

                            if (childElement != null)
                            {
                                childElement.SetValue(value.ToString());
                            }
                        }
                    }
                }
            }
        }

        protected virtual void FillFieldsWithPrimitive(object fillValue)
        {
            foreach (var element in _elements)
            {
                element.SetValue(fillValue.ToString());

                if (_fixture.Configuration.WaitWhileBusyDuringFillActions)
                {
                    _fixture.Instance.WaitWhileBusy();
                }
            }
        }
    }
}
