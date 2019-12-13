using ppedv.ProjectYeong.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ppedv.ProjectYeong.Data.EF
{
    // Konfiguration für das EF
    public class EFContext : DbContext
    {
        // Teilnehmer - PC:
        // public EFContext() : base("Server=.;Database=ProjectYeong_Produktiv;Trusted_Connection=True;")

        // Trainer - PC:
        public EFContext() : this(@"Server=(localDb)\MSSQLLocalDb;Database=ProjectYeong_Produktiv;Trusted_Connection=True;"){}
        public EFContext(string connectionString) : base(connectionString) { }

        public DbSet<Book> Book { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<BookStore> BookStore { get; set; }
    }
}
