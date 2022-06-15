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

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tb_CUAHANG> tb_CUAHANG { get; set; }
        public virtual DbSet<tb_CUAHANG_SPCT> tb_CUAHANG_SPCT { get; set; }
        public virtual DbSet<tb_CUAHANG_SPDK> tb_CUAHANG_SPDK { get; set; }
        public virtual DbSet<tb_GIOHANG> tb_GIOHANG { get; set; }
        public virtual DbSet<tb_GIOHANG_SPC> tb_GIOHANG_SPC { get; set; }
        public virtual DbSet<tb_GIOHANG_SPDK> tb_GIOHANG_SPDK { get; set; }
        public virtual DbSet<tb_KICHCO> tb_KICHCO { get; set; }
        public virtual DbSet<tb_LOAISANPHAM> tb_LOAISANPHAM { get; set; }
        public virtual DbSet<tb_SANPHAM> tb_SANPHAM { get; set; }
        public virtual DbSet<tb_SANPHAM_SPDK> tb_SANPHAM_SPDK { get; set; }
        public virtual DbSet<tb_SPDK> tb_SPDK { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.tb_CUAHANG)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IDUSER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.tb_GIOHANG)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IDKHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG>()
                .Property(e => e.partnerCode)
                .IsFixedLength();

            modelBuilder.Entity<tb_CUAHANG>()
                .Property(e => e.accessKey)
                .IsFixedLength();

            modelBuilder.Entity<tb_CUAHANG>()
                .Property(e => e.serectkey)
                .IsFixedLength();

            modelBuilder.Entity<tb_CUAHANG>()
                .HasMany(e => e.tb_CUAHANG_SPCT)
                .WithRequired(e => e.tb_CUAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG>()
                .HasMany(e => e.tb_CUAHANG_SPDK)
                .WithRequired(e => e.tb_CUAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG>()
                .HasMany(e => e.tb_SANPHAM_SPDK)
                .WithRequired(e => e.tb_CUAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG_SPCT>()
                .HasMany(e => e.tb_GIOHANG_SPC)
                .WithRequired(e => e.tb_CUAHANG_SPCT)
                .HasForeignKey(e => e.IDSANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG_SPCT>()
                .HasMany(e => e.tb_GIOHANG_SPDK)
                .WithRequired(e => e.tb_CUAHANG_SPCT)
                .HasForeignKey(e => e.IDSANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG_SPDK>()
                .HasMany(e => e.tb_GIOHANG_SPDK)
                .WithRequired(e => e.tb_CUAHANG_SPDK)
                .HasForeignKey(e => e.IDSPDK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_GIOHANG>()
                .HasMany(e => e.tb_GIOHANG_SPC)
                .WithRequired(e => e.tb_GIOHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_GIOHANG>()
                .HasMany(e => e.tb_GIOHANG_SPDK)
                .WithRequired(e => e.tb_GIOHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_KICHCO>()
                .HasMany(e => e.tb_CUAHANG_SPCT)
                .WithRequired(e => e.tb_KICHCO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_KICHCO>()
                .HasMany(e => e.tb_SANPHAM)
                .WithMany(e => e.tb_KICHCO)
                .Map(m => m.ToTable("tb_SANPHAM_KICHCO").MapLeftKey("IDKICHCO").MapRightKey("IDSANPHAM"));

            modelBuilder.Entity<tb_LOAISANPHAM>()
                .HasMany(e => e.tb_SANPHAM)
                .WithRequired(e => e.tb_LOAISANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SANPHAM>()
                .HasMany(e => e.tb_CUAHANG_SPCT)
                .WithRequired(e => e.tb_SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SANPHAM>()
                .HasMany(e => e.tb_SANPHAM_SPDK)
                .WithRequired(e => e.tb_SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SPDK>()
                .HasMany(e => e.tb_CUAHANG_SPDK)
                .WithRequired(e => e.tb_SPDK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SPDK>()
                .HasMany(e => e.tb_SANPHAM_SPDK)
                .WithRequired(e => e.tb_SPDK)
                .WillCascadeOnDelete(false);
        }
    }
}
