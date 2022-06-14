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
    public class ToppingController : Controller
    {
        private DaisyContext db = new DaisyContext();

        // GET: Topping
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/Topping/" + file.FileName));
            return "/Content/img/Topping/" + file.FileName;
        }
        public ActionResult Index()
        {
            return View(db.tb_SPDK.ToList());
        }

        // GET: Topping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SPDK tb_SPDK = db.tb_SPDK.Find(id);
            if (tb_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_SPDK);
        }

        // GET: Topping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSPDK,TENSPDK,GIABANSPDK,HINHANH")] tb_SPDK tb_SPDK)
        {
            if (ModelState.IsValid)
            {
                db.tb_SPDK.Add(tb_SPDK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_SPDK);
        }

        // GET: Topping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SPDK tb_SPDK = db.tb_SPDK.Find(id);
            if (tb_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_SPDK);
        }

        // POST: Topping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSPDK,TENSPDK,GIABANSPDK,HINHANH")] tb_SPDK tb_SPDK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SPDK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_SPDK);
        }

        // GET: Topping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_SPDK tb_SPDK = db.tb_SPDK.Find(id);
            if (tb_SPDK == null)
            {
                return HttpNotFound();
            }
            return View(tb_SPDK);
        }

        // POST: Topping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_SPDK tb_SPDK = db.tb_SPDK.Find(id);
            db.tb_SPDK.Remove(tb_SPDK);
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
