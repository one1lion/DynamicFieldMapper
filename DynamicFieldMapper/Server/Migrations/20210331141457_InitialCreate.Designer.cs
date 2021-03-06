// <auto-generated />
using DynamicFieldMapper.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DynamicFieldMapper.Server.Migrations
{
    [DbContext(typeof(DataMapContext))]
    [Migration("20210331141457_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DynamicFieldMapper.Shared.MappableField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DataType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("MappableField");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataType = 18,
                            Name = "Company"
                        },
                        new
                        {
                            Id = 2,
                            DataType = 18,
                            Name = "Product"
                        },
                        new
                        {
                            Id = 3,
                            DataType = 18,
                            Name = "Quantity"
                        },
                        new
                        {
                            Id = 4,
                            DataType = 18,
                            Name = "EffectiveDate"
                        },
                        new
                        {
                            Id = 5,
                            DataType = 18,
                            Name = "LessIncluded"
                        },
                        new
                        {
                            Id = 6,
                            DataType = 18,
                            Name = "UnitCost"
                        },
                        new
                        {
                            Id = 7,
                            DataType = 18,
                            Name = "UnitPrice"
                        },
                        new
                        {
                            Id = 8,
                            DataType = 18,
                            Name = "CancelDate"
                        },
                        new
                        {
                            Id = 9,
                            DataType = 18,
                            Name = "Sequence"
                        },
                        new
                        {
                            Id = 10,
                            DataType = 18,
                            Name = "SerialNumber"
                        });
                });

            modelBuilder.Entity("DynamicFieldMapper.Shared.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "UserName")
                        .IsUnique();

                    b.ToTable("Template");
                });

            modelBuilder.Entity("DynamicFieldMapper.Shared.TemplateField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MappableFieldId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MappableFieldId");

                    b.HasIndex("TemplateId");

                    b.ToTable("TemplateField");
                });

            modelBuilder.Entity("DynamicFieldMapper.Shared.TemplateField", b =>
                {
                    b.HasOne("DynamicFieldMapper.Shared.MappableField", "MappableField")
                        .WithMany("TemplateFields")
                        .HasForeignKey("MappableFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DynamicFieldMapper.Shared.Template", "Template")
                        .WithMany("TemplateFields")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MappableField");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("DynamicFieldMapper.Shared.MappableField", b =>
                {
                    b.Navigation("TemplateFields");
                });

            modelBuilder.Entity("DynamicFieldMapper.Shared.Template", b =>
                {
                    b.Navigation("TemplateFields");
                });
#pragma warning restore 612, 618
        }
    }
}
