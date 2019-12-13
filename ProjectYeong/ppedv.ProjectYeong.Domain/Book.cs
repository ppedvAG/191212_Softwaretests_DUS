namespace ppedv.ProjectYeong.Domain
{
    public class Book : Entity
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public decimal BasePrice { get; set; }
        public string Genre { get; set; }
        // Migration mit EF:
        // 1) PackageManager-Console öffnen und im DropDown-Menü das EF-Projekt wählen

        // 2) > Enable-Migrations
        // 3) > Add-Migration MIGRATIONSNAME
        // 3.5) Änderungen im Model durchführen
        // 4) > Update-Database 
    }


}
