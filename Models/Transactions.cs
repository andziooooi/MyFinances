using System.ComponentModel.DataAnnotations;

namespace MyFinances.Models
{
    public class Transactions
    {
        [Key]
        public int ID { get; set; }
        public double Amount { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
    }
}
