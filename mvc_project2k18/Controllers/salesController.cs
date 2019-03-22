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
    public class salesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: sales
        public ActionResult Index()
        {
            var sales = db.sales.Include(s => s.store).Include(s => s.title);
            return View(sales.ToList());
        }

        // GET: sales/Details/5
        public ActionResult Details(string id, string id2, string id3)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog=pubs;Trusted_Connection=Yes;");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT ord_date,sales.stor_id,sales.ord_num,sales.qty,sales.payterms,sales.title_id FROM sales where ord_num='" + id + "' AND stor_id='" + id2 + "' AND title_id='" + id3 + "'", conn);
            sale s = new sale();

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    s.stor_id = reader["stor_id"].ToString();
                    s.ord_date = Convert.ToDateTime(reader["ord_date"].ToString());
                    s.ord_num = reader["ord_num"].ToString();
                    s.qty = short.Parse(reader["qty"].ToString());
                    s.payterms = reader["payterms"].ToString();
                    s.store = db.stores.Find(id2);
                    s.title = db.titles.Find(id3);
                }
            }

            conn.Close();

            return View(s);
        }

        // GET: sales/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (ModelState.IsValid)
            {
                db.sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            return View(sale);
        }

        // GET: sales/Edit/5
        public ActionResult Edit(string id, string id2, string id3)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog=pubs;Trusted_Connection=Yes;");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT ord_date,sales.stor_id,sales.ord_num,sales.qty,sales.payterms,sales.title_id FROM sales where ord_num='" + id + "' AND stor_id='" + id2 + "' AND title_id='" + id3 + "'", conn);
            sale s = new sale();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                // iterate your results here
                while (reader.Read())
                {
                    s.stor_id = reader["stor_id"].ToString();
                    s.ord_num = reader["ord_num"].ToString();
                    s.qty = short.Parse(reader["qty"].ToString());
                    s.payterms = reader["payterms"].ToString();
                    s.ord_date = Convert.ToDateTime(reader["ord_date"].ToString());
                    s.title = db.titles.Find(id3);
                    s.store = db.stores.Find(id2);
                    s.title_id = id3;
                    s.stor_id = id2;

                }
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", s.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", s.title_id);
            conn.Close();
            return View(s);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sale.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title1", sale.title_id);
            return View(sale);
        }

        // GET: sales/Delete/5
        public ActionResult Delete(string id, string id2, string id3)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sale s = new sale();
            s.stor_id = id2;
            s.ord_num = id;
            s.title_id = id3;
            
            /*sale sales = db.sales.Find(id3);
            if (sales == null)
            {
                return HttpNotFound();
            }*/
            return View(s);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string id2, string id3)
        {
            sale sale = db.sales.Find(id,id2,id3);
            db.sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
