namespace DAISY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_THEODOI
    {
        [Key]
        [Column(Order = 0)]
        public string IdUser { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCuahang { get; set; }

        public DateTime? NgayTheoDoi { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual tb_CUAHANG tb_CUAHANG { get; set; }
    }
}
