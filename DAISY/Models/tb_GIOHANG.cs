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
            tb_GIOHANG_SPDK = new HashSet<tb_GIOHANG_SPDK>();
        }

        [Key]
        public int IDGIOHANG { get; set; }

        [Required]
        [StringLength(128)]
        public string IDKHACHHANG { get; set; }

        public DateTime? NGAYTAO { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_GIOHANG_SPC> tb_GIOHANG_SPC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_GIOHANG_SPDK> tb_GIOHANG_SPDK { get; set; }
    }
}
