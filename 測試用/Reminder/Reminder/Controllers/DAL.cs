using Reminder.Models;
using System.Data;
using System.Data.SqlClient;

namespace Reminder.Controllers
{
    public class DAL
    {
        //用於使用註冊的方法
        public Response Registration (Users users,SqlConnection connection)
        {
            Response response = new Response ();
            try
            {
                //建立一個SqlCommand物件，用於執行名為bo_register的儲存程序
                SqlCommand cmd = new SqlCommand("bo_register", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //設定儲存參數
                cmd.Parameters.AddWithValue("@Username", users.Username);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Password", users.Password);
                //用於捕獲儲存程序的錯誤訊息
                cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = System.Data.ParameterDirection.Output;
                connection.Open();
                //執行儲存程序，獲取受影響的行數
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                //從輸出參數中獲取錯誤訊息
                string message = (string)cmd.Parameters["@ErrorMessage"].Value;
                // 根據儲存程序的結果設定回應的狀態碼和訊息
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = message;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = message;
                }

            }
            catch (Exception ex)
            {
                // 在發生例外情況時設定回應的狀態碼和訊息
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }

        //用於使用者登入的方法
        public Response Login(Users users,SqlConnection connection)
        {
            Response response = new Response ();

            try
            {
                // 建立一個 SqlDataAdapter 物件，用於執行名為 "bo_Login" 的儲存程序
                SqlDataAdapter da = new SqlDataAdapter("bo_Login", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
                da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
                // 建立一個 DataTable 來保存儲存程序的結果
                DataTable dt = new DataTable();
                da.Fill(dt);
                // 檢查 DataTable 是否有任何資料列，表示使用者是有效的
                if (dt.Rows.Count > 0)
                {
                    response.StatusCode=200;
                    response.StatusMessage = "有效的使用者";

                }
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "無效的使用者";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }

            return response;

        }
    }
}
