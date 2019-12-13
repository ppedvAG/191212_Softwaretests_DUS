using ppedv.ProjectYeong.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Robotech.Hardware.Tests
{
    public class BookPrinter3000Tests
    {
        [Fact]
        public void BookPrinter3000_can_print_a_Book()
        {
            BookPrinter3000 bp = new BookPrinter3000();

            Book result = bp.PrintBook();

            Assert.NotNull(result);
        }
    }
}
