using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pose;
using System;
using System.Collections.Generic;
using System.IO;
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


        [TestMethod]
        public void IsNowOpen_returns_FakeFramework() // VisualStudio Enterprise
        {
            // References -> Rechtsklick -> Add Fakes Libary
            Öffnungszeiten öz = new Öffnungszeiten();

            // Definieren, dass statt DateTime.Now unser Fake genommen werden soll
            using (ShimsContext.Create())
            { // Codebereich, wo unsere Fake-Konfiguration gültig ist:

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(1848, 3, 13, 12, 45, 59);

                DateTime date = DateTime.Now;

                Assert.IsTrue(öz.IsNowOpen());

                // Andere Varianten:
                System.IO.Fakes.ShimFile.ExistsString = file => true; // jede datei Existiert ;)

                if (File.Exists("7:\\demo/abc&%$%$\\&&%$&\t\tasd\tdemo.&%$.exe"))
                    ;
                else
                    ;
            }

            DateTime date2 = DateTime.Now;

        }

        [TestMethod]
        public void IsNowOpen_returns_tonerdo_pose() // OpenSource: Pose
        {
            Shim fakeDateTimeNow = Shim.Replace(() => DateTime.Now)
                                       .With(() => new DateTime(2199, 12, 24, 13, 55, 00));

            DateTime heute = DateTime.Now; // original

            PoseContext.Isolate(() =>
            {
                // Logik:.....
                heute = DateTime.Now;
            },fakeDateTimeNow);
        }
    }
}
