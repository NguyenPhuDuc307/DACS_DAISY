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
            tb_CUAHANG_SPCT = new HashSet<tb_CUAHANG_SPCT>();
        }

        [Key]
        public int IDCUAHANG { get; set; }

        [Required]
        [StringLength(128)]
        public string IDUSER { get; set; }

        [Required]
        [StringLength(200)]
        public string TENCUAHANG { get; set; }

        public string HINHANH { get; set; }

        public string GOOGLEMAP { get; set; }

        [StringLength(128)]
        public string TRANGTHAI { get; set; }

        public bool? UYTIN { get; set; }

        public bool? DINHCHI { get; set; }

        public bool? XETDUYET { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CUAHANG_SPCT> tb_CUAHANG_SPCT { get; set; }
    }
}
