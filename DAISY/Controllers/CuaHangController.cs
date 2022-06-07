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
        DaisyContext context = new DaisyContext();

        // GET: CuaHang
        public ActionResult Index()
        {
            return View(context.tb_CUAHANG.ToList());
        }

        // GET: CuaHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = context.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG);
        }

        // GET: CuaHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuaHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCUAHANG,TENCUAHANG,DIACHI,SODIENTHOAI,EMAIL")] tb_CUAHANG tb_CUAHANG)
        {
            if (ModelState.IsValid)
            {
                context.tb_CUAHANG.Add(tb_CUAHANG);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_CUAHANG);
        }

        // GET: CuaHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = context.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG);
        }

        // POST: CuaHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCUAHANG,TENCUAHANG,DIACHI,SODIENTHOAI,EMAIL")] tb_CUAHANG tb_CUAHANG)
        {
            if (ModelState.IsValid)
            {
                context.Entry(tb_CUAHANG).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_CUAHANG);
        }

        public ActionResult Delete(int id)
        {
            tb_CUAHANG ch = context.tb_CUAHANG.Find(id);
            context.tb_CUAHANG.Remove(ch);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
