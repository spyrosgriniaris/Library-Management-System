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
    public class topSalesInSpecificTimesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: topSalesInSpecificTimes
        public ActionResult Index()
        {
            List<Models.topSalesInSpecificTime> list1 = new List<topSalesInSpecificTime>();
            String sql = "select authors.au_lname,authors.au_fname,SUM(titles.price*sales.qty) as total from authors inner join titleauthor On authors.au_id = titleauthor.au_id inner join titles on titleauthor.title_id = titles.title_id inner join sales on titles.title_id = sales.title_id  group by au_lname,au_fname order by total DESC ";

            string strCon = "data source=(localdb)\\MSSQLLocalDB;initial catalog=pubs;integrated security=True;MultipleActiveResultSets=True;";
            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Models.topSalesInSpecificTime topSales = new Models.topSalesInSpecificTime();
                topSales.name = nwReader[0].ToString();
                topSales.sname = nwReader[1].ToString();
                topSales.total = float.Parse(nwReader[2].ToString());

                list1.Add(topSales);
            }
            nwReader.Close();
            conn.Close();
            return View(list1);
        }
        [HttpPost]
        public ActionResult Index(string searchString, string date1, string date2)
        {

            List<Models.topSalesInSpecificTime> list1 = new List<topSalesInSpecificTime>();

            string stringwhere = "where (sales.ord_date BETWEEN '" + date1 + "' AND '" + date2 + "')";
            if (date1 == "" || date2 == "")
            {
                stringwhere = "";
            }
            string stringtop = "TOP " + searchString;
            if (searchString == "")
            {
                stringtop = "";
            }

            String sql = "select " + stringtop + " authors.au_lname,authors.au_fname,SUM(titles.price*sales.qty) as total from authors inner join titleauthor On authors.au_id = titleauthor.au_id inner join titles on titleauthor.title_id = titles.title_id inner join sales on titles.title_id = sales.title_id " + stringwhere + " group by au_lname,au_fname order by total DESC ";

            string strCon = "data source = (localdb)\\MSSQLLocalDB; initial catalog = pubs; integrated security = True; MultipleActiveResultSets = True; ";

            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Models.topSalesInSpecificTime topSales = new Models.topSalesInSpecificTime();
                topSales.name = nwReader[0].ToString();
                topSales.sname = nwReader[1].ToString();
                topSales.total = float.Parse(nwReader[2].ToString());

                list1.Add(topSales);
            }
            nwReader.Close();
            conn.Close();
            return View(list1);
        }

    }
}
