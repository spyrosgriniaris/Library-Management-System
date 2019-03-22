using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc_project2k18.Models;

namespace mvc_project2k18.Controllers
{
    public class storeSalesInSpecificTimesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: storeSalesInSpecificTimes
        public ActionResult Index()
        {
            List<Models.storeSalesInSpecificTime> list1 = new List<storeSalesInSpecificTime>();
            String sql = "select sales.ord_num,stores.stor_name,stores.stor_address,sales.ord_date, titles.title from sales"
                + " inner join stores on sales.stor_id = stores.stor_id"
                + " inner join titles on sales.title_id = titles.title_id"
                + " order by ord_date,title asc";

            string strCon = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";

            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Models.storeSalesInSpecificTime storeSales = new Models.storeSalesInSpecificTime();
                storeSales.id = nwReader[0].ToString();
                storeSales.store_name = nwReader[1].ToString();
                storeSales.store_adress = nwReader[2].ToString();
                storeSales.title = nwReader[4].ToString();
                storeSales.date = nwReader[3].ToString();

                list1.Add(storeSales);
            }
            nwReader.Close();
            conn.Close();
            return View(list1);
        }

        [HttpPost]
        public ActionResult Index(string searchString, string searchdate1, string searchdate2)
        {
            List<Models.storeSalesInSpecificTime> list1 = new List<storeSalesInSpecificTime>();

            string searchdate = "AND (sales.ord_date BETWEEN '" + searchdate1 + "' AND '" + searchdate2 + "')";
            if (searchdate1 == "" || searchdate2 == "")
            {
                searchdate = "";

            }

            string stringwhere = "WHERE(stores.stor_name LIKE '%" + searchString + "%')";

            String sql = "select sales.ord_num,stores.stor_name,stores.stor_address,sales.ord_date, titles.title from sales"
                + " inner join stores on sales.stor_id = stores.stor_id"
                + " inner join titles on sales.title_id = titles.title_id "
                + stringwhere + searchdate
                + "order by stores.stor_name ASC";

            string strCon = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";

            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Models.storeSalesInSpecificTime storeSales = new Models.storeSalesInSpecificTime();
                storeSales.id = nwReader[0].ToString();
                storeSales.store_name = nwReader[1].ToString();
                storeSales.store_adress = nwReader[2].ToString();
                storeSales.title = nwReader[4].ToString();
                storeSales.date = nwReader[3].ToString();

                list1.Add(storeSales);
            }
            nwReader.Close();
            conn.Close();
            return View(list1);

        }
    }
}
