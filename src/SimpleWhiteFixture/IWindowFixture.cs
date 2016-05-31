using SimpleFixture;
using SimpleWhiteFixture.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;
using static TestStack.White.UIItems.WindowItems.Window;

namespace SimpleWhiteFixture
{
    /// <summary>
    /// Click mode
    /// </summary>
    public enum ClickMode
    {
        /// <summary>
        /// Click all elements returned, throws exception when there are none
        /// </summary>
        ClickAll,

        /// <summary>
        /// Click any element returned, does not throw an exception if no elements are found
        /// </summary>
        ClickAny,

        /// <summary>
        /// Click one and only one element, throws exception when there isn't exactly one element
        /// </summary>
        ClickOne,

        /// <summary>
        /// Click the first element, throws exception when there are no element
        /// </summary>
        ClickFirst,
    }


    public interface IWindowFixture
    {
        Window Instance { get; }

        WindowFixtureConfiguration Configuration { get; }

        void Close();

        bool IsClosed { get; }

        Fixture Data { get; }

        IFillWithAction Fill(string form = null);

        IFillWithAction Fill(SearchCriteria by);

        IWindowFixture Focus(string id);

        IWindowFixture Focus(SearchCriteria by);

        IIntoAction Enter(string value);
        
        IIntoAction Key(KeyboardInput.SpecialKeys key);        

        IWindowFixture AutoFill(string startingPoint = null, object seed = null);

        IWindowFixture AutoFill(SearchCriteria by, object seed = null);

        IWindowFixture Click(string id);

        IWindowFixture DoubleClick(string id);

        IWindowFixture Click(SearchCriteria by, ClickMode clickMode = ClickMode.ClickFirst);

        IWindowFixture DoubleClick(SearchCriteria by, ClickMode clickMode = ClickMode.ClickFirst);

        IWindowFixture NewWindow(string windowName);

        IWindowFixture NewWindow(Window window);

        IGetAction Get { get; }

        IWindowFixture WaitWhileBusy();

        IWindowFixture WaitTill(WaitTillDelegate till);

        T Yields<T>();
    }
}
