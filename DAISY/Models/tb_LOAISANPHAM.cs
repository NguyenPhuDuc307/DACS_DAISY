namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_LOAISANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_LOAISANPHAM()
        {
            tb_SANPHAM = new HashSet<tb_SANPHAM>();
        }

        [Key]
        public int IDLOAISANPHAM { get; set; }

        [Required]
        [StringLength(100)]
        public string TENLOAISANPHAM { get; set; }

        [StringLength(100)]
        public string HINHANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SANPHAM> tb_SANPHAM { get; set; }
    }
}
