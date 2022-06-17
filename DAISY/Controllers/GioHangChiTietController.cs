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
    public class GioHangChiTietController : Controller
    {
        private DaisyContext db = new DaisyContext();

        // GET: GioHangChiTiet
        public ActionResult Index()
        {
            var tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.Include(t => t.tb_CUAHANG_SPCT).Include(t => t.tb_GIOHANG);
            return View(tb_GIOHANG_SPC.ToList());
        }

        public ActionResult Chitiet(int id)
        {
            var tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.Where(p=> p.IDGIOHANG == id);
            return View(tb_GIOHANG_SPC.ToList());
        }

        // GET: GioHangChiTiet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANG_SPC tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.Find(id);
            if (tb_GIOHANG_SPC == null)
            {
                return HttpNotFound();
            }
            return View(tb_GIOHANG_SPC);
        }

        // GET: GioHangChiTiet/Create
        public ActionResult Create()
        {
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT, "ID", "TENSANPHAM");
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "IDKHACHHANG");
            return View();
        }

        // POST: GioHangChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGIOHANG,IDSANPHAM,SOLUONGSPCHINH,GIABAN,THANHTIEN")] tb_GIOHANG_SPC tb_GIOHANG_SPC)
        {
            if (ModelState.IsValid)
            {
                db.tb_GIOHANG_SPC.Add(tb_GIOHANG_SPC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT, "ID", "TENSANPHAM", tb_GIOHANG_SPC.IDSANPHAM);
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "IDKHACHHANG", tb_GIOHANG_SPC.IDGIOHANG);
            return View(tb_GIOHANG_SPC);
        }

        // GET: GioHangChiTiet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANG_SPC tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.Find(id);
            if (tb_GIOHANG_SPC == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT, "ID", "TENSANPHAM", tb_GIOHANG_SPC.IDSANPHAM);
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "IDKHACHHANG", tb_GIOHANG_SPC.IDGIOHANG);
            return View(tb_GIOHANG_SPC);
        }

        // POST: GioHangChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGIOHANG,IDSANPHAM,SOLUONGSPCHINH,GIABAN,THANHTIEN")] tb_GIOHANG_SPC tb_GIOHANG_SPC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_GIOHANG_SPC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT, "ID", "TENSANPHAM", tb_GIOHANG_SPC.IDSANPHAM);
            ViewBag.IDGIOHANG = new SelectList(db.tb_GIOHANG, "IDGIOHANG", "IDKHACHHANG", tb_GIOHANG_SPC.IDGIOHANG);
            return View(tb_GIOHANG_SPC);
        }

        // GET: GioHangChiTiet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANG_SPC tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.Find(id);
            if (tb_GIOHANG_SPC == null)
            {
                return HttpNotFound();
            }
            return View(tb_GIOHANG_SPC);
        }

        // POST: GioHangChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_GIOHANG_SPC tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.Find(id);
            db.tb_GIOHANG_SPC.Remove(tb_GIOHANG_SPC);
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
