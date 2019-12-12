using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public byte Alter { get; set; }
        public decimal Kontostand { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null || (obj is Person) == false) // null oder falscher Typ -> False
                return false;

            Person second = (Person)obj;
            if (this == obj) // Referenzgleich -> True
                return true;

            else if (this.Vorname == second.Vorname &&
                    this.Nachname == second.Nachname &&
                    this.Alter == second.Alter &&
                    this.Kontostand == second.Kontostand) // Wertegleich
                return true;
            else
                return false;
        }
    }
}
