using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAISY.Helper;
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
            int id=0;
            if(Session["IdCuaHang"] != null)
            {
                id = (int)Session["IdCuaHang"];
            }
            var tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Include(t => t.tb_CUAHANG).Include(t => t.tb_KICHCO).Include(t => t.tb_SANPHAM);
            return View(tb_CUAHANG_SPCT.Include(p=>p.tb_CUAHANG).Where(p=> p.tb_CUAHANG.IDCUAHANG == id).ToList());
        }

        public ActionResult Quanly()
        {
            var listdoi = db.tb_CUAHANG_SPCT.Where(p => p.CHODUYET == "Chờ duyệt").ToList();

            Session["doiduyet"] = listdoi.Count();
            var tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Include(t => t.tb_CUAHANG).Include(t => t.tb_KICHCO).Include(t => t.tb_SANPHAM);
            return View(tb_CUAHANG_SPCT.Include(p => p.tb_CUAHANG).OrderBy(p=>p.CHODUYET).ToList());
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

        public string KiemtraTitle(string title, FormCollection collection)
        {
            string ktra = "Chưa tồn tại";

            // tìm loai sản phẩm theo idSP
            tb_CUAHANG_SPCT sp = db.tb_CUAHANG_SPCT.FirstOrDefault(p => p.METATITLE == title);

            if (sp != null) { ktra = "Tồn tại"; }
            return ktra;
        }

        public string KiemtraTitleEdit(string title, string tt, FormCollection collection)
        {
            string ktra = "Chưa tồn tại";

            // tìm loai sản phẩm theo idSP
            tb_CUAHANG_SPCT sp = db.tb_CUAHANG_SPCT.FirstOrDefault(p => p.METATITLE == title);



            if (sp != null)
            {

                if (sp.METATITLE == tt)
                {
                    ktra = "Chưa tồn tại";
                }
                else
                {
                    ktra = "Tồn tại";
                }
            }
            return ktra;
        }

        // GET: CuaHang_SanPham/Create
        public ActionResult Create()
        {
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "IDUSER");
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO");
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM");
            return View();
        }

        // POST: CuaHang_SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCUAHANG,IDSANPHAM,IDKICHCO,TENSANPHAM,MOTA,HINHANH,GIASANPHAM,METATITLE,TRANGTHAI,CHODUYET")] tb_CUAHANG_SPCT tb_CUAHANG_SPCT)
        {
            if (ModelState.IsValid)
            {
                db.tb_CUAHANG_SPCT.Add(tb_CUAHANG_SPCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "IDUSER", tb_CUAHANG_SPCT.IDCUAHANG);
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO", tb_CUAHANG_SPCT.IDKICHCO);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM", tb_CUAHANG_SPCT.IDSANPHAM);
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
            List<string> ids = new List<string>();
            ids.Add("Còn hàng");
            ids.Add("Tạm ngưng bán");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_CUAHANG_SPCT.TRANGTHAI);
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "IDUSER", tb_CUAHANG_SPCT.IDCUAHANG);
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO", tb_CUAHANG_SPCT.IDKICHCO);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM", tb_CUAHANG_SPCT.IDSANPHAM);
            return View(tb_CUAHANG_SPCT);
        }

        // POST: CuaHang_SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCUAHANG,IDSANPHAM,IDKICHCO,TENSANPHAM,MOTA,HINHANH,GIASANPHAM,METATITLE,TRANGTHAI,CHODUYET")] tb_CUAHANG_SPCT tb_CUAHANG_SPCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_CUAHANG_SPCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<string> ids = new List<string>();
            ids.Add("Còn hàng");
            ids.Add("Tạm ngưng bán");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_CUAHANG_SPCT.TRANGTHAI);
            ViewBag.IDCUAHANG = new SelectList(db.tb_CUAHANG, "IDCUAHANG", "IDUSER", tb_CUAHANG_SPCT.IDCUAHANG);
            ViewBag.IDKICHCO = new SelectList(db.tb_KICHCO, "IDKICHCO", "TENKICHCO", tb_CUAHANG_SPCT.IDKICHCO);
            ViewBag.IDSANPHAM = new SelectList(db.tb_SANPHAM, "IDSANPHAM", "TENSANPHAM", tb_CUAHANG_SPCT.IDSANPHAM);
            return View(tb_CUAHANG_SPCT);
        }
        /// <summary>
        /// //////////////////////////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Ngung(int? id)
        {
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);

            if (tb_CUAHANG_SPCT.TRANGTHAI == "Tạm ngưng bán")
            {
                tb_CUAHANG_SPCT.TRANGTHAI = "Còn hàng";
                db.Entry(tb_CUAHANG_SPCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                tb_CUAHANG_SPCT.TRANGTHAI = "Tạm ngưng bán";
                db.Entry(tb_CUAHANG_SPCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Tuchoi(int? id)
        {
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);
            db.tb_CUAHANG_SPCT.Remove(tb_CUAHANG_SPCT);
            db.SaveChanges();

            SendMail.SendEmail(tb_CUAHANG_SPCT.tb_CUAHANG.AspNetUsers.Email, "DAISY - Sản phẩm của bạn bị từ chối đăng bài.", "Chúng tôi nhận thấy sản phẩm của bạn không đúng mô tả hoặc không phù hợp<br/>" +
                "Chúng tôi quyết định từ chối bài đăng. Trân trọng!", "");

            return RedirectToAction("Quanly");
        }

        public ActionResult Duyet(int? id)
        {
            tb_CUAHANG_SPCT tb_CUAHANG_SPCT = db.tb_CUAHANG_SPCT.Find(id);

                tb_CUAHANG_SPCT.CHODUYET = "Đã duyệt";
                db.Entry(tb_CUAHANG_SPCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Quanly");
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
