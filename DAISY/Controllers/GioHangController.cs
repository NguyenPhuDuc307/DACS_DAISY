using DAISY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAISY.Payment.Momo;
using Newtonsoft.Json.Linq;

namespace DAISY.Controllers
{
    public class GioHangController : Controller
    {
        // Tạo biến kết nối đến Database
        DaisyContext data = new DaisyContext();

        // Lấy giỏ hàng từ Models.Giohang
        public List<Giohang> Laygiohang()
        {
            // Tao danh sách giỏ hàng
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;

            // nếu danh sách giỏ hàng trống: tạo một danh sách giỏ hàng trống
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }

            // trả về giỏ hàng
            return lstGiohang;
        }

        public JsonResult GetData(int id)
        {
            // This returned data is a sample. You should get it using some logic
            // This can be an object or an anonymous object like this:
            var returnedData = new
            {
                id,
                age = 23,
                name = "John Smith"
            };
            return Json(returnedData, JsonRequestBehavior.AllowGet);
        }



        // thêm sản phẩm vào giỏ hàng
        public ActionResult ThemGioHang(int id, string strURL)
        {

            // gọi danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // Tìm sản phẩm trong giỏ hàng
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);

            // nếu sản phẩm chưa có: thêm sản phẩm vào giỏ hàng
            // về lại trang đang dùng
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            // nếu sản phẩm đã tồn tại: tăng số lượng sản phẩm thêm 1 đơn vị
            // về lại trang đang dùng
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(strURL);
            }
        }

        // Mua ngay
        public ActionResult Muangay(int id, string strURL)
        {
            // gọi danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // Tìm sản phẩm trong giỏ hàng
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);

            // nếu sản phẩm chưa có: thêm sản phẩm vào giỏ hàng
            // về lại trang đang dùng
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            // nếu sản phẩm đã tồn tại: tăng số lượng sản phẩm thêm 1 đơn vị
            // dẫn đến trang giỏ hàng
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(@Url.Action("GioHang", "GioHang"));
            }
        }

        // đếm tổng số sản phẩm
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

        // đếm số lượng sản phẩm
        private int TongSoLuongSanPham()
        {
            // khởi tạo tổng số lượng sản phẩm
            int tsl = 0;
            
            // Lấy ra danh sách giỏ hàng
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            // nếu danh sách khác 0: gán tsl = đếm số lượng danh sách
            // trả về
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        // tính tổng tiền
        private double TongTien()
        {
            // tạo biến tính tổng tiền
            float tt = 0;

            // Lấy ra danh sách giỏ hàng
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            // nếu danh sách khác 0: gán tt = tổng tiền trong giỏ hàng
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(p => p.dThanhtien);
            }
            return tt;
        }

        private string ThanhTien()
        {
            // tạo biến tính tổng tiền
            float tt = 0;

            // Lấy ra danh sách giỏ hàng
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;

            // nếu danh sách khác 0: gán tt = tổng tiền trong giỏ hàng
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(p => p.dThanhtien);
            }
            return tt.ToString();
        }

        // load giỏ hàng
        public ActionResult GioHang()
        {
            //  lấy danh sách cửa hàng
            List<Giohang> lstGiohang = Laygiohang();

            // trả về tổng số lượng
            ViewBag.Tongsoluong = TongSoLuong();

            Session["soluong"] = TongSoLuong();

            // trả về tổng tiền
            ViewBag.Tongtien = TongTien();

            // trả về tổng số lượng sản phẩm
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            // đưa danh sách cửa hàng lên giao diện
            return View(lstGiohang);
        }

        public ActionResult GiohangPartial()
        {
            // trả về tổng số lượng
            ViewBag.Tongsoluong = TongSoLuong();

            // trả về tổng tiền
            ViewBag.Tongtien = TongTien();

            // trả về tổng số lượng sản phẩm
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            Session["soluong"] = TongSoLuong();

            return PartialView();
        }

        // xóa giỏ hàng
        public ActionResult XoaGiohang(int id)
        {
            // lấy danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // tìm sản phẩm theo idSP
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);

            // nếu sản phẩm tồn tai: Xóa tất cả sản phẩm có id = idSP
            // về trang giỏ hàng
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(p => p.idSP == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        // Cập nhật giỏ hàng
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            // lấy danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // tìm sản phẩm theo idSP
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);

            Session["soluong"] = TongSoLuong();

            // nếu sản phẩm tồn tai: Cập nhật số lượng từ FormCollection
            // về trang giỏ hàng
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohangJQ(int id, int sl, FormCollection collection)
        {
            // lấy danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // tìm sản phẩm theo idSP
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);

            Session["soluong"] = TongSoLuong();

            // nếu sản phẩm tồn tai: Cập nhật số lượng từ FormCollection
            // về trang giỏ hàng
            if (sanpham != null)
            {
                sanpham.iSoluong = sl;
            }
            return RedirectToAction("GioHang");
        }

        // Xóa tất cả giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            // lấy danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            Session["soluong"] = TongSoLuong();

            // xoá tất cả giỏ hàng
            // trả về trang giỏ hàng
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        // Đặt hàng
        public ActionResult DatHang()
        {
            // nếu chưa đăng nhập: đến trang đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }

            // nếu giỏ hàng trống: đến trang home
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Session["soluong"] = TongSoLuong();

            // lấy danh sách giỏ hàng
            List<Giohang> lstGiohang = Laygiohang();

            // trả về tổng số lượng
            ViewBag.Tongsoluong = TongSoLuong();

            // trả về tổng tiền
            ViewBag.Tongtien = TongTien();

            // trả về tổng số lượng sản phẩm
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();

            // đưa danh sách cửa hàng lên giao diện
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            // lấy ra giỏ hàng
            tb_GIOHANG dh = new tb_GIOHANG();

            Session["soluong"] = TongSoLuong();

            // lấy ra user
            AspNetUsers kh = (AspNetUsers)Session["TaiKhoan"];

            // lấy ra sản phẩm của cửa hàng
            tb_CUAHANG_SPCT sp = new tb_CUAHANG_SPCT();

            // lấy ra Models.Giohang
            List<Giohang> gh = Laygiohang();

            var httt = collection["httt"].ToString();


            // gán đây đủ dữ liệu cho giỏ hàng
            dh.IDKHACHHANG = kh.Id;
            dh.NGAYTAO = DateTime.Now;
            dh.TRANGTHAI = "Chờ xử lý";
            dh.THANHTOAN = false;
            dh.THANHTIEN = TongTien();
            float tt = (float)TongTien();

            string ttstr = tt.ToString();
            // thêm giỏ hàng vào cơ sở dữ liệu
            data.tb_GIOHANG.Add(dh);

            // lưu lại
            data.SaveChanges();

            string idgh = dh.IDGIOHANG.ToString();
            string khachhang = (string)Session["Name"];
            string ngaytao = dh.NGAYTAO.ToString();
            string noidung = idgh +" - " + khachhang + " - " + ngaytao;

            // vòng lặp thêm chi tiết sản phẩm vào cơ sở dữ liệu
            foreach (var item in gh)
            {
                // tạo ra đối tượng giỏ hàng sản phẩm chính
                tb_GIOHANG_SPC ghsp = new tb_GIOHANG_SPC();

                // thêm đầy đủ dữ liệu giỏ hàng sản phẩm chính
                ghsp.IDGIOHANG = dh.IDGIOHANG;
                ghsp.IDSANPHAM = item.idSP;
                ghsp.SOLUONGSPCHINH = item.iSoluong;
                ghsp.GIABAN = item.giaBan;
                ghsp.THANHTIEN = (float)TongTien();

                sp = data.tb_CUAHANG_SPCT.Single(n => n.ID == item.idSP);

                // thêm vào danh sách sản phẩm chi tiết
                data.tb_GIOHANG_SPC.Add(ghsp);
            }

            // lưu vào cơ sở dữ liệu
            data.SaveChanges();

            // gán Giỏ hàng =  rỗng
            Session["Giohang"] = null;

            

            if(httt == "1")
            {
                // đến trang xác nhận
                return RedirectToAction("XacnhanDonhang", "GioHang");
            }
            else
            {
                return RedirectToAction("Payment", new { @sotien = ttstr, @noidung = noidung });
            }
        }

        public ActionResult Index()
        {
            Session["soluong"] = TongSoLuong();
            return View();

        }

        public ActionResult Payment(string sotien, string noidung)
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJGX20220612";
            string accessKey = "G8bunXEtTKh8TVcx";
            string serectkey = "ET8pMcrAwvZqkTFfghVu3f3kv0k9cyd7";
            string orderInfo = noidung;
            string returnUrl = "https://localhost:44396/GioHang/ConfirmPaymentClient";
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = sotien;
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
        }


        public ActionResult Thanhtoan()
        {
            return View();
        }

        // trang xác nhận đặt hàng thành công
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