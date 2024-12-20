﻿using System;
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
    public class LoaiSanPhamController : Controller
    {

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/Danhmuc/" + file.FileName));
            return "/Content/img/danhmuc/" + file.FileName;
        }

        private DaisyContext db = new DaisyContext();

        // GET: LoaiSanPham
        public ActionResult Index()
        {
            return View(db.tb_LOAISANPHAM.OrderBy(p => p.TENLOAISANPHAM).ToList());
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Quanly()
        {
            return View(db.tb_LOAISANPHAM.OrderByDescending(p => p.TRANGTHAI).ToList());
        }

        // GET: LoaiSanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISANPHAM tb_LOAISANPHAM = db.tb_LOAISANPHAM.Find(id);
            if (tb_LOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_LOAISANPHAM);
        }

        public string KiemtraTitle(string title, FormCollection collection)
        {
            string ktra = "Chưa tồn tại";

            // tìm loai sản phẩm theo idSP
            tb_LOAISANPHAM sp = db.tb_LOAISANPHAM.FirstOrDefault(p => p.METATITLE == title);

            if (sp != null) { ktra = "Tồn tại"; }
            return ktra;
        }

        public string KiemtraTitleEdit(string title, string tt, FormCollection collection)
        {
            string ktra = "Chưa tồn tại";

            // tìm loai sản phẩm theo idSP
            tb_LOAISANPHAM sp = db.tb_LOAISANPHAM.FirstOrDefault(p => p.METATITLE == title);



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

        // GET: LoaiSanPham/Create
        public ActionResult Create()
        {
            ViewBag.listlsp = db.tb_LOAISANPHAM.OrderByDescending(p => p.TRANGTHAI).ToList();
            return View();
        }

        // POST: LoaiSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLOAISANPHAM,TENLOAISANPHAM,HINHANH,METATITLE")] tb_LOAISANPHAM tb_LOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                tb_LOAISANPHAM.TRANGTHAI = "Khả dụng";
                db.tb_LOAISANPHAM.Add(tb_LOAISANPHAM);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.listlsp = db.tb_LOAISANPHAM.OrderByDescending(p => p.TRANGTHAI).ToList();
            return View(tb_LOAISANPHAM);
        }

        // GET: LoaiSanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISANPHAM tb_LOAISANPHAM = db.tb_LOAISANPHAM.Find(id);
            if (tb_LOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            List<string> ids = new List<string>();
            ids.Add("Khả dụng");
            ids.Add("Tạm ngưng");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_LOAISANPHAM.TRANGTHAI);
            ViewBag.listlsp = db.tb_LOAISANPHAM.OrderByDescending(p => p.TRANGTHAI).ToList();
            return View(tb_LOAISANPHAM);
        }

        // POST: LoaiSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLOAISANPHAM,TENLOAISANPHAM,HINHANH,METATITLE,TRANGTHAI")] tb_LOAISANPHAM tb_LOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_LOAISANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }

            List<string> ids = new List<string>();
            ids.Add("Khả dụng");
            ids.Add("Tạm ngưng");
            ViewBag.TRANGTHAI = ids;
            ViewBag.listlsp = db.tb_LOAISANPHAM.OrderByDescending(p => p.TRANGTHAI).ToList();
            return View(tb_LOAISANPHAM);
        }

        // GET: LoaiSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISANPHAM tb_LOAISANPHAM = db.tb_LOAISANPHAM.Find(id);
            if (tb_LOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(tb_LOAISANPHAM);
        }

        // POST: LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_LOAISANPHAM tb_LOAISANPHAM = db.tb_LOAISANPHAM.Find(id);
            db.tb_LOAISANPHAM.Remove(tb_LOAISANPHAM);
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
