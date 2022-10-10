using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TeacherUniv.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        public List<TeacherInfo> listTeachers = new List<TeacherInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=teacher;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql= "SELECT * FROM teachers";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TeacherInfo teacherInfo = new TeacherInfo();
                                teacherInfo.id = "" + reader.GetInt32(0);
                                teacherInfo.name = reader.GetString(1);
                                teacherInfo.email = reader.GetString(2);
                                teacherInfo.phone = reader.GetString(3);
                                teacherInfo.address = reader.GetString(4);
                                teacherInfo.created_at = reader.GetDateTime(5).ToString();

                                listTeachers.Add(teacherInfo);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exceptions: " + ex.ToString());
            }
        }
    }
    public class TeacherInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}
