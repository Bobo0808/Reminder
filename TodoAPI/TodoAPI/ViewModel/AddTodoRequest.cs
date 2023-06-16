using System.ComponentModel.DataAnnotations;
using TodoAPI.Models;

namespace TodoAPI.ViewModel
{
    public class AddTodoRequest
    {
        public int TodoId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "請輸入你的主題")]
        public string Category { get; set; } = null!;
        [Required]
        public string Remark { get; set; } = null!;
        public DateTime? AddTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public bool? IsCompleted { get; set; }

        public virtual Webuser User { get; set; }
    }
}
