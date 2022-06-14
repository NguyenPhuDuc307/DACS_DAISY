using DAISY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DAISY.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        DaisyContext context = new DaisyContext();

        public string Roles { get; private set; }

        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            tb_CUAHANG cuahang = context.tb_CUAHANG.FirstOrDefault(p => p.IDUSER == id);

            if (user!= null)
            {
                Session["Name"] = user.Name;
                if(cuahang!=null)
                    Session["IdCuaHang"] = cuahang.IDCUAHANG;
            }

            var listsp = context.tb_SANPHAM.OrderBy(p => p.TENSANPHAM).ToList();

            var listDanhmuc = context.tb_LOAISANPHAM.OrderBy(p => p.TENLOAISANPHAM).ToList();
            ViewBag.Danhmuc = listDanhmuc;

            var listCuahang = context.tb_CUAHANG.OrderBy(p => p.TENCUAHANG).ToList();
            ViewBag.Cuahang = listCuahang;
            
            return View(listsp);
        }

        public ActionResult ViewDanhmuc()
        {
            var listDanhmuc = context.tb_LOAISANPHAM.OrderBy(p => p.TENLOAISANPHAM).ToList();
            return View(listDanhmuc);
        }

        public ActionResult SanPhamByDanhMuc(int id)
        {
            var listsp = context.tb_SANPHAM.Where(p=> p.IDLOAISANPHAM == id).OrderBy(p => p.TENSANPHAM).ToList();
            var Danhmuc = context.tb_SANPHAM.FirstOrDefault(p => p.IDLOAISANPHAM == id);

            if (Danhmuc != null)
                ViewBag.Danhmuc = Danhmuc.tb_LOAISANPHAM.TENLOAISANPHAM;
            else
                ViewBag.Danhmuc = "Trở về";
            return View(listsp);
        }

        [CustomAuthorize(Roles = "Admin")]//user 1
        public ActionResult Quanly()
        {
            if (Roles == null)
            {

            }

            ViewBag.Message = "Chào mừng bạn đến với Quản lý Website.";

            return View();
        }

        [CustomAuthorize(Roles = "Cửa hàng")]//user 2
        //[Authorize(Roles = "Cửa hàng, Admin")]//user 1,2
        public ActionResult Cuahang()
        {
            ViewBag.Message = "Chào mừng bạn đến với Quản lý Cửa Hàng.";

            return View();
        }
        [CustomAuthorize(Roles = "Khách hàng")]//user 3
        public ActionResult Khachhang()
        {
            ViewBag.Message = "Chào mừng bạn đến với Quản lý giỏ hàng.";

            return View();
        }

    }
}