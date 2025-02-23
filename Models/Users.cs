using System.ComponentModel.DataAnnotations;

namespace MyFinances.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
