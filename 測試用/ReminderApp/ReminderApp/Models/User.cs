using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReminderApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        
        public ICollection<Reminder>? Reminders { get; set; }
    }
}
