using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAISY.Models;

namespace DAISY.Controllers
{
    public class KichCoController : Controller
    {
        private DaisyContext db = new DaisyContext();

        // GET: KichCo
        public ActionResult Index()
        {
            return View(db.tb_KICHCO.ToList());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Quanly()
        {
            return View(db.tb_KICHCO.ToList());
        }

        // GET: KichCo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KICHCO tb_KICHCO = db.tb_KICHCO.Find(id);
            if (tb_KICHCO == null)
            {
                return HttpNotFound();
            }
            return View(tb_KICHCO);
        }

        // GET: KichCo/Create
        public ActionResult Create()
        {
            ViewBag.listkc = db.tb_KICHCO.OrderBy(p => p.TENKICHCO).ToList();
            return View();
        }

        // POST: KichCo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKICHCO,TENKICHCO,THETICH_TRONGLUONG")] tb_KICHCO tb_KICHCO)
        {
            if (ModelState.IsValid)
            {
                db.tb_KICHCO.Add(tb_KICHCO);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.listkc = db.tb_KICHCO.OrderBy(p => p.TENKICHCO).ToList();
            return View(tb_KICHCO);
        }

        // GET: KichCo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KICHCO tb_KICHCO = db.tb_KICHCO.Find(id);
            if (tb_KICHCO == null)
            {
                return HttpNotFound();
            }
            ViewBag.listkc = db.tb_KICHCO.OrderBy(p => p.TENKICHCO).ToList();
            return View(tb_KICHCO);
        }

        // POST: KichCo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKICHCO,TENKICHCO,THETICH_TRONGLUONG")] tb_KICHCO tb_KICHCO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_KICHCO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.listkc = db.tb_KICHCO.OrderBy(p => p.TENKICHCO).ToList();
            return View(tb_KICHCO);
        }

        // GET: KichCo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KICHCO tb_KICHCO = db.tb_KICHCO.Find(id);
            if (tb_KICHCO == null)
            {
                return HttpNotFound();
            }
            return View(tb_KICHCO);
        }

        // POST: KichCo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_KICHCO tb_KICHCO = db.tb_KICHCO.Find(id);
            db.tb_KICHCO.Remove(tb_KICHCO);
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
