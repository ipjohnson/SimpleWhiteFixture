using FluentAssertions;
using SimpleWhiteFixture.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWhiteFixture.Example.Tests
{
    public class BaseUITheoryTests
    {
        [UITheory]
        [Application("SimpleWhiteFixture.Example.exe")]
        public void UITheory_LaunchTestApp_WindowFixtureNotNull(IWindowFixture i)
        {
            i.Get.Text.From("LeftTopLabel").Should().Be("Test Label");
        }
    }
}
