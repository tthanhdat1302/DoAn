namespace DoAn2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NhaSachDbContext : DbContext
    {
        public NhaSachDbContext()
            : base("name=NhaSachDbContext2")
        {
        }

        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<DatHangCT> DatHangCTs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<SlideBar> SlideBars { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<ThamGia> ThamGias { get; set; }
        public virtual DbSet<ThongTinCongTi> ThongTinCongTis { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserKH> UserKHs { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<YKienKH> YKienKHs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChuDe>()
                .Property(e => e.MaChuDe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DatHang>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<DatHang>()
                .HasMany(e => e.DatHangCTs)
                .WithRequired(e => e.DatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuType>()
                .HasMany(e => e.Menus)
                .WithOptional(e => e.MenuType)
                .HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<NhaXuatBan>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhaXuatBan>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.NhaXuatBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.MaChuDe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.DatHangCTs)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ThamGias)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SlideBar>()
                .Property(e => e.Hinh)
                .IsUnicode(false);

            modelBuilder.Entity<TacGia>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.ThamGias)
                .WithRequired(e => e.TacGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinCongTi>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinCongTi>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<UserKH>()
                .Property(e => e.Hinh)
                .IsUnicode(false);

            modelBuilder.Entity<UserKH>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.UserKH)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YKienKH>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
