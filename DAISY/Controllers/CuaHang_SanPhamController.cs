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
    public class CuaHang_SanPhamController : Controller
    {
        private DaisyContext db = new DaisyContext();

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/Cuahangsanpham/" + file.FileName));
            return "/Content/img/Cuahangsanpham/" + file.FileName;
        }

        // GET: CuaHang_SanPham
        public ActionResult Index()
        {
            var tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Include(t => t.tb_CUAHANG).Include(t => t.tb_KICHCO).Include(t => t.tb_SANPHAM);
            return View(tb_CUAHANG_SPCT.ToList());
        }

        // GET: CuaHang_SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);
            if (tb_CUAHANG_SPCT == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG_SPCT);
        }

        // GET: CuaHang_SanPham/Create
        public ActionResult Create()
        {
            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "TENCUAHANG");
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO");
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: CuaHang_SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCUAHANG,IDSANPHAM,IDKICHCO,TENSANPHAM,HINHANH,GIASANPHAM,TRANGTHAI")] tb_CUAHANG_SPCT tb_CUAHANG_SPCT)
        {
            if (ModelState.IsValid)
            {
                db.tb_CUAHANG_SPCT.Add(tb_CUAHANG_SPCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO");
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            return View(tb_CUAHANG_SPCT);
        }

        // GET: CuaHang_SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);
            if (tb_CUAHANG_SPCT == null)
            {
                return HttpNotFound();
            }

            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO");
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            return View(tb_CUAHANG_SPCT);
        }

        // POST: CuaHang_SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCUAHANG,IDSANPHAM,IDKICHCO,TENSANPHAM,HINHANH,GIASANPHAM,TRANGTHAI")] tb_CUAHANG_SPCT tb_CUAHANG_SPCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_CUAHANG_SPCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO");
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            return View(tb_CUAHANG_SPCT);
        }

        // GET: CuaHang_SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);
            if (tb_CUAHANG_SPCT == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG_SPCT);
        }

        // POST: CuaHang_SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);
            db.tb_CUAHANG_SPCT.Remove(tb_CUAHANG_SPCT);
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
