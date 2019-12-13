namespace ppedv.ProjectYeong.Domain
{
    public class Book : Entity
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public decimal BasePrice { get; set; }
    }


}
