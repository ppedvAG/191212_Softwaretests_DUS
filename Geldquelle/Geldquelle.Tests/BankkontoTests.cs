using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geldquelle.Tests
{
    [TestClass]
    public class BankkontoTests
    {
        [TestMethod]
        public void Can_Create_Bankkonto_with_default_Balance()
        {
            Bankkonto konto = new Bankkonto();
            Assert.AreEqual(100m, konto.Kontostand);
        }

        [TestMethod]
        public void Can_Create_Bankkonto_with_set_Balance()
        {
            Bankkonto konto = new Bankkonto(2345m);
            Assert.AreEqual(2345m, konto.Kontostand);
        }

        [TestMethod]
        public void Create_Bankkonto_with_wrong_Balance_throws_ArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Bankkonto(-2345m));
        }

        [TestMethod]
        public void Einzahlen_with_valid_amount_increases_Balance()
        {
            Bankkonto konto = new Bankkonto();
            decimal amount = 1234m;

            decimal oldBalance = konto.Kontostand;
            konto.Einzahlen(amount);

            Assert.AreEqual(oldBalance + amount, konto.Kontostand);
        }

        [TestMethod]
        [DataRow(0)]        // Null-Fall
        [DataRow(-1234)]    // Negativ-Fall
        public void Einzahlen_with_invalid_amount_throws_ArgumentException(int invalidAmountInt)
        {
            Bankkonto konto = new Bankkonto();
            decimal invalidAmount = invalidAmountInt;

            Assert.ThrowsException<ArgumentException>(() => konto.Einzahlen(invalidAmount));
        }


        [TestMethod]
        [DataRow(200.22222)]
        [DataRow(400.12345)]
        [DataRow(600.00)]
        public void Abheben_with_valid_amount_decreases_Balance(double validAmountDouble)
        {
            Bankkonto konto = new Bankkonto(1000m);
            decimal oldBalance = konto.Kontostand;
            decimal validAmount = Convert.ToDecimal(validAmountDouble); // vorsicht: konvertierungsfehler

            konto.Abheben(validAmount);
            Assert.AreEqual(oldBalance - validAmount, konto.Kontostand);
        }

        [TestMethod]
        [DataRow(200.22222)]
        [DataRow(400.12345)]
        [DataRow(10.00000000001)]
        public void Abheben_with_more_than_Balance_throws_InvalidOperationException(double validAmountDouble)
        {
            Bankkonto konto = new Bankkonto(10m);
            decimal validAmount = Convert.ToDecimal(validAmountDouble);

            Assert.ThrowsException<InvalidOperationException>(() => konto.Abheben(validAmount));
        }

        [TestMethod]
        [DataRow(-200.22222)]
        [DataRow(-400.12345)]
        [DataRow(-10.00000000001)]
        public void Abheben_with_invalid_amount_throws_ArgumentException(double invalidAmountDouble)
        {
            Bankkonto konto = new Bankkonto(10m);
            decimal invalidAmount = Convert.ToDecimal(invalidAmountDouble);

            Assert.ThrowsException<ArgumentException>(() => konto.Abheben(invalidAmount));
        }

        // Zustand, den man Einchecken
    }
}
