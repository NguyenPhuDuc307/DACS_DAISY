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

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tb_CUAHANG> tb_CUAHANG { get; set; }
        public virtual DbSet<tb_CUAHANG_SPCT> tb_CUAHANG_SPCT { get; set; }
        public virtual DbSet<tb_GIOHANG> tb_GIOHANG { get; set; }
        public virtual DbSet<tb_GIOHANG_SPC> tb_GIOHANG_SPC { get; set; }
        public virtual DbSet<tb_KICHCO> tb_KICHCO { get; set; }
        public virtual DbSet<tb_LOAISANPHAM> tb_LOAISANPHAM { get; set; }
        public virtual DbSet<tb_SANPHAM> tb_SANPHAM { get; set; }
        public virtual DbSet<tb_THEODOI> tb_THEODOI { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

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

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.tb_THEODOI)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG>()
                .HasMany(e => e.tb_CUAHANG_SPCT)
                .WithRequired(e => e.tb_CUAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG>()
                .HasMany(e => e.tb_THEODOI)
                .WithRequired(e => e.tb_CUAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_CUAHANG_SPCT>()
                .HasMany(e => e.tb_GIOHANG_SPC)
                .WithRequired(e => e.tb_CUAHANG_SPCT)
                .HasForeignKey(e => e.IDSANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_GIOHANG>()
                .HasMany(e => e.tb_GIOHANG_SPC)
                .WithRequired(e => e.tb_GIOHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_KICHCO>()
                .HasMany(e => e.tb_CUAHANG_SPCT)
                .WithRequired(e => e.tb_KICHCO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_LOAISANPHAM>()
                .HasMany(e => e.tb_SANPHAM)
                .WithRequired(e => e.tb_LOAISANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SANPHAM>()
                .HasMany(e => e.tb_CUAHANG_SPCT)
                .WithRequired(e => e.tb_SANPHAM)
                .WillCascadeOnDelete(false);
        }
    }
}
