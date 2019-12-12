using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geldquelle
{
    public enum Reichtum
    {
        Zero,
        Arm,
        Mittelstand,
        Reich,
        SehrReich,
        Oberes1Prozent
    }
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
        public Reichtum Reichtum
        {
            get
            {
                if (Kontostand == 0)
                    return Reichtum.Zero;
                else if (Kontostand < 100)
                    return Reichtum.Arm;
                else if (Kontostand < 10_000)
                    return Reichtum.Mittelstand;
                else if (Kontostand < 100_000)
                    return Reichtum.Reich;
                else if (Kontostand < 10_000_000)
                    return Reichtum.SehrReich;
                else
                    return Reichtum.Oberes1Prozent;
            }
           
        }

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
