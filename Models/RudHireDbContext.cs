using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Rudhire_BE.Models;

public partial class RudHireDbContext : DbContext
{
    public RudHireDbContext()
    {
    }

    public RudHireDbContext(DbContextOptions<RudHireDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUserDetail> TblUserDetails { get; set; }

    public virtual DbSet<TblUserQualification> TblUserQualifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-6AIBT7VI;Database=RudHireDB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblUserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tblUserDetails");

            entity.HasIndex(e => e.EmailId, "UQ_UserDetails_EmailID").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_UserDetails_PhoneNumber").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ_UserDetails_UserName").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nick_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('USER')")
                .HasColumnName("role");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<TblUserQualification>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("tblUserQualification");

            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.Degree)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("degree");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.UniversityName)
                .IsUnicode(false)
                .HasColumnName("university_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserQualifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblUserQualification_tblUserDetails");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
