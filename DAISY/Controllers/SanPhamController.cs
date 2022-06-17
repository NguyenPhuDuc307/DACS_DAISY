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
    public class SanPhamController : Controller
    {
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/Sanpham/" + file.FileName));
            return "/Content/img/Sanpham/" + file.FileName;
        }

        private DaisyContext db = new DaisyContext();

        // GET: SanPham
        public ActionResult Index()
        {
            var tb_SANPHAM = db.tb_SANPHAM.Include(t => t.tb_LOAISANPHAM);
            return View(tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Quanly()
        {
            var tb_SANPHAM = db.tb_SANPHAM.Include(t => t.tb_LOAISANPHAM);
            return View(tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList());
        }

        // GET: SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);

            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM);
        }

        // GET: SanPham/Create
        public ActionResult Create()
        {
            ViewBag.listsp = db.tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList();
            ViewBag.IDLOAISANPHAM = new SelectList(db.tb_LOAISANPHAM, "IDLOAISANPHAM", "TENLOAISANPHAM");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSANPHAM,IDLOAISANPHAM,TENSANPHAM,HINHANH")] tb_SANPHAM tb_SANPHAM)
        {
            if (ModelState.IsValid)
            {
                tb_SANPHAM.TRANGTHAI = "Khả dụng";
                db.tb_SANPHAM.Add(tb_SANPHAM);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.listsp = db.tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList();
            ViewBag.IDLOAISANPHAM = new SelectList(db.tb_LOAISANPHAM, "IDLOAISANPHAM", "TENLOAISANPHAM", tb_SANPHAM.IDLOAISANPHAM);
            return View(tb_SANPHAM);
        }

        // GET: SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            List<string> ids = new List<string>();
            ids.Add("Khả dụng");
            ids.Add("Tạm ngưng");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_SANPHAM.TRANGTHAI);
            ViewBag.listsp = db.tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList();
            ViewBag.IDLOAISANPHAM = new SelectList(db.tb_LOAISANPHAM, "IDLOAISANPHAM", "TENLOAISANPHAM", tb_SANPHAM.IDLOAISANPHAM);
            return View(tb_SANPHAM);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSANPHAM,IDLOAISANPHAM,TENSANPHAM,HINHANH,TRANGTHAI")] tb_SANPHAM tb_SANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            List<string> ids = new List<string>();
            ids.Add("Khả dụng");
            ids.Add("Tạm ngưng");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_SANPHAM.TRANGTHAI);
            ViewBag.listsp = db.tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList();
            ViewBag.IDLOAISANPHAM = new SelectList(db.tb_LOAISANPHAM, "IDLOAISANPHAM", "TENLOAISANPHAM", tb_SANPHAM.IDLOAISANPHAM);
            return View(tb_SANPHAM);
        }

        // GET: SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            if (tb_SANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_SANPHAM);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_SANPHAM tb_SANPHAM = db.tb_SANPHAM.Find(id);
            db.tb_SANPHAM.Remove(tb_SANPHAM);
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
