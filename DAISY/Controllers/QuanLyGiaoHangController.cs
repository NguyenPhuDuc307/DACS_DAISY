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
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        public ActionResult Quanly()
        {
            DateTime ngay = DateTime.Now.Date;
            DateTime now = DateTime.Now;
            int dayOfWeek = (int)now.DayOfWeek;
            DateTime tuan = now.AddDays(-(int)now.DayOfWeek);
            DateTime thang = GetFirstDayOfMonth(now);

            if (Session["IdCuaHang"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                int id = (int)Session["IdCuaHang"];
                var tb_GIOHANG = db.tb_GIOHANG.OrderByDescending(p => p.NGAYTAO).ToList();
                var tb_GIOHANG_SPC = db.tb_GIOHANG_SPC.ToList();
                var tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.ToList();

                var list = from e in tb_GIOHANG
                           join d in tb_GIOHANG_SPC on e.IDGIOHANG equals d.IDGIOHANG into table1
                           from d in table1.ToList()
                           join i in tb_CUAHANG_SPCT on d.IDSANPHAM equals i.ID into table2
                           from i in table2.ToList()
                           where d.tb_CUAHANG_SPCT.IDCUAHANG == id
                           select new GioHang_CuaHang
                           {
                               gh = e,
                               ghct = d,
                               ch = i
                           };
                var listdoi = list.Where(p => p.gh.TRANGTHAI == "Chờ xử lý");
                Session["listdoi"] = listdoi.Count();

                ViewBag.sl = list.Count();
                ViewBag.tien = list.Where(p => p.gh.THANHTOAN == true).Sum(p => p.gh.THANHTIEN);
                ViewBag.sp = tb_GIOHANG_SPC.Where(p => p.tb_CUAHANG_SPCT.IDCUAHANG == id).Sum(p => p.SOLUONGSPCHINH);

                var listkh = list.Where(p=> p.gh.TRANGTHAI == "Đã thanh toán").GroupBy(p => p.gh.IDKHACHHANG);
                ViewBag.listkh = listkh.OrderByDescending(p=> p.Sum(a => a.gh.THANHTIEN)).Take(5);

                ViewBag.listngay = list.Where(p => p.gh.NGAYTAO >= ngay);
                ViewBag.slngay = list.Where(p => p.gh.NGAYTAO >= ngay).Count();
                ViewBag.tienngay = list.Where(p => p.gh.THANHTOAN == true).Where(p => p.gh.NGAYTAO >= ngay).Sum(p => p.gh.THANHTIEN);
                ViewBag.spngay = tb_GIOHANG_SPC.Where(p => p.tb_GIOHANG.NGAYTAO >= ngay && p.tb_CUAHANG_SPCT.IDCUAHANG == id).Sum(p => p.SOLUONGSPCHINH);

                var listkhngay = list.Where(p => p.gh.TRANGTHAI == "Đã thanh toán" && p.gh.NGAYTAO >= ngay).GroupBy(p => p.gh.IDKHACHHANG);
                ViewBag.listkhngay = listkhngay.OrderByDescending(p => p.Sum(a => a.gh.THANHTIEN)).Take(5);

                ViewBag.listtuan = list.Where(p => p.gh.NGAYTAO >= tuan);
                ViewBag.sltuan = list.Where(p => p.gh.NGAYTAO >= tuan).Count();
                ViewBag.tientuan = list.Where(p => p.gh.THANHTOAN == true).Where(p => p.gh.NGAYTAO >= tuan).Sum(p => p.gh.THANHTIEN);
                ViewBag.sptuan = tb_GIOHANG_SPC.Where(p => p.tb_GIOHANG.NGAYTAO >= tuan && p.tb_CUAHANG_SPCT.IDCUAHANG == id).Sum(p => p.SOLUONGSPCHINH);

                var listkhtuan = list.Where(p => p.gh.TRANGTHAI == "Đã thanh toán" && p.gh.NGAYTAO >= tuan).GroupBy(p => p.gh.IDKHACHHANG);
                ViewBag.listkhtuan = listkhtuan.OrderByDescending(p => p.Sum(a => a.gh.THANHTIEN)).Take(5);

                ViewBag.listthang = list.Where(p => p.gh.NGAYTAO >= thang);
                ViewBag.slthang = list.Where(p => p.gh.NGAYTAO >= thang).Count();
                ViewBag.tienthang = list.Where(p => p.gh.THANHTOAN == true).Where(p => p.gh.NGAYTAO >= thang).Sum(p => p.gh.THANHTIEN);
                ViewBag.spthang = tb_GIOHANG_SPC.Where(p => p.tb_GIOHANG.NGAYTAO >= thang && p.tb_CUAHANG_SPCT.IDCUAHANG == id).Sum(p => p.SOLUONGSPCHINH);

                var listthang = list.Where(p => p.gh.TRANGTHAI == "Đã thanh toán" && p.gh.NGAYTAO >= thang).GroupBy(p => p.gh.IDKHACHHANG);
                ViewBag.listkhthang = listthang.OrderByDescending(p => p.Sum(a => a.gh.THANHTIEN)).Take(5);

                return View(list);
            }
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

        public ActionResult Thanhcong(int? id)
        {
            tb_GIOHANG tb_GIOHANG = db.tb_GIOHANG.Find(id);
            tb_GIOHANG.TRANGTHAI = "Đã thanh toán";
            tb_GIOHANG.THANHTOAN = true;
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
