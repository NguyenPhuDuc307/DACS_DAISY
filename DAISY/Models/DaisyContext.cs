using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAISY.Models
{
    public partial class DaisyContext : DbContext
    {
        public DaisyContext()
            : base("name=DaisyContext")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<TB_SANPHAM> TB_SANPHAM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_SANPHAM>()
                .Property(e => e.GIABAN)
                .HasPrecision(18, 0);
        }
    }
}
