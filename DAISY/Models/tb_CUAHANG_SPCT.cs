namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_CUAHANG_SPCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_CUAHANG_SPCT()
        {
            tb_GIOHANG_SPC = new HashSet<tb_GIOHANG_SPC>();
        }

        public int ID { get; set; }

        public int IDCUAHANG { get; set; }

        public int IDSANPHAM { get; set; }

        public int IDKICHCO { get; set; }

        [Required]
        [StringLength(256)]
        public string TENSANPHAM { get; set; }

        [Column(TypeName = "ntext")]
        public string MOTA { get; set; }

        [Required]
        [StringLength(128)]
        public string HINHANH { get; set; }

        public double GIASANPHAM { get; set; }

        [Required]
        [StringLength(64)]
        public string METATITLE { get; set; }

        [Required]
        [StringLength(128)]
        public string TRANGTHAI { get; set; }

        [StringLength(64)]
        public string CHODUYET { get; set; }

        public virtual tb_CUAHANG tb_CUAHANG { get; set; }

        public virtual tb_KICHCO tb_KICHCO { get; set; }

        public virtual tb_SANPHAM tb_SANPHAM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_GIOHANG_SPC> tb_GIOHANG_SPC { get; set; }
    }
}
