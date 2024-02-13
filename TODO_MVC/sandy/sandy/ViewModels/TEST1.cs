using sandy.Models;

namespace sandy.ViewModels
{
    public class TEST1
    {
        public int UserID { get; set; }
        public int TodoID { get; set; }
        public string Username { get; set; }

        //internal object Include(Func<object, object> value)
        //{
        //    throw new NotImplementedException();
        //}
        public TEST1(Todolist todolist, Webuser webuser)
        {
            UserID = todolist.UserID;
            TodoID = todolist.TodoID;
            Username = webuser.Username;
          
        }
    }
}
