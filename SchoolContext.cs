using System;
using System.Collections.Generic;
using lab3_School.Models;
using Microsoft.EntityFrameworkCore;

namespace lab3_School;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassType> ClassTypes { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-I3E57O3\\SQLEXPRESS;Initial Catalog=School;Integrated Security=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC0708000F09");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ClassType).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassTypeId)
                .HasConstraintName("FK__Classes__ClassTy__3B75D760");

            entity.HasOne(d => d.ClassroomTeacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassroomTeacherId)
                .HasConstraintName("FK__Classes__Classro__3A81B327");
        });

        modelBuilder.Entity<ClassType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClassTyp__3214EC0752BF5CFC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3214EC07E3E8BE30");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LessonDate).HasColumnType("date");

            entity.HasOne(d => d.Class).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Schedules__Class__4316F928");

            entity.HasOne(d => d.Subject).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Schedules__Subje__440B1D61");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Schedules__Teach__44FF419A");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0799DFBD76");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AdditionalInfo).IsUnicode(false);
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FutherFullName).IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MotherFullName).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__ClassI__3E52440B");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC07E8976DC6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teachers__3214EC07622B0D40");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
