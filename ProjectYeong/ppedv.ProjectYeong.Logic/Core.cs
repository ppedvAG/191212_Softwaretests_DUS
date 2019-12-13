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
        private readonly IRepository repo;

        // Geschäftslogik

        public void GenerateTestData()
        {
            Fixture fix = new Fixture();

            var stores = fix.CreateMany<BookStore>(5);

            foreach (var store in stores)
            {
                repo.Add(store);
            }

            repo.Save();
        }
    }
}
