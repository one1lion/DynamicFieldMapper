using DynamicFieldMapper.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicFieldMapper.Server.Data {
  public class DataMapContext : DbContext {
    public DataMapContext(DbContextOptions options) : base(options) {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder) {
      if (!builder.IsConfigured) {
        builder.UseSqlServer("name=DBContext");
      }
    }

    public DbSet<MappableField> MappableFields { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<TemplateField> TemplateFields { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<MappableField>(entity => {
        entity.ToTable("MappableField");

        entity.Property(x => x.Name)
          .HasMaxLength(60)
          .IsRequired();

        entity.HasMany(x => x.TemplateFields)
          .WithOne(x => x.MappableField);
      });

      modelBuilder.Entity<Template>(entity => {
        entity.ToTable("Template");

        entity.Property(x => x.Name)
          .HasMaxLength(60)
          .IsRequired();

        entity.Property(x => x.UserName)
          .HasMaxLength(60)
          .IsRequired();

        entity.HasIndex(x => new { x.Name, x.UserName })
          .IsUnique();

        entity.HasMany(x => x.TemplateFields)
          .WithOne(x => x.Template);
      });

      modelBuilder.Entity<TemplateField>(entity => {
        entity.ToTable("TemplateField");

        entity.Property(x => x.Name)
          .HasMaxLength(60)
          .IsRequired();

        entity.HasOne(x => x.MappableField)
          .WithMany(x => x.TemplateFields);

        entity.HasOne(x => x.Template)
          .WithMany(x => x.TemplateFields);
      });

      Seed(modelBuilder);
    }

    public void Seed(ModelBuilder modelBuilder) {
      var id = 1;
      var mappableFields = ImportFile.GetPropertyNames()
        .Select(x => new MappableField() {
          Id = id++,
          Name = x,
          DataType = TypeCode.String
        });
      modelBuilder.Entity<MappableField>().HasData(mappableFields);
    }
  }
}
