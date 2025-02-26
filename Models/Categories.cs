using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinances.Models
{
    public class Categories
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public virtual List<Transactions> Transactions { get; set; }

        [NotMapped]
        public virtual double Sum { get; set; }
    }
}
