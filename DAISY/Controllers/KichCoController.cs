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
    public class KichCoController : Controller
    {
        DaisyContext context = new DaisyContext();

        // GET: KichCo
        public ActionResult Kichco()
        {
            return View(context.tb_KICHCO.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KICHCO tb_KICHCO = context.tb_KICHCO.Find(id);
            if (tb_KICHCO == null)
            {
                return HttpNotFound();
            }
            return View(tb_KICHCO);
        }

        // GET: KichCo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KichCo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKICHCO,TENKICHCO,THETICH_TRONGLUONG")] tb_KICHCO tb_KICHCO)
        {
            if (ModelState.IsValid)
            {
                context.tb_KICHCO.Add(tb_KICHCO);
                context.SaveChanges();
                return RedirectToAction("Kichco");
            }

            return View(tb_KICHCO);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_KICHCO kichco = context.tb_KICHCO.Find(id);
            if (kichco == null)
            {
                return HttpNotFound();
            }
            return View(kichco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKICHCO,TENKICHCO,THETICH_TRONGLUONG")] tb_KICHCO tb_KICHCO)
        {
            if (ModelState.IsValid)
            {
                context.Entry(tb_KICHCO).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Kichco");
            }
            return View(tb_KICHCO);
        }

        public ActionResult Delete(int id)
        {
            tb_KICHCO kichco = context.tb_KICHCO.Find(id);
            context.tb_KICHCO.Remove(kichco);
            context.SaveChanges();
            return RedirectToAction("Kichco");
        }
    }
}
