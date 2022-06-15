using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAISY.Models
{
    public class GioHang_Topping
    {
        [Key]
        public int IDGIOHANG { get; set; }

        [Key]
        public int IDSPDK { get; set; }

        public int IDSANPHAM { get; set; }

        public int SOLUONG { get; set; }

        public float GIABAN { get; set; }

        public float THANHTIEN { get; set; }

        public virtual Giohang Giohang { get; set; }

    }
}