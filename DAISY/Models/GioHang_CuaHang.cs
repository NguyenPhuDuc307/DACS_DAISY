using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAISY.Models
{
    public class GioHang_CuaHang
    {
        public tb_GIOHANG gh { get; set; }
        public tb_GIOHANG_SPC ghct { get; set; }
        public tb_CUAHANG_SPCT ch { get; set; }
    }
}