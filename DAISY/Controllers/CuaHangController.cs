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
    public class CuaHangController : Controller
    {
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/Cuahang/" + file.FileName));
            return "/Content/img/Cuahang/" + file.FileName;
        }

        private DaisyContext db = new DaisyContext();

        // GET: CuaHang
        public ActionResult Index()
        {
            var tb_CUAHANG = db.tb_CUAHANG.Include(t => t.AspNetUsers);
            return View(tb_CUAHANG.ToList());
        }

        // GET: CuaHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG);
        }

        // GET: CuaHang/Create
        public ActionResult Create()
        {
            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: CuaHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCUAHANG,IDUSER,TENCUAHANG,DIACHI,HINHANH")] tb_CUAHANG tb_CUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.tb_CUAHANG.Add(tb_CUAHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email", tb_CUAHANG.IDUSER);
            return View(tb_CUAHANG);
        }

        // GET: CuaHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email", tb_CUAHANG.IDUSER);
            return View(tb_CUAHANG);
        }

        // POST: CuaHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCUAHANG,IDUSER,TENCUAHANG,DIACHI,HINHANH")] tb_CUAHANG tb_CUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_CUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email", tb_CUAHANG.IDUSER);
            return View(tb_CUAHANG);
        }

        // GET: CuaHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG);
        }

        // POST: CuaHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            db.tb_CUAHANG.Remove(tb_CUAHANG);
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
