using DynamicForm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Form> Forms => Set<Form>();
        public DbSet<FormField> FormFields => Set<FormField>();
        public DbSet<FieldValidation> FieldValidations => Set<FieldValidation>();
        public DbSet<FieldOption> FieldOptions => Set<FieldOption>();
        public DbSet<FormSubmission> FormSubmissions => Set<FormSubmission>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FormKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(x => new { x.FormKey, x.Version })
                    .IsUnique();

                entity.HasMany(x => x.Fields)
                    .WithOne(x => x.Form)
                    .HasForeignKey(x => x.FormId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FormField>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FieldName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Label)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.InputType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.Placeholder)
                    .HasMaxLength(250);

                entity.Property(x => x.DefaultValue)
                    .HasMaxLength(500);

                entity.HasIndex(x => new { x.FormId, x.FieldName })
                    .IsUnique();

                entity.HasMany(x => x.Validations)
                    .WithOne(x => x.FormField)
                    .HasForeignKey(x => x.FormFieldId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.Options)
                    .WithOne(x => x.FormField)
                    .HasForeignKey(x => x.FormFieldId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FieldValidation>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.ValidationType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.ValidationValue)
                    .HasMaxLength(250);

                entity.Property(x => x.ErrorMessage)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<FieldOption>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.OptionLabel)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.OptionValue)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<FormSubmission>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.SubmittedDataJson)
                    .IsRequired();

                entity.HasOne(x => x.Form)
                    .WithMany()
                    .HasForeignKey(x => x.FormId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
