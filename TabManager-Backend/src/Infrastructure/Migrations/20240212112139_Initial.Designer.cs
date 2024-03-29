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
    [Migration("20240212112139_Initial")]
    partial class Initial
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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<Guid>("UserAccount_Id")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("WebIcon")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Category_Id");

                    b.HasIndex("UserAccount_Id");

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

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("Core.Domain.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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

                    b.Property<Guid>("UserInfo_Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserInfo_Id");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Core.Domain.Entities.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Account_Id")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Account_Id");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Core.Domain.Entities.Bookmark", b =>
                {
                    b.HasOne("Core.Domain.Entities.Category", "Category")
                        .WithMany("Bookmarks")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.UserAccount", "UserAccount")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserAccount_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("UserAccount");
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

            modelBuilder.Entity("Core.Domain.Entities.UserAccount", b =>
                {
                    b.HasOne("Core.Domain.Entities.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfo_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Core.Domain.Entities.UserInfo", b =>
                {
                    b.HasOne("Core.Domain.Entities.UserAccount", "Account")
                        .WithMany()
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
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
                    b.Navigation("Bookmarks");
                });
#pragma warning restore 612, 618
        }
    }
}
