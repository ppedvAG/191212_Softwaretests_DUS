using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib.Tests.NUnit
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_3_and_5_returns_8()
        {
            // Arrange:
            Calculator c = new Calculator();

            // Action:
            int result = c.Add(3, 5);

            // Assert:
            Assert.AreEqual(8, result);
        }

        // Szenarien:
        // * Korrekte Verhalten (3 und 5) (3 und -5)
        // * Falsche Verhalten (falsche Eingaben)
        // * Null - Verhalten (0 und 0)
        // * Extremfälle ( IntMax +1 ) (IntMin +  -1)

        // Null

        [Test]
        public void Add_0_and_0_returns_0()
        {
            // Arrange:
            Calculator c = new Calculator();

            // Action:
            int result = c.Add(0, 0);

            // Assert:
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Add_Int32Max_and_1_throws_OverFlowException()
        {
            // Arrange:
            Calculator c = new Calculator();

            // Action:
            Assert.Throws<OverflowException>(() => c.Add(Int32.MaxValue, 1));

            // Wenn die Exception kommt, ist alles OK
        }

        [Test]
        public void Add_Int32Min_and_N1_throws_OverFlowException()
        {
            // Arrange:
            Calculator c = new Calculator();

            // Action:
            Assert.Throws<OverflowException>(() => c.Add(Int32.MinValue, -1));

            // Wenn die Exception kommt, ist alles OK
        }

        // DataRow:

        [TestCase(5, 3, 8)]
        [TestCase(5, -3, 2)]
        [TestCase(-5, 3, -2)]
        [TestCase(50000, 3, 50003)]
        [TestCase(-5, -3, -8)]
        [TestCase(0, 0, 0)]
        public void Add_returns_expected_result(int z1, int z2, int expectedResult)
        {
            // Arrange:
            Calculator c = new Calculator();

            // Action:
            int result = c.Add(z1, z2);

            // Assert:
            Assert.AreEqual(expectedResult, result);
        }
    }
}
