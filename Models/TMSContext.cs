using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace T.Models
{
    public partial class TMSContext : DbContext
    {
        public TMSContext()
        {
        }

        public TMSContext(DbContextOptions<TMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Subtask> Subtask { get; set; }
        public virtual DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=laptop-doatngi0\\sqlexpress;Database=TMS;UID=hope;PWD=hope;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subtask>(entity =>
            {
                entity.HasKey(e => e.SubId);

                entity.ToTable("subtask");

                entity.Property(e => e.SubId)
                    .HasColumnName("sub_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.SubDesc)
                    .IsRequired()
                    .HasColumnName("sub_desc")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.SubFdate)
                    .HasColumnName("sub_fdate")
                    .HasColumnType("date");

                entity.Property(e => e.SubName)
                    .IsRequired()
                    .HasColumnName("sub_name")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.SubSdate)
                    .HasColumnName("sub_sdate")
                    .HasColumnType("date");

                entity.Property(e => e.SubState)
                    .IsRequired()
                    .HasColumnName("sub_state")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Subtask)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_subtask_task");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.TaskId)
                    .HasColumnName("task_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TaskDesc)
                    .IsRequired()
                    .HasColumnName("task_desc")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.TaskFdate)
                    .HasColumnName("task_fdate")
                    .HasColumnType("date");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasColumnName("task_name")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.TaskSdate)
                    .HasColumnName("task_sdate")
                    .HasColumnType("date");

                entity.Property(e => e.TaskState)
                    .IsRequired()
                    .HasColumnName("task_state")
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
