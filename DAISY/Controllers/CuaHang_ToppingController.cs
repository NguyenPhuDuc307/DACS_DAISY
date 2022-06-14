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
    public class CuaHang_ToppingController : Controller
    {
        private DaisyContext db = new DaisyContext();

        

        // GET: CuaHang_Topping
        public ActionResult Index()
        {
            var tb_CUAHANG_SPDK = db.tb_CUAHANG_SPDK.Include(t => t.tb_CUAHANG).Include(t => t.tb_SPDK);
            return View(tb_CUAHANG_SPDK.ToList());
        }

        // GET: CuaHang_Topping/Details/5
        public ActionResult Details(int? idch, int? idsp)
        {
            if (idch == null || idsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG_SPDK tb_CUAHANG_SPDK = db.tb_CUAHANG_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSPDK == idsp);
            if (tb_CUAHANG_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG_SPDK);
        }

        // GET: CuaHang_Topping/Create
        public ActionResult Create()
        {
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "TENCUAHANG");
            ViewBag.IDSPDK = new SelectList(db.tb_SPDK, "IDSPDK", "TENSPDK");
            return View();
        }

        // POST: CuaHang_Topping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCUAHANG,IDSPDK,TENSPDK,HINHANH,GIABAN,TRANGTHAI")] tb_CUAHANG_SPDK tb_CUAHANG_SPDK)
        {
            if (ModelState.IsValid)
            {
                db.tb_CUAHANG_SPDK.Add(tb_CUAHANG_SPDK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "TENCUAHANG", tb_CUAHANG_SPDK.IDCUAHANG);
            ViewBag.IDSPDK = new SelectList(db.tb_SPDK, "IDSPDK", "TENSPDK", tb_CUAHANG_SPDK.IDSPDK);
            return View(tb_CUAHANG_SPDK);
        }

        // GET: CuaHang_Topping/Edit/5
        public ActionResult Edit(int? idch, int? idsp)
        {
            if (idch == null || idsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG_SPDK tb_CUAHANG_SPDK = db.tb_CUAHANG_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSPDK == idsp);
            if (tb_CUAHANG_SPDK == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "TENCUAHANG", tb_CUAHANG_SPDK.IDCUAHANG);
            ViewBag.IDSPDK = new SelectList(db.tb_SPDK, "IDSPDK", "TENSPDK", tb_CUAHANG_SPDK.IDSPDK);
            return View(tb_CUAHANG_SPDK);
        }

        // POST: CuaHang_Topping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCUAHANG,IDSPDK,TENSPDK,HINHANH,GIABAN,TRANGTHAI")] tb_CUAHANG_SPDK tb_CUAHANG_SPDK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_CUAHANG_SPDK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "TENCUAHANG", tb_CUAHANG_SPDK.IDCUAHANG);
            ViewBag.IDSPDK = new SelectList(db.tb_SPDK, "IDSPDK", "TENSPDK", tb_CUAHANG_SPDK.IDSPDK);
            return View(tb_CUAHANG_SPDK);
        }

        // GET: CuaHang_Topping/Delete/5
        public ActionResult Delete(int? idch, int? idsp)
        {
            if (idch == null || idsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG_SPDK tb_CUAHANG_SPDK = db.tb_CUAHANG_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSPDK == idsp);
            if (tb_CUAHANG_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG_SPDK);
        }

        // POST: CuaHang_Topping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idch, int? idsp)
        {
            tb_CUAHANG_SPDK tb_CUAHANG_SPDK = db.tb_CUAHANG_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSPDK == idsp);
            db.tb_CUAHANG_SPDK.Remove(tb_CUAHANG_SPDK);
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
