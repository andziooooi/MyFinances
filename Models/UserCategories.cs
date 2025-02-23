using System.ComponentModel.DataAnnotations;

namespace MyFinances.Models
{
    public class UserCategories
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public virtual List<Transactions> Transactions { get; set; }
    }
}
