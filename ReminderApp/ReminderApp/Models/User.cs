using System.ComponentModel.DataAnnotations;

namespace ReminderApp.Models
{
    public class User
    {
        [Key]
        public int U_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public ICollection<Reminder>? CardOrders { get; set; }
    }
}
