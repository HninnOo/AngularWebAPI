using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Optimization;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = "Select DepartmentId, DepartmentName from dbo.Department order by DepartmentId, DepartmentName;";

            using (var con = new SqlConnection (ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }               
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dept)
        {
            try
            {
                string query = @" Insert into dbo.Department
                                    values('" + dept.DepartmentName + @"')";

                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString);
               
                
                    var cmd = new SqlCommand(query, con);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();                                  
                
                return "Added Successfuly";
            }
            catch (Exception)
            {                
                return "Failed to add";                
            }           
        }

        public string Put(Department dept)
        {
            try
            {
                string query = @" Update dbo.Department
                                  Set DepartmentName ='" + dept.DepartmentName + 
                                  @"' Where DepartmentId=" + dept.DepartmentId;

                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString);
                var cmd = new SqlCommand(query, con);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to update";                
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"Delete from dbo.Department
                                    Where DepartmentId=" + id;

                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString);
                var cmd = new SqlCommand(query, con);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }           
        }       
    }
}
