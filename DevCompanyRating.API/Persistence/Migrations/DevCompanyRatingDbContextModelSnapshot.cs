﻿// <auto-generated />
using System;
using DevCompanyRating.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevCompanyRating.API.Persistence.Migrations
{
    [DbContext(typeof(DevCompanyRatingDbContext))]
    partial class DevCompanyRatingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevCompanyRating.API.Domain.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DevCompanyRating.API.Domain.CompanyRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentsInEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<double>("NegativeScore")
                        .HasColumnType("float");

                    b.Property<double>("NeutralScore")
                        .HasColumnType("float");

                    b.Property<double>("PositiveScore")
                        .HasColumnType("float");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Sentiment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCompany");

                    b.ToTable("CompanyRatings");
                });

            modelBuilder.Entity("DevCompanyRating.API.Domain.CompanyRating", b =>
                {
                    b.HasOne("DevCompanyRating.API.Domain.Company", null)
                        .WithMany("Ratings")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DevCompanyRating.API.Domain.Company", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
