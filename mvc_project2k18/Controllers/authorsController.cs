using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc_project2k18.Models;

namespace mvc_project2k18.Controllers
{
    public class authorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: authors
        /*public ActionResult Index()
        {
            return View(db.authors.ToList());
        }*/
        public ActionResult Index(string authorCity, string searchString)
        {
            var CityLst = new List<string>();

            var CityQry = from d in db.authors
                          orderby d.city
                          select d.city;

            CityLst.AddRange(CityQry.Distinct());
            ViewBag.authorCity = new SelectList(CityLst);

            var authors = from a in db.authors
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(s => s.au_lname.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(authorCity))
            {
                authors = authors.Where(x => x.city == authorCity);
            }

            return View(authors);
        }

        // GET: authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] author author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            author author = db.authors.Find(id);
            db.authors.Remove(author);
            titleauthor ta = db.titleauthors.Find(id);
            db.titleauthors.Remove(ta);
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