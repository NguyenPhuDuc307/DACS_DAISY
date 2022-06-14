namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_CUAHANG_SPDK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_CUAHANG_SPDK()
        {
            tb_SANPHAM_SPDK = new HashSet<tb_SANPHAM_SPDK>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDCUAHANG { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDSPDK { get; set; }
        [Required]
        [StringLength(256)]
        public string TENSPDK { get; set; }

        [Required]
        [StringLength(128)]
        public string HINHANH { get; set; }

        public double GIABAN { get; set; }

        [Required]
        [StringLength(128)]
        public string TRANGTHAI { get; set; }

        public virtual tb_CUAHANG tb_CUAHANG { get; set; }

        public virtual tb_SPDK tb_SPDK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SANPHAM_SPDK> tb_SANPHAM_SPDK { get; set; }
    }
}
