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
    public class SanPham_ToppingController : Controller
    {
        private DaisyContext db = new DaisyContext();

        // GET: SanPham_Topping
        public ActionResult Index()
        {
            var tb_SANPHAM_SPDK = db.tb_SANPHAM_SPDK.Include(t => t.tb_CUAHANG).Include(t => t.tb_SANPHAM).Include(t => t.tb_SPDK);
            return View(tb_SANPHAM_SPDK.ToList());
        }

        // GET: SanPham_Topping/Details/5
        public ActionResult Details(int? idch, int? idsp, int? idtp)
        {
            if (idch == null || idsp == null || idtp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM_SPDK tb_SANPHAM_SPDK = db.tb_SANPHAM_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSANPHAM == idsp && p.IDSPDK == idtp);
            if (tb_SANPHAM_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM_SPDK);
        }

        // GET: SanPham_Topping/Create
        public ActionResult Create()
        {
            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            ViewBag.IDSPDK = new SelectList(db.tb_CUAHANG_SPDK.Where(p => p.IDCUAHANG == idCuaHang), "IDSPDK", "TENSPDK");
            return View();
        }

        // POST: SanPham_Topping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCUAHANG,IDSANPHAM,IDSPDK")] tb_SANPHAM_SPDK tb_SANPHAM_SPDK)
        {
            tb_SANPHAM_SPDK sp_tp = db.tb_SANPHAM_SPDK.FirstOrDefault(p => p.IDCUAHANG == tb_SANPHAM_SPDK.IDCUAHANG && p.IDSANPHAM == tb_SANPHAM_SPDK.IDSANPHAM && p.IDSPDK == tb_SANPHAM_SPDK.IDSPDK);
            if (ModelState.IsValid)
            {
                if (sp_tp == null)
                {
                    db.tb_SANPHAM_SPDK.Add(tb_SANPHAM_SPDK);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Topping cho sản phẩm nào đã tồn tại, hãy thử lại";
                    return RedirectToAction("Create");
                }
            }

            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            ViewBag.IDSPDK = new SelectList(db.tb_CUAHANG_SPDK.Where(p => p.IDCUAHANG == idCuaHang), "IDSPDK", "TENSPDK");
            return View(tb_SANPHAM_SPDK);
        }

        // GET: SanPham_Topping/Edit/5
        public ActionResult Edit(int? idch, int? idsp, int? idtp)
        {
            if (idch == null || idsp == null || idtp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM_SPDK tb_SANPHAM_SPDK = db.tb_SANPHAM_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSANPHAM == idsp && p.IDSPDK == idtp);
            if (tb_SANPHAM_SPDK == null)
            {
                return HttpNotFound();
            }
            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            ViewBag.IDSPDK = new SelectList(db.tb_CUAHANG_SPDK.Where(p => p.IDCUAHANG == idCuaHang), "IDSPDK", "TENSPDK");
            return View(tb_SANPHAM_SPDK);
        }

        // POST: SanPham_Topping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCUAHANG,IDSANPHAM,IDSPDK")] tb_SANPHAM_SPDK tb_SANPHAM_SPDK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SANPHAM_SPDK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int idCuaHang = (int)Session["IdCuaHang"];
            ViewBag.IDSANPHAM = new SelectList(db.tb_CUAHANG_SPCT.Where(p => p.IDCUAHANG == idCuaHang), "IDSANPHAM", "TENSANPHAM");
            ViewBag.IDSPDK = new SelectList(db.tb_CUAHANG_SPDK.Where(p => p.IDCUAHANG == idCuaHang), "IDSPDK", "TENSPDK");
            return View(tb_SANPHAM_SPDK);
        }

        // GET: SanPham_Topping/Delete/5
        public ActionResult Delete(int? idch, int? idsp, int? idtp)
        {
            if (idch == null || idsp == null || idtp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM_SPDK tb_SANPHAM_SPDK = db.tb_SANPHAM_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSANPHAM == idsp && p.IDSPDK == idtp);
            if (tb_SANPHAM_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM_SPDK);
        }

        // POST: SanPham_Topping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idch, int? idsp, int? idtp)
        {
            tb_SANPHAM_SPDK tb_SANPHAM_SPDK = db.tb_SANPHAM_SPDK.FirstOrDefault(p => p.IDCUAHANG == idch && p.IDSANPHAM == idsp && p.IDSPDK == idtp);
            db.tb_SANPHAM_SPDK.Remove(tb_SANPHAM_SPDK);
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
