namespace EFFrameworkTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreFürBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ISBN = c.String(),
                        Title = c.String(),
                        Author = c.String(),
                        Pages = c.Int(nullable: false),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Genre = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BookStores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Book_ID = c.Int(),
                        BookStore_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.Book_ID)
                .ForeignKey("dbo.BookStores", t => t.BookStore_ID)
                .Index(t => t.Book_ID)
                .Index(t => t.BookStore_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "BookStore_ID", "dbo.BookStores");
            DropForeignKey("dbo.Inventories", "Book_ID", "dbo.Books");
            DropIndex("dbo.Inventories", new[] { "BookStore_ID" });
            DropIndex("dbo.Inventories", new[] { "Book_ID" });
            DropTable("dbo.Inventories");
            DropTable("dbo.BookStores");
            DropTable("dbo.Books");
        }
    }
}
