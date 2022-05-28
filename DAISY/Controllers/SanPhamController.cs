using DAISY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAISY.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        DaisyContext context = new DaisyContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            TB_SANPHAM objSAMPHAM = new TB_SANPHAM();
            return View(objSAMPHAM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TB_SANPHAM objSANPHAM)
        {
            if (!ModelState.IsValid)
            {
                objSANPHAM.lstsp = context.TB_SANPHAM.ToList();
                return View("Create", objSANPHAM);
            }
            //lấy Login user ID
            //add vào CSDL
            context.TB_SANPHAM.Add(objSANPHAM);
            context.SaveChanges();
            //Trở về Home, Action Index
           return RedirectToAction("Index", "Home");
        }
    }
}