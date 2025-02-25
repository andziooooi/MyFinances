using System.ComponentModel.DataAnnotations;

namespace MyFinances.Models
{
    public class Categories
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public virtual List<Transactions> Transactions { get; set; }
    }
}
