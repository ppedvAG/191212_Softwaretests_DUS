using ppedv.ProjectYeong.Data.EF;
using ppedv.ProjectYeong.Domain.Interfaces;
using ppedv.ProjectYeong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.ProjectYeong.UI.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reflection-Beispielcode:

            //Assembly a = Assembly.LoadFrom("ppedv.ProjectYeong.Data.EF.dll");
            //IRepository dllRepo = (IRepository)Activator.CreateInstance(a.GetType("ppedv.ProjectYeong.Data.EF.EFRepository"));

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
