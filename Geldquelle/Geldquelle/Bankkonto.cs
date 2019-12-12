using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geldquelle
{
    public class Bankkonto
    {
        public Bankkonto() : this(100m) {}

        public Bankkonto(decimal Kontostand)
        {
            if (Kontostand < 0)
                throw new ArgumentException();
            this.Kontostand = Kontostand;
        }

        public decimal Kontostand { get; protected set; }

        public void Einzahlen(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException();
            Kontostand += amount;
        }

        public void Abheben(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException();
            else if (Kontostand - amount < 0)
                throw new InvalidOperationException();

            Kontostand -= amount;
        }
    }
}
