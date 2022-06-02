namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_CUAHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_CUAHANG()
        {
            tb_CUAHANG_SANPHAM = new HashSet<tb_CUAHANG_SANPHAM>();
            tb_CUAHANG_SPDK = new HashSet<tb_CUAHANG_SPDK>();
            tb_CUAHANG_SPCT = new HashSet<tb_CUAHANG_SPCT>();
        }

        [Key]
        public int IDCUAHANG { get; set; }

        [Required]
        [StringLength(200)]
        public string TENCUAHANG { get; set; }

        [Required]
        [StringLength(200)]
        public string DIACHI { get; set; }

        [Required]
        [StringLength(12)]
        public string SODIENTHOAI { get; set; }

        [Required]
        [StringLength(100)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CUAHANG_SANPHAM> tb_CUAHANG_SANPHAM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CUAHANG_SPDK> tb_CUAHANG_SPDK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CUAHANG_SPCT> tb_CUAHANG_SPCT { get; set; }
    }
}
