using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib.Tests.MSTest
{
    [TestClass]
    public class PersonTests
    {
        // Snippets:
        // testc + TAB + TAB
        // testm + TAB + TAB

        [TestMethod]
        public void Person_Equals_Compare_with_null_returns_false()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };

            Assert.IsFalse(p1.Equals(null));
        }

        [TestMethod]
        public void Person_Equals_Compare_with_same_Reference_returns_true()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            Person p2 = p1;

            Assert.IsTrue(p1.Equals(p2));
        }


        [TestMethod]
        public void Person_Equals_Compare_with_same_Values_returns_true()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            Person p2 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };

            Assert.IsTrue(p1.Equals(p2));
        }

        [TestMethod]
        public void Person_Equals_Compare_with_different_Values_returns_false()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            Person p2 = new Person { Vorname = "Anna", Nachname = "Nass", Alter = 10, Kontostand = 100 };

            Assert.IsFalse(p1.Equals(p2));
        }

        [TestMethod]
        public void Person_Equals_Compare_with_different_Type_returns_false()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            StringBuilder o2 = new StringBuilder();

            Assert.IsFalse(p1.Equals(o2));
        }
    }

}
