using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAISY.Models;
using Microsoft.AspNet.Identity;

namespace DAISY.Controllers
{
    public class QuanLyGiaoHangController : Controller
    {
        private DaisyContext db = new DaisyContext();

        // GET: QuanLyGiaoHang
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var tb_GIOHANG = db.tb_GIOHANG.Include(t => t.AspNetUsers);
            return View(tb_GIOHANG.Where(p=> p.IDKHACHHANG == id).OrderByDescending(p=> p.NGAYTAO).ToList());
        }

        public ActionResult Quanly()
        {
            int id = (int)Session["IdCuaHang"];
            var tb_GIOHANG = db.tb_GIOHANG.OrderByDescending(p=> p.NGAYTAO).ToList();
            var tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.ToList();
            var tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.ToList();

            var list = from e in tb_GIOHANG
                       join d in tb_GIOHANG_SPC on e.IDGIOHANG equals d.IDGIOHANG into table1
                       from d in table1.ToList()
                       join i in tb_CUAHANG_SPCT on d.IDSANPHAM equals i.ID into table2
                       from i in table2.ToList()
                       select new GioHang_CuaHang
                       {
                           gh = e,
                           ghct = d,
                           ch = i
                       };
            return View(list);
        }

        public ActionResult Huy(int? id)
        {
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            tb_GIOHANG.TRANGTHAI = "Đã hủy";
            db.Entry(tb_GIOHANG).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Giaohang(int? id)
        {
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            tb_GIOHANG.TRANGTHAI = "Đang giao hàng";
            db.Entry(tb_GIOHANG).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Quanly");
        }

        public ActionResult HuyQL(int? id)
        {
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            tb_GIOHANG.TRANGTHAI = "Đã hủy";
            db.Entry(tb_GIOHANG).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Quanly");
        }

        // GET: QuanLyGiaoHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            if (tb_GIOHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_GIOHANG);
        }

        // GET: QuanLyGiaoHang/Create
        public ActionResult Create()
        {
            ViewBag.IDKHACHHANG = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: QuanLyGiaoHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGIOHANG,IDKHACHHANG,NGAYTAO,THANHTIEN,THANHTOAN,TRANGTHAI")] tb_GIOHANG tb_GIOHANG)
        {
            if (ModelState.IsValid)
            {
                db.tb_GIOHANG.Add(tb_GIOHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKHACHHANG = new SelectList(db.AspNetUsers, "Id", "Email", tb_GIOHANG.IDKHACHHANG);
            return View(tb_GIOHANG);
        }

        // GET: QuanLyGiaoHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            if (tb_GIOHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKHACHHANG = new SelectList(db.AspNetUsers, "Id", "Email", tb_GIOHANG.IDKHACHHANG);
            return View(tb_GIOHANG);
        }

        // POST: QuanLyGiaoHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGIOHANG,IDKHACHHANG,NGAYTAO,THANHTIEN,THANHTOAN,TRANGTHAI")] tb_GIOHANG tb_GIOHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_GIOHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKHACHHANG = new SelectList(db.AspNetUsers, "Id", "Email", tb_GIOHANG.IDKHACHHANG);
            return View(tb_GIOHANG);
        }

        // GET: QuanLyGiaoHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            if (tb_GIOHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_GIOHANG);
        }

        // POST: QuanLyGiaoHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            db.tb_GIOHANG.Remove(tb_GIOHANG);
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
