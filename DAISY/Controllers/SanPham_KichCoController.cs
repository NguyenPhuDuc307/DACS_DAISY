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
    public class SanPham_KichCoController : Controller
    {
        private DaisyContext db = new DaisyContext();

        // GET: SanPham_KichCo
        public ActionResult Index()
        {
            var tb_SANPHAM_KICHCO = db.tb_SANPHAM_KICHCO.Include(t => t.tb_KICHCO).Include(t => t.tb_SANPHAM).OrderBy(p => p.tb_SANPHAM.TENSANPHAM);
            return View(tb_SANPHAM_KICHCO.ToList());
        }

        // GET: SanPham_KichCo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM_KICHCO tb_SANPHAM_KICHCO = db.tb_SANPHAM_KICHCO.Find(id);
            if (tb_SANPHAM_KICHCO == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM_KICHCO);
        }

        // GET: SanPham_KichCo/Create
        public ActionResult Create()
        {
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO");
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: SanPham_KichCo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSANPHAM,IDKICHCO")] tb_SANPHAM_KICHCO tb_SANPHAM_KICHCO)
        {
            if (ModelState.IsValid)
            {
                db.tb_SANPHAM_KICHCO.Add(tb_SANPHAM_KICHCO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO", tb_SANPHAM_KICHCO.IDKICHCO);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM", tb_SANPHAM_KICHCO.IDSANPHAM);
            return View(tb_SANPHAM_KICHCO);
        }

        // GET: SanPham_KichCo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM_KICHCO tb_SANPHAM_KICHCO = db.tb_SANPHAM_KICHCO.Find(id);
            if (tb_SANPHAM_KICHCO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO", tb_SANPHAM_KICHCO.IDKICHCO);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM", tb_SANPHAM_KICHCO.IDSANPHAM);
            return View(tb_SANPHAM_KICHCO);
        }

        // POST: SanPham_KichCo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSANPHAM,IDKICHCO")] tb_SANPHAM_KICHCO tb_SANPHAM_KICHCO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SANPHAM_KICHCO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO", tb_SANPHAM_KICHCO.IDKICHCO);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM", tb_SANPHAM_KICHCO.IDSANPHAM);
            return View(tb_SANPHAM_KICHCO);
        }

        // GET: SanPham_KichCo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM_KICHCO tb_SANPHAM_KICHCO = db.tb_SANPHAM_KICHCO.Find(id);
            if (tb_SANPHAM_KICHCO == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM_KICHCO);
        }

        // POST: SanPham_KichCo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_SANPHAM_KICHCO tb_SANPHAM_KICHCO = db.tb_SANPHAM_KICHCO.Find(id);
            db.tb_SANPHAM_KICHCO.Remove(tb_SANPHAM_KICHCO);
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
