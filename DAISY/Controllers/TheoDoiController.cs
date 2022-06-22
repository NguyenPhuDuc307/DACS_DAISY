using DAISY.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DAISY.Controllers
{
    public class TheoDoiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follow(tb_THEODOI follow)
        {
            //user login là người theo dõi, follow.FolloweeId là người được theo dõi
            var userID = User.Identity.GetUserId();
            if (userID == null)
                return BadRequest("Please login first!");
            DaisyContext context = new DaisyContext();
            //kiểm tra xem mã userID đã được theo dõi chưa
            tb_THEODOI find = context.tb_THEODOI.FirstOrDefault(p => p.IdUser ==
            userID && p.IdCuahang == follow.IdCuahang);
            if (find != null)
            {
                // return BadRequest("The already following exists!");
                context.tb_THEODOI.Remove(context.tb_THEODOI.SingleOrDefault(p =>
                p.IdUser == userID && p.IdCuahang == follow.IdCuahang));
                context.SaveChanges();
                return Ok("cancel");
            }
            //set object follow
            follow.IdUser = userID;
            context.tb_THEODOI.Add(follow);
            context.SaveChanges();
            return Ok();
        }
    }
}
