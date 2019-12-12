using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geldquelle.Tests
{
    [TestClass]
    public class ÖffnungszeitenTests
    {
        // Mo - Fr 10:30 bis 19:00
        // Sa      10:30 bis 14:00
        // So zu

        [TestMethod]
        [DataRow(2019, 12, 9, 10, 29,false)] // Montag 10:29 -> false
        [DataRow(2019, 12, 9, 10, 30,true)] // Montag 10:30 -> true
        [DataRow(2019, 12, 9, 18, 59, true)] // Montag 10:29 -> false
        [DataRow(2019, 12, 9, 19, 00, false)] // Montag 10:30 -> true
        [DataRow(2019, 12, 13, 10, 29, false)] // Freitag 10:29 -> false
        [DataRow(2019, 12, 13, 10, 30, true)] // Freitag 10:30 -> true
        [DataRow(2019, 12, 13, 18, 59, true)] // Freitag 10:29 -> false
        [DataRow(2019, 12, 13, 19, 00, false)] // Freitag 10:30 -> true
        [DataRow(2019, 12, 14, 10, 29, false)] // Samstag 10:29 -> false
        [DataRow(2019, 12, 14, 10, 30, true)] // Samstag 10:30 -> true
        [DataRow(2019, 12, 14, 13, 59, true)] // Samstag 10:29 -> false
        [DataRow(2019, 12, 14, 14, 00, false)] // Samstag 10:30 -> true
        [DataRow(2019, 12, 15, 10, 30, false)] // Sonntag zu
        [DataRow(2019, 12, 15, 13, 59, false)] // Sonntag zu
        [DataRow(2019, 12, 15, 14, 00, false)] // Sonntag zu
        public void IsOpen(int jahr, int monat, int tag, int stunde, int minute,bool expectedResult)
        {
            Öffnungszeiten öz = new Öffnungszeiten();
            DateTime testDatum = new DateTime(jahr, monat, tag, stunde, minute, 00);
            Assert.AreEqual(expectedResult,öz.IsOpen(testDatum));
        }
    }
}
