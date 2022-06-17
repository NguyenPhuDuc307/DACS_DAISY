namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_GIOHANG_SPC
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDGIOHANG { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDSANPHAM { get; set; }

        public int SOLUONGSPCHINH { get; set; }

        public double GIABAN { get; set; }

        public double THANHTIEN { get; set; }

        public virtual tb_CUAHANG_SPCT tb_CUAHANG_SPCT { get; set; }

        public virtual tb_GIOHANG tb_GIOHANG { get; set; }
    }
}
