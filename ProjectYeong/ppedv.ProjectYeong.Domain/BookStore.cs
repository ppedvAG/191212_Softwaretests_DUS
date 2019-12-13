using System.Collections.Generic;

namespace ppedv.ProjectYeong.Domain
{
    public class BookStore : Entity
    {
        public string Name { get; set; }
        public virtual HashSet<Inventory> Stock { get; set; } = new HashSet<Inventory>();
    }


}
