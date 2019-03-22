using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project2k18.Controllers
{
    public class incrementalBackupController : Controller
    {
        // GET: incrementalBackup
        [HttpPost]
        public string Index()
        {
            string connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";
            string file = "C:\\IncrementalBackups\\IncBackup.bak";
            if (!System.IO.Directory.Exists("C:\\IncrementalBackups"))
            {
                System.IO.Directory.CreateDirectory("C:\\IncrementalBackups");
            }
            string sql = "BACKUP DATABASE pubs  TO DISK = '" + file + "' WITH DIFFERENTIAL";
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
            string file = "C:\\IncrementalBackups\\IncBackup.bak";
            if (System.IO.File.Exists(file))
            {
                String sql = "Restore  DATABASE pubs  from  DISK = '" + file + "' WITH NORECOVERY";

                string connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";


                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand comm = new SqlCommand(sql, conn);

                String sql2 = "Restore  DATABASE pubs  from  DISK = '" + file + "' WITH FILE = 2,  RECOVERY";

                string connectionString2 = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";


                SqlConnection conn2 = new SqlConnection(connectionString);
                SqlCommand comm2 = new SqlCommand(sql2, conn2);

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