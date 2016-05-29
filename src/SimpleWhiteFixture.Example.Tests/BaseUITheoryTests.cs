using CloseEnoughEquality;
using FluentAssertions;
using SimpleWhiteFixture.Example.Tests.Models;
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

        [UITheory]
        [Application("SimpleWhiteFixture.Example.exe")]
        public void UITheory_LaunchTestApp_FillGetModel(IWindowFixture i)
        {
            var model = i.Data.Generate<IntTextBoxModel>();

            i.Fill().With(model);

            var model2 = i.Get.ValuesAs<IntTextBoxModel>().From();

            CloseEnough.Equals(model, model2);
        }

        [UITheory]
        [Application("SimpleWhiteFixture.Example.exe")]
        public void UITheory_LaunchTestApp_AutoFillGetModel(IWindowFixture i)
        {
            i.AutoFill();

            var model = i.Get.ValuesAs<StringTextBoxModel>().From();

            model.Should().NotBeNull();
            model.TextBox1.Should().NotBeNullOrEmpty();
            model.TextBox2.Should().NotBeNullOrEmpty();
            model.TextBox3.Should().NotBeNullOrEmpty();
            model.TextBox4.Should().NotBeNullOrEmpty();
            model.TextBox5.Should().NotBeNullOrEmpty();
            model.TextBox6.Should().NotBeNullOrEmpty();
        }
    }
}
