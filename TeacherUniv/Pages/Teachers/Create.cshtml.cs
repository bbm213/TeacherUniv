using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TeacherUniv.Pages.Teachers
{
    public class CreateModel : PageModel
    {
        public TeacherInfo teacherInfo = new TeacherInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            teacherInfo.name = Request.Form["name"];
            teacherInfo.email = Request.Form["email"];
            teacherInfo.phone = Request.Form["phone"];
            teacherInfo.address = Request.Form["address"];

            if (teacherInfo.name.Length==0 || teacherInfo.email.Length == 0||
                teacherInfo.phone.Length == 0|| teacherInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required ";
                return;
            }

            //save new teacher into the data base
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=teacher;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO teachers "+ 
                                 "(name, email, phone, address) VALUES "+
                                 "(@name, @email, @phone, @address)";
                    using (SqlCommand command= new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@name", teacherInfo.name);
                        command.Parameters.AddWithValue("@email", teacherInfo.email);
                        command.Parameters.AddWithValue("@phone", teacherInfo.phone);
                        command.Parameters.AddWithValue("@address", teacherInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            teacherInfo.name = "";
            teacherInfo.email = "";
            teacherInfo.phone = "";
            teacherInfo.address = "";
            successMessage = "New teacher added correctly";

            Response.Redirect("/Teachers/Index");
        }

    }
}
