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

        public int TongSoLuong()
        {
            // khởi tạo tổng số sản phẩm
            int tsl = 0;

            // Lấy ra danh sách giỏ hàng
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            // nếu danh sách khác 0: gán tsl = tính tổng số lượng danh sách
            // trả về
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(p => p.iSoluong);
            }
            return tsl;
        }

        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            tb_CUAHANG cuahang = context.tb_CUAHANG.FirstOrDefault(p => p.IDUSER == id);
            AspNetUsers us = context.AspNetUsers.FirstOrDefault(p => p.Id == id);

            Session["soluong"] = TongSoLuong();

            if (user!= null)
            {
                Session["Name"] = user.Name;
                Session["ViDo"] = us.ToaDo_VD;
                Session["KinhDo"] = us.ToaDo_KD;

                Session["TaiKhoan"] = us;

                if (cuahang != null)
                {
                    Session["IdCuaHang"] = cuahang.IDCUAHANG;
                }
                    
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

        public ActionResult SanPhamBySanPham(int id)
        {
            var listsp = context.tb_CUAHANG_SPCT.Where(p => p.IDSANPHAM == id).OrderBy(p => p.TENSANPHAM).ToList();

            List<string> list = new List<string>();

            foreach (var item in listsp)
            {
                list.Add(item.tb_CUAHANG.AspNetUsers.ToaDo_VD.ToString());
            }

            ViewBag.listvd = list;
            Session["soluong"] = TongSoLuong();

            var Danhmuc = context.tb_SANPHAM.FirstOrDefault(p => p.IDLOAISANPHAM == id);

            ViewBag.dem = listsp.Count;

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