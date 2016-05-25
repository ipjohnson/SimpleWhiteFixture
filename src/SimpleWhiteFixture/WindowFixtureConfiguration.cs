using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWhiteFixture
{
    public class WindowFixtureConfiguration
    {
        public WindowFixtureConfiguration()
        {
            WaitWhileBusyByDefault = true;
            WaitWhileBusyDuringFillActions = false;
        }

        /// <summary>
        /// Call WaitWhileBusy inbetween fill actions, false by default
        /// </summary>
        public bool WaitWhileBusyDuringFillActions { get; set; }
        /// <summary>
        /// Call WaitWhileBusy after all actions, true by default
        /// </summary>
        public bool WaitWhileBusyByDefault { get; set; }
    }
}
