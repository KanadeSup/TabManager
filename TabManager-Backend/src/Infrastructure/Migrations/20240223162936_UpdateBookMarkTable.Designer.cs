﻿// <auto-generated />
using System;
using Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AnnotationDbContext))]
    [Migration("20240223162936_UpdateBookMarkTable")]
    partial class UpdateBookMarkTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Domain.Entities.Bookmark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Category_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("WebIcon")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("Core.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Space_Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Space_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Domain.Entities.Space", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("HexColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("userAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("userAccountId");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("Core.Domain.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Core.Domain.Entities.Bookmark", b =>
                {
                    b.HasOne("Core.Domain.Entities.Category", "Category")
                        .WithMany("Bookmarks")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Core.Domain.Entities.Category", b =>
                {
                    b.HasOne("Core.Domain.Entities.Space", "Space")
                        .WithMany("Categories")
                        .HasForeignKey("Space_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Space");
                });

            modelBuilder.Entity("Core.Domain.Entities.Space", b =>
                {
                    b.HasOne("Core.Domain.Entities.UserAccount", "userAccount")
                        .WithMany("Spaces")
                        .HasForeignKey("userAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userAccount");
                });

            modelBuilder.Entity("Core.Domain.Entities.Category", b =>
                {
                    b.Navigation("Bookmarks");
                });

            modelBuilder.Entity("Core.Domain.Entities.Space", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Core.Domain.Entities.UserAccount", b =>
                {
                    b.Navigation("Spaces");
                });
#pragma warning restore 612, 618
        }
    }
}
