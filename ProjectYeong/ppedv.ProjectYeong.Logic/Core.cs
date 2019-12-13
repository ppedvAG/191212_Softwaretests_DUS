using AutoFixture;
using ppedv.ProjectYeong.Domain;
using ppedv.ProjectYeong.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ppedv.ProjectYeong.Logic
{
    public class Core
    {
        public Core(IRepository repo)
        {
            this.repo = repo;
        }
        public Core(IDevice device)
        {
            this.device = device;
        }

        private readonly IRepository repo;
        private readonly IDevice device;

        // Geschäftslogik

        public void GenerateTestData()
        {
            Fixture fix = new Fixture();

            //var stores = fix.CreateMany<BookStore>(5);
            var stores = fix.Build<BookStore>()
                            .With(x => x.Name, "Michis Store")
                            .CreateMany(5);

            foreach (var store in stores)
            {
                repo.Add(store);
            }

            repo.Save();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return repo.GetAll<Book>();
        }

        public Book[] PrintBooks(int amount)
        {
            if (amount < 0)
                throw new ArgumentException();

            Book[] newBooks = new Book[amount];
            for (int i = 0; i < amount; i++)
            {
                newBooks[i] = device.PrintBook();
            }

            return newBooks;
        }
    }
}
