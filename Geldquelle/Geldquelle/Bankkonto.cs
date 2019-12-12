using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geldquelle
{
    public class Bankkonto
    {
        private decimal v;

        public Bankkonto()
        {
        }

        public Bankkonto(decimal v)
        {
            this.v = v;
        }

        public decimal Kontostand { get; set; }

        public void Einzahlen(decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Abheben(decimal validAmount)
        {
            throw new NotImplementedException();
        }
    }
}
