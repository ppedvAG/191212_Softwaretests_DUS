namespace ppedv.ProjectYeong.Domain
{
    public class Inventory : Entity
    {
        public virtual Book Book { get; set; }
        public int Amount { get; set; }
        public decimal SalePrice { get; set; } 
    }


}
