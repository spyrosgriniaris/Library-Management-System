using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project2k18.Controllers
{
    public class totalBackupController : Controller
    {
        // GET: totalBackup
        [HttpPost]
        public string Index()
        {
            string connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";
            string file = "C:\\backups\\backup.bak";
            if (!System.IO.Directory.Exists("C:\\backups"))
            {
                System.IO.Directory.CreateDirectory("C:\\backups");
            }
            string sql = "BACKUP DATABASE pubs  TO DISK = '" + file + "'";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();


            conn.Close();





            return "Επιτυχής Αποθήκευση.";
        }

        [HttpPost]
        public string Restore(string filename)
        {
            string file = "C:\\backups\\backup.bak";
            if (System.IO.File.Exists(file))
            {
                String sql = "Restore  DATABASE pubs  from  DISK = '" + file + "'";

                string connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";


                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand comm = new SqlCommand(sql, conn);

                //conn.Open();
                return "Η ανάκτηση έγινε με επιτυχία";
                //conn.Close();
                
                
            }
            else
            {
                return "Η ανάκτηση απέτυχε";
            }
        }
    }
}