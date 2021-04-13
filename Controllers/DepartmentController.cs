// default imports

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// user imports 
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using webAPIv3.models; // import Employee.cs and Department.cs models 
using Npgsql; //-----> added this to try out PostgreSQL


namespace webAPIv3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration; 
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //
        //
        //      Npgsql CONNECTION TRIAL HERE!!!!
        //      WORKING FINE NOW!!
        //
        //
         private void npgConnDriver(string query, DataTable table) {
            try
            {
                string npgSqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(npgSqlDataSource)) {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon)) {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine("\nMessage ---\n{0}", ex.Message);
               Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink);
               Console.WriteLine("\nSource ---\n{0}", ex.Source);
               Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
               Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite);
            }
        }

        //
        //                  MS SQL IMPLEMENTATION
        //
        // private void connDriver(string query, DataTable table)
        // {
        //     try
        //     {
        //         string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //         SqlDataReader myReader;
        //         using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //         {
        //             myCon.Open();
        //             using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //             {
        //                 myReader = myCommand.ExecuteReader();
        //                 table.Load(myReader);
        //                 myReader.Close();
        //                 myCon.Close();
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("\nMessage ---\n{0}", ex.Message);
        //         Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink);
        //         Console.WriteLine("\nSource ---\n{0}", ex.Source);
        //         Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
        //         Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite);
        //     }
        // }

        [HttpGet]
        public JsonResult Get()
        {
            // POSTGRESQL
            string query = @"select DepartmentId, DepartmentName from department";

            // MS SQL

            // string query = @"select DepartmentId, DepartmentName from dbo.department";
            
            DataTable table = new DataTable();
            //  POSTGRESQL
             npgConnDriver(query, table);

            // FOR MS SQL
            // connDriver(query, table);
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {

            // POSTGRESQL
             //
            string query =@"
                    insert into department (departmentname) values ('" + dep.DepartmentName + @"')
                 ";
            // MS SQL
            //
            // string query = @"
            //     insert into dbo.department (departmentname) values ('" + dep.DepartmentName + @"')";

            DataTable table = new DataTable();

            npgConnDriver(query, table);    //  POSTGRESQL

            // connDriver(query, table);     // FOR MS SQL

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            // POSTGRESQL
            //
            string query =
                @"
                    update department set departmentname = '" + dep.DepartmentName + @"'
                    where departmentid = '" + dep.DepartmentId + @"'    ";
            // MS SQL
            //
            // string query =
            //    @"
            //    update dbo.Department set DepartmentName = '" + dep.DepartmentName + @"'
            //    where DepartmentId = '" + dep.DepartmentId + @"'
            //    ";

            DataTable table = new DataTable();
            npgConnDriver(query, table);    //POSTGRESQL

            // connDriver(query, table);     // FOR MS SQL
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        //need to change the argument from Department dep to an int id since it is much better if the identifier to find is the Department Id
        public JsonResult Delete(int id){
            //  POSTGRESQL
            //
            string query =
                @"
                    Delete from department
                    where departmentid = '" + id + @"'
                ";
            //  MS SQL
            //
            // string query =
            //     @"
            //     Delete from dbo.Department
            //     where DepartmentId = '" + id + @"'
            //    ";
            DataTable table = new DataTable();
            npgConnDriver(query, table);    //  POSTGRESQL
            // connDriver(query, table);     // FOR MS SQL
            return new JsonResult("Deleted Successfully");
        }

    }
}
