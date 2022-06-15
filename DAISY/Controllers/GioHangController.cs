using DAISY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAISY.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        DaisyContext data = new DaisyContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(strURL);
            }
        }

        public ActionResult Muangay(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(@Url.Action("GioHang", "GioHang"));
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(p => p.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(p => p.dThanhtien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }

        public ActionResult XoaGiohang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(p => p.idSP == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            tb_GIOHANG dh = new tb_GIOHANG();
            AspNetUsers kh = (AspNetUsers)Session["TaiKhoan"];
            tb_CUAHANG_SPCT sp = new tb_CUAHANG_SPCT();

            List<Giohang> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["ngayGiao"]);

            dh.IDKHACHHANG = kh.Id;
            dh.NGAYTAO = DateTime.Now;
            dh.TRANGTHAI = "Chờ xử lý";
            dh.THANHTOAN = false;

            data.tb_GIOHANG.Add(dh);
            data.SaveChanges();

            foreach (var item in gh)
            {
                tb_GIOHANG_SPC ghsp = new tb_GIOHANG_SPC();
                ghsp.IDGIOHANG = dh.IDGIOHANG;
                ghsp.IDSANPHAM = item.idSP;
                ghsp.SOLUONGSPCHINH = item.iSoluong;
                ghsp.GIABAN = item.giaBan;
                ghsp.THANHTIEN = (float)TongTien();
                sp = data.tb_CUAHANG_SPCT.Single(n => n.ID == item.idSP);

                data.tb_GIOHANG_SPC.Add(ghsp);
            }
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacnhanDonhang", "GioHang");
        }

        public ActionResult Thanhtoan()
        {
            return View();
        }

        public ActionResult XacnhanDonhang()
        {
            AspNetUsers tk = (AspNetUsers)Session["Taikhoan"];
            Session["htThanhToan"] = tk.Name;
            Session["sdtThanhToan"] = tk.PhoneNumber;
            Session["timeThanhToan"] = DateTime.Now.Date;
            return View();
        }


    }
}