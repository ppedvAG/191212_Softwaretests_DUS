using FluentAssertions;
using NUnit.Framework;
using Robotech.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.ProjectYeong.Logic.Tests
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void Core_can_print_5_Books()
        {
            Core core = new Core(new BookPrinter3000());

            var result = core.PrintBooks(5);

            result.Should().HaveCount(5);
        }
    }
}
