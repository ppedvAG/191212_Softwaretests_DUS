using FluentAssertions;
using Moq;
using NUnit.Framework;
using ppedv.ProjectYeong.Domain;
using ppedv.ProjectYeong.Domain.Interfaces;
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
            Mock<IDevice> mock = new Mock<IDevice>();
            mock.Setup(x => x.PrintBook())
                .Returns(() => new Book { Title = "MOCKBOOK", Author = "DAS IST EIN FAKE" });

            Core core = new Core(mock.Object);

            var result = core.PrintBooks(5);

            result.Should().HaveCount(5);

            mock.Verify(x => x.PrintBook(), Times.Exactly(5));
            // Verifiziere mir, dass auch WIRKLICH die Methode 5 mal genutzt wurde
        }

        [Test]
        public void Core_PrintBooks_with_invalid_value_throws_ArgumentException()
        {
            Mock<IDevice> mock = new Mock<IDevice>();

            Core core = new Core(mock.Object);

            core.Invoking(x => x.PrintBooks(-5))
                .Should().ThrowExactly<ArgumentException>();
            
            mock.Verify(x => x.PrintBook(), Times.Never);
            // Verifiziere mir, dass die Methode NIE ausgelöst wird !!!!!!!eineinself!111!!!
        }
    }
}
