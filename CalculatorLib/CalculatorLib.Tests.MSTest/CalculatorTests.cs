using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorLib.Tests.MSTest
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_3_and_5_returns_8()
        {
            // Arrange:
            Calculator c = new Calculator();

            // Action:
            int result = c.Add(3, 5);

            // Assert:
            Assert.AreEqual(8, result);
        }
    }
}
