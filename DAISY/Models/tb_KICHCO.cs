namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_KICHCO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_KICHCO()
        {
            tb_SANPHAM_KICHCO = new HashSet<tb_SANPHAM_KICHCO>();
        }

        [Key]
        public int IDKICHCO { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên kích cỡ")]
        public string TENKICHCO { get; set; }

        [Column("THETICH-TRONGLUONG")]
        [Required]
        [StringLength(20)]
        [Display(Name = "Thể tích - Trọng lượng")]
        public string THETICH_TRONGLUONG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SANPHAM_KICHCO> tb_SANPHAM_KICHCO { get; set; }
    }
}
