namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TB_SANPHAM
    {
        public int ID { get; set; }

        public string TENSP { get; set; }

        [StringLength(50)]
        public string SIZE { get; set; }

        [StringLength(50)]
        public string HINH { get; set; }

        public decimal? GIABAN { get; set; }

        public double? KHUYENMAI { get; set; }

        public int? DANHGIA { get; set; }

        public List<TB_SANPHAM> lstsp = new List<TB_SANPHAM>();
    }
}
