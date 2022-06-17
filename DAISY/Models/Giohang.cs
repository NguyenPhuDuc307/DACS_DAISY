using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAISY.Models
{
    public class Giohang
    {
        DaisyContext data = new DaisyContext();

        public Giohang(int id)
        {
            idSP = id;
            tb_CUAHANG_SPCT sanpham = data.tb_CUAHANG_SPCT.Single(p => p.ID == idSP);
            tenSP = sanpham.TENSANPHAM;
            hinh = sanpham.HINHANH;
            giaBan = float.Parse(sanpham.GIASANPHAM.ToString());
            iSoluong = 1;
        }

        [Display(Name = "ID sản phẩm")]
        public int idSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string tenSP { get; set; }

        [Display(Name = "Hình ảnh")]
        public string hinh { get; set; }

        [Display(Name = "Giá bán")]
        public float giaBan { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }

        [Display(Name = "Thành tiền")]
        public float dThanhtien
        {
            get { return iSoluong * giaBan; }
        }
    }
}