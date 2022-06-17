using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAISY.Helper;
using DAISY.Models;

namespace DAISY.Controllers
{
    public class CuaHangController : Controller
    {
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/Cuahang/" + file.FileName));
            return "/Content/img/Cuahang/" + file.FileName;
        }

        private DaisyContext db = new DaisyContext();

        // GET: CuaHang
        public ActionResult Index()
        {
            var tb_CUAHANG = db.tb_CUAHANG.Include(t => t.AspNetUsers);
            return View(tb_CUAHANG.ToList());
        }

        // GET: CuaHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG);
        }

        public ActionResult Thongtin()
        {
            if(Session["IdCuaHang"] == null)
            {
                return HttpNotFound();
            }
            else
            {
                int id = (int)Session["IdCuaHang"];

                tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
                if (tb_CUAHANG == null)
                {
                    return HttpNotFound();
                }
                return View(tb_CUAHANG);
            }
        }

        // GET: CuaHang/Create
        public ActionResult Create()
        {
            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: CuaHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCUAHANG,IDUSER,TENCUAHANG,DIACHI,HINHANH")] tb_CUAHANG tb_CUAHANG)
        {
            if (ModelState.IsValid)
            {
                tb_CUAHANG.UYTIN = false;
                tb_CUAHANG.DINHCHI = false;
                db.tb_CUAHANG.Add(tb_CUAHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email", tb_CUAHANG.IDUSER);
            return View(tb_CUAHANG);
        }

        // GET: CuaHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            List<string> ids = new List<string>();
            ids.Add("Đang hoạt động");
            ids.Add("Tạm ngưng hoạt động");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_CUAHANG.TRANGTHAI);
            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email", tb_CUAHANG.IDUSER);
            return View(tb_CUAHANG);
        }

        // POST: CuaHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCUAHANG,IDUSER,TENCUAHANG,DIACHI,HINHANH, GOOGLEMAP,TRANGTHAI")] tb_CUAHANG tb_CUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_CUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Thongtin");
            }
            List<string> ids = new List<string>();
            ids.Add("Đang hoạt động");
            ids.Add("Tạm ngưng hoạt động");
            ViewBag.TRANGTHAI = new SelectList(ids, tb_CUAHANG.TRANGTHAI);
            ViewBag.IDUSER = new SelectList(db.AspNetUsers, "Id", "Email", tb_CUAHANG.IDUSER);
            return View(tb_CUAHANG);
        }

        public ActionResult Uytin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }

            if(tb_CUAHANG.UYTIN == true)
            {
                ViewBag.mess = "Bạn có chắc chắn muốn cấp huy hiệu gian hàng tích cực cho cửa hàng này?";
            }
            else
            {
                ViewBag.mess = "Bạn có chắc chắn muốn hủy bỏ huy hiệu gian hàng tích cực của cửa hàng này?";
            }

            return View(tb_CUAHANG);
        }

        [HttpPost, ActionName("Uytin")]
        [ValidateAntiForgeryToken]
        public ActionResult UytinConfirmed(int id)
        {
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);

            if (tb_CUAHANG.UYTIN == true)
            {
                tb_CUAHANG.UYTIN = false;
                db.Entry(tb_CUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                SendMail.SendEmail(tb_CUAHANG.AspNetUsers.Email, "DAISY - Gian hàng của bạn đã bị xóa huy hiệu.", "Cửa hàng của bạn hiện không đủ tiêu chuẩn để cấp huy hiệu tích cực<br/>" +
                    "Chúng tôi quyết định thu hồi huy hiệu của bạn. Trân trọng!", "");
            }
            else
            {
                tb_CUAHANG.UYTIN = true;
                db.Entry(tb_CUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                SendMail.SendEmail(tb_CUAHANG.AspNetUsers.Email, "DAISY - Xin chúc mừng gian hàng của bạn đã có huy hiệu.", "Qua quá trình kiểm tra và theo dõi, chúng tôi nhận thấy gian hàng của bạn có những đóng góp tích cực trong hoạt động bán hàng <br/>" +
                    "Chúng tôi quyết định cấp cho bạn huy hiệu GIAN HÀNG CHÍNH HÃNG để trang bán hàng của bạn dễ dàng nhận diện hơn.", "");
            }

            
            return RedirectToAction("Details", new {@id = id });
        }

        public ActionResult Dinhchi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }

            if (tb_CUAHANG.DINHCHI == true)
            {
                ViewBag.mess = "Bạn có chắc chắn muốn cho phép cửa hàng tiếp tục hoạt động?";
            }
            else
            {
                ViewBag.mess = "Bạn có chắc chắn muốn đình chỉ hoạt động của cửa hàng này?";
            }

            return View(tb_CUAHANG);
        }

        [HttpPost, ActionName("Dinhchi")]
        [ValidateAntiForgeryToken]
        public ActionResult DinhchiConfirmed(int id)
        {
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);

            if (tb_CUAHANG.DINHCHI == true)
            {
                tb_CUAHANG.DINHCHI = false;
                tb_CUAHANG.TRANGTHAI = "Đang hoạt động";
                db.Entry(tb_CUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                SendMail.SendEmail(tb_CUAHANG.AspNetUsers.Email, "DAISY - Chúc mừng cửa hàng của bạn đã được tiếp tục hoạt động.", "Chúng tôi đã cho phép cửa hàng của bạn được tiếp tục bán hàng. Trân trọng!", "");
            }
            else
            {
                tb_CUAHANG.DINHCHI = true;
                tb_CUAHANG.TRANGTHAI = "Tạm ngưng hoạt động";
                db.Entry(tb_CUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                SendMail.SendEmail(tb_CUAHANG.AspNetUsers.Email, "DAISY - Cửa hàng bạn tạm đình chỉ.", "Qua quá trình kiểm tra và theo dõi, chúng tôi nhận thấy gian hàng của bạn có vấn đề trong hoạt động bán hàng <br/>" +
                    "Chúng tôi quyết định tạm đình chỉ hoạt động của cửa hàng bạn để tiếp tục giải quyết.<br/>" +
                    "Mọi thắc mắc vui lòng liên hệ với chúng tôi theo địa chỉ nguyenphuduc62001@icloud.com<br/>" +
                    "Chúng tôi rất tiếc về điều này. Trân trọng!", "");
            }

            
            return RedirectToAction("Details", new { @id = id });
        }


        // GET: CuaHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            if (tb_CUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tb_CUAHANG);
        }

        // POST: CuaHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_CUAHANG tb_CUAHANG = db.tb_CUAHANG.Find(id);
            db.tb_CUAHANG.Remove(tb_CUAHANG);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
