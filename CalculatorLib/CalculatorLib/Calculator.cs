﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class Calculator
    {
        public int Add(int z1, int z2)
        {
            checked // Prüft auf Overflow/Underflow
            {
                return z1 + z2;
            }
        }
    }
}
