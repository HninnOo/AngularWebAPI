using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"Select EmployeeId, EmployeeName, Department, DateOfJoining, PhotoFileName
                            From dbo.Employee; ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {
                string query = @" Insert into dbo.Employee
                                    values('" + emp.EmployeeName +
                                            @"','" + emp.Department +
                                            @"','" + emp.DateOfJoining +
                                            @"','" + emp.PhotoFileName + @"')";

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

        public string Put(Employee emp)
        {
            try
            {
                string query = @" Update dbo.Employee
                                  Set EmployeeName ='" + emp.EmployeeName +
                                        @"',Department = '" + emp.Department +
                                        @"',DateOfJoining = '" + emp.DateOfJoining +
                                        @"',PhotoFileName = '" + emp.PhotoFileName +
                                  @"' Where EmployeeId=" + emp.EmployeeId;

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
                string query = @"Delete from dbo.Employee
                                    Where EmployeeId=" + id;

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

        [Route("api/Employee/GetAllDepartments")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartments()
        {
            string query = @"Select Department from dbo.Employee;";
            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]
        [HttpPost]
        public string SaveFile()
        {
            try
            {
                var fileIdex = HttpContext.Current.Request.Files[0];
                var fileName = fileIdex.FileName;
                fileIdex.SaveAs(HttpContext.Current.Server.MapPath("~/Photos/" + fileName));
                return fileName;
            }
            catch (Exception)
            {
                return "anonymous.jpg";                
            }
        }
    }
}
