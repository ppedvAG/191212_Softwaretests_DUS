using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.ProjectYeong.Domain;

namespace ppedv.ProjectYeong.Data.EF.Tests
{
    [TestClass]
    public class EFContextTests
    {
        const string connectionString = @"Server=(localDb)\MSSQLLocalDb;Database=ProjectYeong_Test;Trusted_Connection=True;";
        
        [TestMethod]
        public void EFContext_can_create_context()
        {
            EFContext context = new EFContext(connectionString);

            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void EFContext_can_create_DB()
        {
            using (EFContext context = new EFContext(connectionString))
            {
                if (context.Database.Exists()) // zb wenns nicht sauber gelöscht wurde
                    context.Database.Delete();

                Assert.IsFalse(context.Database.Exists());

                context.Database.Create();

                Assert.IsTrue(context.Database.Exists());
            }
        }

        // CRUD -> Create, Read, Update, Delete
        [TestMethod]
        public void EFContext_Can_CRUD_Book()
        {
            Book b1 = new Book { Author = "Michael Zöhling", Title = "Unittests leicht gemacht", ISBN = "abcasdksadlk123123", BasePrice = 9.99m, Pages = 200 };
            string newTitle = "Unittests schwer gemacht :)";

            // Create
            using (EFContext context = new EFContext(connectionString))
            {
                context.Book.Add(b1);
                context.SaveChanges();
            }

            // Check für Create / Read
            using (EFContext context = new EFContext(connectionString))
            {
                var loadedBook = context.Book.Find(b1.ID);
                Assert.AreEqual(b1.ISBN, loadedBook.ISBN); // Richtige Vorgehensweise: Graphen-Vergleich

                // Update
                loadedBook.Title = newTitle;
                context.SaveChanges(); 
            }

            // Check für Update
            using (EFContext context = new EFContext(connectionString))
            {
                var loadedBook = context.Book.Find(b1.ID);
                Assert.AreEqual(newTitle, loadedBook.Title);

                // Delete
                context.Book.Remove(loadedBook);
                context.SaveChanges();
            }

            // Check für Delete
            using (EFContext context = new EFContext(connectionString))
            {
                var loadedBook = context.Book.Find(b1.ID);
                Assert.IsNull(loadedBook);
            }
        }

        [TestMethod]
        public void EFContext_Can_CRUD_Book_FluentAssertions()
        {
            Book b1 = new Book { Author = "Michael Zöhling", Title = "Unittests leicht gemacht", ISBN = "abcasdksadlk123123", BasePrice = 9.99m, Pages = 200 };
            string newTitle = "Unittests schwer gemacht :)";

            // Create
            using (EFContext context = new EFContext(connectionString))
            {
                context.Book.Add(b1);
                context.SaveChanges();
            }

            // Check für Create / Read
            using (EFContext context = new EFContext(connectionString))
            {
                var loadedBook = context.Book.Find(b1.ID);
                // Assert.AreEqual(b1.ISBN, loadedBook.ISBN); // Richtige Vorgehensweise: Graphen-Vergleich

                // loadedBook.ISBN.Should().Be(b1.ISBN);
                // Graph:
                loadedBook.Should().BeEquivalentTo(b1);

                // Update
                loadedBook.Title = newTitle;
                context.SaveChanges();
            }

            // Check für Update
            using (EFContext context = new EFContext(connectionString))
            {
                var loadedBook = context.Book.Find(b1.ID);

                //Assert.AreEqual(newTitle, loadedBook.Title);
                loadedBook.Title.Should().Be(newTitle);

                // Delete
                context.Book.Remove(loadedBook);
                context.SaveChanges();
            }

            // Check für Delete
            using (EFContext context = new EFContext(connectionString))
            {
                var loadedBook = context.Book.Find(b1.ID);
                // Assert.IsNull(loadedBook);
                loadedBook.Should().BeNull();
            }
        }

        [TestMethod]
        public void EFContext_Autofixture_Test()
        {
            Fixture fix = new Fixture();

            Book b0 = fix.Create<Book>(); 
            List<Book> b1 = fix.CreateMany<Book>(1000).ToList();

            BookStore bs = fix.Create<BookStore>();
        }
    }
}
