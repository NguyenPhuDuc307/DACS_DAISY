namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_SANPHAM_SPDK
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDCUAHANG { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDSANPHAM { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDSPDK { get; set; }

        public virtual tb_CUAHANG tb_CUAHANG { get; set; }

        public virtual tb_SANPHAM tb_SANPHAM { get; set; }

        public virtual tb_SPDK tb_SPDK { get; set; }
    }
}
