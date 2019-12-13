using FluentAssertions;
using Moq;
using NUnit.Framework;
using ppedv.ProjectYeong.Data.EF;
using ppedv.ProjectYeong.Domain;
using ppedv.ProjectYeong.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.ProjectYeong.Logic.Tests
{
    [TestFixture]
    public class CoreTests
    {
        // UnitTest für den Core
        [Test]
        public void Core_can_print_5_Books()
        {
            Mock<IDevice> mock = new Mock<IDevice>();
            mock.Setup(x => x.PrintBook())
                .Returns(() => new Book { Title = "MOCKBOOK", Author = "DAS IST EIN FAKE" });

            Core core = new Core(mock.Object);

            var result = core.PrintBooks(5);

            result.Should().HaveCount(5);

            mock.Verify(x => x.PrintBook(), Times.Exactly(5));
            // Verifiziere mir, dass auch WIRKLICH die Methode 5 mal genutzt wurde
        }

        // UnitTest für den Core
        [Test]
        public void Core_PrintBooks_with_invalid_value_throws_ArgumentException()
        {
            Mock<IDevice> mock = new Mock<IDevice>();

            Core core = new Core(mock.Object);

            core.Invoking(x => x.PrintBooks(-5))
                .Should().ThrowExactly<ArgumentException>();
            
            mock.Verify(x => x.PrintBook(), Times.Never);
            // Verifiziere mir, dass die Methode NIE ausgelöst wird !!!!!!!eineinself!111!!!
        }

        // Integrationstest für den Core mit Hardware
        [Test]
        public void Core_can_print_5_Books_with_real_Hardware()
        {
            Core core = new Core(new Robotech.Hardware.BookPrinter3000()); // <--- ECHTES GERÄT

            var result = core.PrintBooks(5);

            result.Should().HaveCount(5);
        }


        // Unittest für den Core
        [Test]
        public void Core_can_get_all_Books()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Book>())
                .Returns(() => new Book[5]);

            Core core = new Core(mock.Object);

            var result = core.GetAllBooks();

            result.Should().HaveCount(5);

            mock.Verify(x => x.GetAll<Book>(), Times.Once());
        }

        // Unittest für den Core
        [Test]
        public void Core_can_get_Book_with_highest_Price()
        {
            Mock<IRepository> mock = new Mock<IRepository>();

            Book wrongBook1 = new Book { Author = "WRONG", BasePrice = 1 };
            Book wrongBook2 = new Book { Author = "WRONG", BasePrice = 2 };
            Book correctBook = new Book { Author = "RIGHT", BasePrice = 200 };
            Book wrongBook3 = new Book { Author = "WRONG", BasePrice = 3 };

            mock.Setup(x => x.GetAll<Book>())
                .Returns(() => new Book[] { wrongBook1, wrongBook2, correctBook, wrongBook3 });

            Core core = new Core(mock.Object);

            Book result = core.GetBookWithHighestPrice();

            result.Should().Be(correctBook);

            // mock.Verify(x => x.GetAll<Book>(), Times.Once());
        }

        // Integrationstest für Core mit DB
        [Test]
        public void Core_can_get_all_Books_from_DB()
        {
            // 1) Setup für die DB:
            EFContext context = new EFContext(@"Server=(localDb)\MSSQLLocalDb;Database=ProjectYeong_Test;Trusted_Connection=True;");
            EFRepository repo = new EFRepository(context);
            Core core = new Core(repo);

            if(context.Database.Exists())
                context.Database.Delete();
            context.Database.Create(); // Cleanup DB

            repo.Add<Book>(new Book { Title = "Demo 1" });
            repo.Add<Book>(new Book { Title = "Demo 2" });
            repo.Add<Book>(new Book { Title = "Demo 3" });
            repo.Save();


            // 2) Eigentlicher Test
            var result = core.GetAllBooks();

            // 3) Check:

            // ToDo: Korrekt: Equivalent mit B1, b2, b3 usw...
            result.Should().HaveCount(3);
        }

        // UnitTest für die Logik im Core
        [Test]
        public void Core_can_create_FakeBooks_and_add_into_FakeDB()
        {
            Mock<IDevice> deviceMock = new Mock<IDevice>();
            Mock<IRepository> repoMock = new Mock<IRepository>();

            Core core = new Core(repoMock.Object, deviceMock.Object);

            int amountOfBooks = 100;

            core.PrintBooksAndSaveIntoDB(amountOfBooks);

            deviceMock.Verify(x => x.PrintBook(), Times.Exactly(amountOfBooks));
            repoMock.Verify(x => x.Add<Book>(It.IsAny<Book>()), Times.Exactly(amountOfBooks));
            repoMock.Verify(x => x.Save(), Times.Once());
        }

        // Integrationstest mit Hardware
        [Test]
        public void Core_can_create_REALBooks_and_add_into_DB()
        {
            Mock<IRepository> repoMock = new Mock<IRepository>();

            Core core = new Core(repoMock.Object, new Robotech.Hardware.BookPrinter3000());

            int amountOfBooks = 3;

            core.PrintBooksAndSaveIntoDB(amountOfBooks);

            repoMock.Verify(x => x.Add<Book>(It.IsAny<Book>()), Times.Exactly(amountOfBooks));
            repoMock.Verify(x => x.Save(), Times.Once());
        }

        // Integrationstest mit DB
        [Test]
        public void Core_can_create_FakeBooks_and_add_into_RealDB()
        {
            Mock<IDevice> deviceMock = new Mock<IDevice>();
            deviceMock.Setup(x => x.PrintBook())
                      .Returns(() => new Book { Title = "FAKE BOOK" });

            EFContext context = new EFContext(@"Server=(localDb)\MSSQLLocalDb;Database=ProjectYeong_Test;Trusted_Connection=True;");
            if (context.Database.Exists())
                context.Database.Delete();
            context.Database.Create(); // Cleanup DB

            EFRepository repo = new EFRepository(context);
            Core core = new Core(repo, deviceMock.Object);

            int amountOfBooks = 10;

            core.PrintBooksAndSaveIntoDB(amountOfBooks);

            deviceMock.Verify(x => x.PrintBook(), Times.Exactly(amountOfBooks));

            // stehen die auch in der DB drinnen ?
            repo.GetAll<Book>().Where(x => x.Title == "FAKE BOOK")
                               .Count()
                               .Should().Be(amountOfBooks);

            // Cleanup
        }


        // Alles Echt -> Akzeptanztest
        // Testen beim "Buttonklick", ob der Workflow richtig funtkioniert
        [Test]
        public void Core_can_create_and_save_Books_into_DB()
        {
            EFContext context = new EFContext(@"Server=(localDb)\MSSQLLocalDb;Database=ProjectYeong_Test;Trusted_Connection=True;");
            if (context.Database.Exists())
                context.Database.Delete();
            context.Database.Create(); // Cleanup DB

            EFRepository repo = new EFRepository(context);
            Core core = new Core(repo, new Robotech.Hardware.BookPrinter3000());

            int amountOfBooks = 10;

            core.PrintBooksAndSaveIntoDB(amountOfBooks);

            // stehen die auch in der DB drinnen ?
            repo.GetAll<Book>().Count()
                               .Should().Be(amountOfBooks);
        }
    }
}
