using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAISY.Models
{
    public class QuanLyAdminController : Controller
    {

        private DaisyContext db = new DaisyContext();

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.listlsp = db.tb_LOAISANPHAM.OrderByDescending(p => p.TRANGTHAI).ToList();

            ViewBag.listsp = db.tb_SANPHAM.OrderBy(p => p.tb_LOAISANPHAM.TENLOAISANPHAM).ToList();

            ViewBag.listkc = db.tb_KICHCO.OrderBy(p => p.TENKICHCO).ToList();

            ViewBag.listch = db.tb_CUAHANG.OrderByDescending(p => p.IDCUAHANG).OrderBy(p=> p.XETDUYET).ToList();
            Session["demch"] = db.tb_CUAHANG.OrderByDescending(p => p.IDCUAHANG).Where(p=> p.XETDUYET == false).Count();

            var listnd = db.AspNetUsers.OrderBy(p => p.Name).ToList();

            var listndct = db.AspNetUserRoles.OrderBy(p => p.UserId).ToList();

            var listall = from e in listnd

                          join d in listndct on e.Id equals d.UserId into table1
                          from d in table1.ToList()
                          select new UserRoles
                          {
                              aspNetUsers = e,
                              aspNetUserRoles = d
                          };

            ViewBag.listnd = listall.Where(p=> p.aspNetUserRoles.RoleId != "8e686074-e1e0-4052-880b-220ce21dc4b7").ToList();
            return View();
        }
    }
}