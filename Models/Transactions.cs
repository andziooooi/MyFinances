using System.ComponentModel.DataAnnotations;

namespace MyFinances.Models
{
    public class Transactions
    {
        [Key]
        public int ID { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int UsersID { get; set; }
        public int CategoriesID { get; set; }
    }
}
