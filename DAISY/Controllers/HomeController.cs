using DAISY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public ActionResult ToaDo(string vd, string kd)
        {
            Session["ViDo"] = vd;
            Session["KinhDo"] = kd;
            return RedirectToAction("Index");
        }

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public ActionResult Index(string str)
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            tb_CUAHANG cuahang = context.tb_CUAHANG.FirstOrDefault(p => p.IDUSER == id);
            AspNetUsers us = context.AspNetUsers.FirstOrDefault(p => p.Id == id);

            Session["soluong"] = TongSoLuong();

            if (user!= null)
            {
                Session["Name"] = user.Name;
                Session["Id"] = id;
                Session["role"] = Session["role1"];
                Session["Avatar"] = user.Image;
                Session["TaiKhoan"] = us;

                if (cuahang != null)
                {
                    Session["IdCuaHang"] = cuahang.IDCUAHANG;

                    var tb_GIOHANG = context.tb_GIOHANG.OrderByDescending(p => p.NGAYTAO).ToList();
                    var tb_GIOHANG_SPC = context.tb_GIOHANG_SPC.ToList();
                    var tb_CUAHANG_SPCT = context.tb_CUAHANG_SPCT.ToList();
                    int idch = (int)Session["IdCuaHang"];

                    var list = from e in tb_GIOHANG
                               join d in tb_GIOHANG_SPC on e.IDGIOHANG equals d.IDGIOHANG into table1
                               from d in table1.ToList()
                               join i in tb_CUAHANG_SPCT on d.IDSANPHAM equals i.ID into table2
                               from i in table2.ToList()
                               where d.tb_CUAHANG_SPCT.IDCUAHANG == idch
                               select new GioHang_CuaHang
                               {
                                   gh = e,
                                   ghct = d,
                                   ch = i
                               };
                    var listdoi = list.Where(p => p.gh.TRANGTHAI == "Chờ xử lý");
                    Session["listdoi"] = listdoi.Count();
                }
                    
            }

            var listdoi1 = context.tb_CUAHANG_SPCT.Where(p => p.CHODUYET == "Chờ duyệt").ToList();

            Session["doiduyet"] = listdoi1.Count();

            var listsp = context.tb_SANPHAM.Where(p => p.TRANGTHAI != "Tạm ngưng").Where(p => p.TENSANPHAM.Contains(str) || p.tb_LOAISANPHAM.TENLOAISANPHAM.Contains(str) || str == null).OrderBy(p => p.TENSANPHAM).ToList();

            var listDanhmuc = context.tb_LOAISANPHAM.Where(p => p.TRANGTHAI != "Tạm ngưng").Where(p => p.TENLOAISANPHAM.Contains(str) || str == null).OrderBy(p => p.TENLOAISANPHAM).ToList();
            ViewBag.Danhmuc = listDanhmuc;

            var listCuahang = context.tb_CUAHANG.Where(p => p.TRANGTHAI == "Đang hoạt động" && p.DINHCHI != true && p.XETDUYET == true).Where(p => p.TENCUAHANG.Contains(str) || str == null).OrderBy(p => p.TENCUAHANG).ToList();
            ViewBag.Cuahang = listCuahang;
            ViewBag.CountCuaHang = listCuahang.Count();

            Session["demch"] = context.tb_CUAHANG.OrderByDescending(p => p.IDCUAHANG).Where(p => p.XETDUYET == false).Count();


            var listsp_ch = context.tb_CUAHANG_SPCT.Where(p => p.TRANGTHAI == "Còn hàng").Where(p=> p.TENSANPHAM.Contains(str) || p.tb_SANPHAM.TENSANPHAM.Contains(str) || p.tb_CUAHANG.TENCUAHANG.Contains(str) || p.tb_SANPHAM.tb_LOAISANPHAM.TENLOAISANPHAM.Contains(str) || str == null).ToList();
            ViewBag.listsp_ch = listsp_ch;
            ViewBag.dem_listsp_ch = listsp_ch.Count();

            ViewBag.dem_listsp = listsp.Count();
            return View(listsp);
        }

        public ActionResult ViewDanhmuc()
        {
            var listDanhmuc = context.tb_LOAISANPHAM.Where(p=> p.TRANGTHAI == "Khả dụng").OrderBy(p => p.TENLOAISANPHAM).ToList();
            ViewBag.count = listDanhmuc.Count;
            return View(listDanhmuc);
        }

        public ActionResult SanPhamByDanhMuc(int id)
        {
            var listsp = context.tb_SANPHAM.Where(p=> p.IDLOAISANPHAM == id && p.TRANGTHAI == "Khả dụng").OrderBy(p => p.TENSANPHAM).ToList();
            var Danhmuc = context.tb_SANPHAM.FirstOrDefault(p => p.IDLOAISANPHAM == id);

            if (Danhmuc != null)
                ViewBag.Danhmuc = Danhmuc.tb_LOAISANPHAM.TENLOAISANPHAM;
            else
                ViewBag.Danhmuc = "Trở về";
            ViewBag.count = listsp.Count;
            return View(listsp);
        }

        public ActionResult SanPhamBySanPham(int id)
        {
            var listsp = context.tb_CUAHANG_SPCT.Where(p => p.IDSANPHAM == id && p.TRANGTHAI == "Còn hàng").OrderBy(p => p.TENSANPHAM).ToList();

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