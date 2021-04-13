//default imports
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//user added dependencies
using System.Data.SqlClient; // for MS SQL 
using System.Data;
using Microsoft.Extensions.Configuration;
using webAPIv3.models; // import Employee.cs and Department.cs models 
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Npgsql;       // PostgreSQL 
namespace webAPIv3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //
        //
        //      Npgsql CONNECTION TRIAL HERE!!!!
        //      WORKING FINE NOW!!
        //
        //
        private void npgConnDriver(string query, DataTable table)
        {
            try
            {
                string npgSqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(npgSqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
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
                throw;
            }
        }

        //
        //                  MS SQL IMPLEMENTATION
        //
        // private void connDriver(string query, DataTable table)
        // {
        //    try
        //    {
        //        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //        SqlDataReader myReader;
        //        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //        {
        //            myCon.Open();
        //            using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //            {
        //                myReader = myCommand.ExecuteReader();
        //                table.Load(myReader);
        //                myReader.Close();
        //                myCon.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //      Console.WriteLine( "\nMessage ---\n{0}", ex.Message );
        //      Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink );
        //      Console.WriteLine( "\nSource ---\n{0}", ex.Source );
        //      Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace );
        //      Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite );
        //      throw new NotImplementedException();
        //      throw;
        //    }
        // }
        [HttpGet]
        public JsonResult Get()
        {
            // MS SQL
            //
            // string query = @"
            // select EmployeeId, EmployeeName, Department, convert(varchar(10), DateofJoining,120) as DateOfJoining, PhotoFileName from dbo.Employee";


            //  edited query to comply to POSTGRESQL
            //
            string query = @"
            select EmployeeId, EmployeeName, Department, CAST(DateOfJoining as varchar(10)), PhotoFileName from Employee";


            DataTable table = new DataTable();
            //  connDriver(query, table); // FOR MS SQL
            npgConnDriver(query, table); // POSTGRESQL
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Employee emp)
        {

            // POSTGRESQL
            //
            string query =
                @"
                    insert into Employee
                    (EmployeeName,Department,DateOfJoining,PhotoFileName)
                    values  
                    ('" + emp.EmployeeName + @"'
                    ,'" + emp.Department + @"'
                    ,'" + emp.DateOFJoining + @"'
                    ,'" + emp.photoFileName + @"'
                    )
                ";

            // MS SQL
            //
            // string query =
            //    @"
            //        insert into dbo.Employee
            //        (EmployeeName,Department,DateOfJoining,PhotoFileName)
            //        values  
            //        ('" + emp.EmployeeName + @"'
            //        ,'" + emp.Department + @"'
            //        ,'" + emp.DateOFJoining + @"'
            //        ,'" + emp.photoFileName + @"'
            //        )
            //    ";
            DataTable table = new DataTable();
            // connDriver(query, table); // FOR MS SQL
            npgConnDriver(query, table);
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            // POSTGRESQL
            //
            string query =
                @"
                    update Employee set 
                    EmployeeName = '" + emp.EmployeeName + @"'
                    ,Department = '" + emp.Department + @"'
                    ,DateOfJoining = '" + emp.DateOFJoining + @"'
                    where EmployeeId = '" + emp.EmployeeId + @"'
                ";
            // MS SQL
            
            // string query =
            //    @"
            //        update dbo.Employee set 
            //        EmployeeName = '" + emp.EmployeeName + @"'
            //        ,Department = '" + emp.Department + @"'
            //        ,DateOfJoining = '" + emp.DateOFJoining + @"'
            //        where EmployeeId = '" + emp.EmployeeId + @"'
            //    ";
            DataTable table = new DataTable();
            // connDriver(query, table); // FOR MS SQL
            npgConnDriver(query, table);    // POSTGRESQL
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            // postgresql
            //
            string query =
                @"
                    Delete from Employee
                    where EmployeeId = '" + id + @"'
                ";
            // ms sql
            //
            // string query =
            //    @"
            //        Delete from dbo.Employee
            //        where EmployeeId = '" + id + @"'
            //    ";

            DataTable table = new DataTable();

            // connDriver(query, table); // FOR MS SQL

            npgConnDriver(query, table);    // postgreSQL
            return new JsonResult("Deleted Successfully");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                // i will simply extract the first file which is attached to the request body
                string filename = postedFile.FileName;
                // inject the dependency IWebHostEnvironment using microsoft.aspnetcore.hosting
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    //save the file to the photos folder
                    postedFile.CopyTo(stream);
                }
                //once saved, simply return the filename
                return new JsonResult(filename);
            }
            catch (Exception ex)
            {
                // if there's an exception, return somethingIsWrong filename
                // use breakpoints to pinpoint where the runtime error
                var exMessage = ex.Message;
                var exSource = ex.Source;
                var exStackTrace = ex.StackTrace;
                var exTargeSite = ex.TargetSite;
                return new JsonResult("somethingIsWrong.png");
            }
        }
        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            // ms sql
            // string query = @"Select DepartmentName from dbo.Department";

            //postgresql
            
            string query = @"Select DepartmentName from Department";

            DataTable table = new DataTable();
            //  connDriver(query,table);
            npgConnDriver(query, table);
            return new JsonResult(table);
        }

    }
}