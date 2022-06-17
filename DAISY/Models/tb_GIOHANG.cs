namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_GIOHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_GIOHANG()
        {
            tb_GIOHANG_SPC = new HashSet<tb_GIOHANG_SPC>();
        }

        [Key]
        public int IDGIOHANG { get; set; }

        [Required]
        [StringLength(128)]
        public string IDKHACHHANG { get; set; }

        public DateTime? NGAYTAO { get; set; }

        public double? THANHTIEN { get; set; }

        public bool THANHTOAN { get; set; }

        [Required]
        [StringLength(64)]
        public string TRANGTHAI { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_GIOHANG_SPC> tb_GIOHANG_SPC { get; set; }
    }
}
