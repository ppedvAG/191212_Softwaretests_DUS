using AutoFixture;
using ppedv.ProjectYeong.Domain;
using ppedv.ProjectYeong.Domain.Interfaces;
using System;
using System.Threading;

namespace Robotech.Hardware
{
    public class BookPrinter3000 : IDevice
    {
        private Fixture fix = new Fixture();
        public Book PrintBook()
        {
            Thread.Sleep(1000);
            Console.Beep();
            return fix.Build<Book>()
                      .With(x => x.Author, "Made By BookPrinter3000")
                      .Create();
        }
    }
}
