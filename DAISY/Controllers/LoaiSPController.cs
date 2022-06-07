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
    public class LoaiSPController : Controller
    {
        DaisyContext context = new DaisyContext();

        // GET: LoaiSP
        public ActionResult LoaiSP()
        {
            var lsp = context.tb_LOAISANPHAM.ToList();
            return View(lsp);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_LOAISANPHAM c_lsp)
        {
            if (!ModelState.IsValid)
            {
                c_lsp.lstlsp = context.tb_LOAISANPHAM.ToList();
                return View("Create", c_lsp);
            }
            context.tb_LOAISANPHAM.Add(c_lsp);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int?id)
        {
            var d_lsp = context.tb_LOAISANPHAM.Where(m => m.IDLOAISANPHAM == id).First();
            return View(d_lsp);
        }

        // GET: LoaiSP/Delete
        public ActionResult Delete(int id)
        {
            tb_LOAISANPHAM tb_LOAISANPHAM = context.tb_LOAISANPHAM.Find(id);
            context.tb_LOAISANPHAM.Remove(tb_LOAISANPHAM);
            context.SaveChanges();
            return RedirectToAction("LoaiSP");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_LOAISANPHAM lsp = context.tb_LOAISANPHAM.Find(id);
            if (lsp == null)
            {
                return HttpNotFound();
            }
            return View(lsp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLOAISANPHAM,TENLOAISANPHAM,HINHANH")] tb_LOAISANPHAM elsp)
        {
            if (ModelState.IsValid)
            {
                context.Entry(elsp).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("LoaiSP");
            }
            return View(elsp);
        }
    }
}
