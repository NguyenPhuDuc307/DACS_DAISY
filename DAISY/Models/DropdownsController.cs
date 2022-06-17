using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAISY.Models
{
    public class DropdownsController : Controller
    {
        // GET: Dropdowns

        private DaisyContext context = new DaisyContext();
        public ActionResult Index()
        {
            ViewBag.LoaiSP = context.tb_LOAISANPHAM.ToList();
            return View();
        }

        public ActionResult loadSP (int idloaisp)
        {
            return Json(context.tb_SANPHAM.Where(p => p.IDLOAISANPHAM == idloaisp).Select(s => new
            {
                Id = s.IDLOAISANPHAM,
                Name = s.TENSANPHAM
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}