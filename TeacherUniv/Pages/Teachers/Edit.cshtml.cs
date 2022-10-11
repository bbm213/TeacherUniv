using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TeacherUniv.Pages.Teachers
{
    public class EditModel : PageModel
    {
        public TeacherInfo teacherInfo = new TeacherInfo();
        public string errorMessage = "";
        public string successMessage = "";
        
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=teacher;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM teachers WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@id",id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                teacherInfo.id = "" + reader.GetInt32(0);
                                teacherInfo.name = reader.GetString(1);
                                teacherInfo.email = reader.GetString(2);
                                teacherInfo.phone = reader.GetString(3);
                                teacherInfo.address = reader.GetString(4);
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            teacherInfo.id = Request.Form["id"];
            teacherInfo.name = Request.Form["name"];
            teacherInfo.email = Request.Form["email"];
            teacherInfo.phone = Request.Form["phone"];
            teacherInfo.address = Request.Form["address"];

            if (teacherInfo.name.Length == 0 || teacherInfo.email.Length == 0 ||
                teacherInfo.phone.Length == 0 || teacherInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required ";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=teacher;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE teachers " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address" +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", teacherInfo.name);
                        command.Parameters.AddWithValue("@email", teacherInfo.email);
                        command.Parameters.AddWithValue("@phone", teacherInfo.phone);
                        command.Parameters.AddWithValue("@address", teacherInfo.address);
                        command.Parameters.AddWithValue("@id",teacherInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Teachers/Index");        }
    }
}
