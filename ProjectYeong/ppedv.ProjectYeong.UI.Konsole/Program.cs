using ppedv.ProjectYeong.Data.EF;
using ppedv.ProjectYeong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.ProjectYeong.UI.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init:
            Core core = new Core(new EFRepository(new EFContext()));

            core.GenerateTestData();

            foreach (var item in core.GetAllBooks())
            {
                Console.WriteLine($"{item.ID}: {item.Title} von {item.Author}");
            }

            Console.WriteLine("--- ENDE ---");
            Console.ReadKey();
        }
    }
}
