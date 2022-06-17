using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAISY.Models
{
    public class UserRoles
    {
        public AspNetUsers aspNetUsers { get; set; }
        public AspNetUserRoles aspNetUserRoles { get; set; }
    }
}