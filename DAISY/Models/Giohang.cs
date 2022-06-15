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

        [Key]
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

        public virtual ICollection<GioHang_Topping> GioHang_Toppings { get; set; }

        public Giohang(int id)
        {
            GioHang_Toppings = new List<GioHang_Topping>();
            idSP = id;
            tb_CUAHANG_SPCT sanpham = data.tb_CUAHANG_SPCT.Single(p => p.ID == idSP);
            tenSP = sanpham.TENSANPHAM;
            hinh = sanpham.HINHANH;
            giaBan = float.Parse(sanpham.GIASANPHAM.ToString());
            iSoluong = 1;
        }
    }
}