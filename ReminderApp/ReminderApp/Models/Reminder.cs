using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReminderApp.Models
{
    public class Reminder
    {
        [Key]
        public int R_Id { get; set; }

        
        public int U_Id { get; set; }
        
        public string Category { get; set; }

        //備註
        public string? Remark { get; set; }

        public DateTime Add_time { get; set; }

        public DateTime Modified_time { get; set; }

        public bool Finish { get; set; }

        public  User User { get; set; }
    }
}
