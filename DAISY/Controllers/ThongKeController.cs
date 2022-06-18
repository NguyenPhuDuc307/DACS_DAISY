using DAISY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAISY.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        DaisyContext context = new DaisyContext();
        public ActionResult Index()
        {

            DateTime ngay = DateTime.Now.Date;

            var tb_GIOHANG = context.tb_GIOHANG.OrderByDescending(p => p.NGAYTAO).ToList();
            var tb_GIOHANG_SPC = context.tb_GIOHANG_SPC.ToList();
            var tb_CUAHANG_SPCT = context.tb_CUAHANG_SPCT.ToList();

            var list = from e in tb_GIOHANG
                       join d in tb_GIOHANG_SPC on e.IDGIOHANG equals d.IDGIOHANG into table1
                       from d in table1.ToList()
                       join i in tb_CUAHANG_SPCT on d.IDSANPHAM equals i.ID into table2
                       from i in table2.ToList()
                       select new GioHang_CuaHang
                       {
                           gh = e,
                           ghct = d,
                           ch = i
                       };
            return View(list.Where(p=> p.gh.NGAYTAO >= ngay));
        }
    }
}