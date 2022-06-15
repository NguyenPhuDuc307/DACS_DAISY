namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_SPDK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_SPDK()
        {
            tb_CUAHANG_SPDK = new HashSet<tb_CUAHANG_SPDK>();
            tb_SANPHAM_SPDK = new HashSet<tb_SANPHAM_SPDK>();
        }

        [Key]
        public int IDSPDK { get; set; }

        [Required]
        [StringLength(100)]
        public string TENSPDK { get; set; }

        [StringLength(100)]
        public string HINHANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CUAHANG_SPDK> tb_CUAHANG_SPDK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SANPHAM_SPDK> tb_SANPHAM_SPDK { get; set; }
    }
}
