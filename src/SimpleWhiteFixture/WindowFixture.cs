using SimpleFixture;
using SimpleWhiteFixture.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;

namespace SimpleWhiteFixture
{
    public class WindowFixture : IWindowFixture
    {
        protected readonly WindowFixtureConfiguration _configuration;
        protected readonly Window _window;
        protected readonly Application _application;
        protected readonly Fixture _data;
        private bool _initialized;

        public WindowFixture(Application application, Window window, Fixture data = null, WindowFixtureConfiguration configuration = null)
        {
            _application = application;
            _window = window;
            _data = data ?? new Fixture();
            _configuration = configuration ?? new WindowFixtureConfiguration();
        }

        public Window Instance { get { return _window; } }

        public Fixture Data { get { return _data; } }

        public WindowFixtureConfiguration Configuration { get { return _configuration; } }

        public IGetAction Get
        {
            get
            {
                Initialize();

                return _data.Locate<IGetAction>(constraints: GetConstrainObject());
            }
        }

        public IWindowFixture Click(string id)
        {
            Initialize();

            return _data.Locate<IClickAction>(constraints: GetConstrainObject()).Click(id);
        }

        public IWindowFixture DoubleClick(string id)
        {
            Initialize();

            return _data.Locate<IDoubleClickAction>(constraints: GetConstrainObject()).DoubleClick(id);
        }

        public IWindowFixture Click(SearchCriteria by, ClickMode clickMode = ClickMode.ClickFirst)
        {
            Initialize();

            return _data.Locate<IClickAction>(constraints: GetConstrainObject()).Click(by, clickMode);
        }

        public IWindowFixture DoubleClick(SearchCriteria by, ClickMode clickMode = ClickMode.ClickFirst)
        {
            Initialize();

            return _data.Locate<IDoubleClickAction>(constraints: GetConstrainObject()).DoubleClick(by, clickMode);
        }

        public IWindowFixture NewWindow(string windowName)
        {
            Initialize();

            var newWindow = _application.GetWindow(windowName);

            return CreateNewWindowFixture(newWindow);
        }

        public IWindowFixture NewWindow(Window window)
        {
            Initialize();

            return CreateNewWindowFixture(window);
        }

        public IWindowFixture WaitWhileBusy()
        {
            Initialize();

            return _data.Locate<IWaitWhileBusyAction>(constraints: GetConstrainObject()).WaitWhileBusy();
        }

        public IWindowFixture WaitTill(Window.WaitTillDelegate till)
        {
            Initialize();

            return _data.Locate<IWaitTillAction>(constraints: GetConstrainObject()).WaitTill(till);
        }

        public T Yields<T>()
        {
            Initialize();

            return _data.Locate<IYieldAction>(constraints: GetConstrainObject()).Yields<T>();
        }

        public IFillWithAction Fill(string form = null)
        {
            Initialize();

            return _data.Locate<IFillAction>(constraints: GetConstrainObject()).Fill(form);
        }

        public IFillWithAction Fill(SearchCriteria by)
        {
            Initialize();

            return _data.Locate<IFillAction>(constraints: GetConstrainObject()).Fill(by);
        }

        public IWindowFixture Focus(string id)
        {
            Initialize();

            return _data.Locate<IFocusAction>(constraints: GetConstrainObject()).Focus(id);
        }

        public IWindowFixture Focus(SearchCriteria by)
        {
            Initialize();

            return _data.Locate<IFocusAction>(constraints: GetConstrainObject()).Focus(by);
        }

        public IIntoAction Enter(string value)
        {
            Initialize();

            return _data.Locate<IEnterAction>(constraints: GetConstrainObject()).Enter(value);
        }

        public IIntoAction Key(KeyboardInput.SpecialKeys key)
        {
            Initialize();

            return _data.Locate<IKeyInAction>(constraints: GetConstrainObject()).Key(key);
        }

        public IWindowFixture AutoFill(string startingPoint = null, object seed = null)
        {
            Initialize();

            return _data.Locate<IAutoFillAction>(constraints: GetConstrainObject()).AutoFill(startingPoint, seed);
        }

        public IWindowFixture AutoFill(SearchCriteria by, object seed = null)
        {
            Initialize();

            return _data.Locate<IAutoFillAction>(constraints: GetConstrainObject()).AutoFill(by, seed);
        }


        protected void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            InitializeContainer();
        }

        protected virtual void InitializeContainer()
        {
            _data.ExportAs<ClickAction, IClickAction>();
            _data.ExportAs<DoubleClickAction, IDoubleClickAction>();
            _data.ExportAs<RightClickAction, IRightClickAction>();
            _data.ExportAs<GetAction, IGetAction>();
            _data.ExportAs<WaitWhileBusyAction, IWaitWhileBusyAction>();
            _data.ExportAs<WaitTillAction, IWaitTillAction>();
            _data.ExportAs<YieldAction, IYieldAction>();
            _data.ExportAs<FromAction<string>, IFromAction<string>>();            
            _data.ExportAs<FillAction, IFillAction>();
            _data.ExportAs<FillWithAction, IFillWithAction>();
            _data.ExportAs<AutoFillAction, IAutoFillAction>();
            _data.ExportAs<FocusAction, IFocusAction>();
            _data.ExportAs<EnterAction, IEnterAction>();
            _data.ExportAs<KeyInAction, IKeyInAction>();
            _data.ExportAs<IntoAction, IIntoAction>();
        }

        protected virtual object GetConstrainObject()
        {
            return new { _Values = new[] { this } };
        }

        protected virtual IWindowFixture CreateNewWindowFixture(Window newWindow)
        {
            return new WindowFixture(_application, newWindow, Data, _configuration);
        }

    }
}
