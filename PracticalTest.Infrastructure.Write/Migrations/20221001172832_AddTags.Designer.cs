﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracticalTest.Infrastructure;

#nullable disable

namespace PracticalTest.Infrastructure.Migrations
{
    [DbContext(typeof(PracticalTestWriteDbContext))]
    [Migration("20221001172832_AddTags")]
    partial class AddTags
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.Property<long>("BlogPostsId")
                        .HasColumnType("bigint");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint");

                    b.HasKey("BlogPostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BlogPostTag", "dbo");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Blogs.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs", "dbo");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Blogs.BlogPost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("BlogId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogPost", "dbo");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Blogs.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("Tag", "dbo");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.HasOne("PracticalTest.Domain.Write.Blogs.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticalTest.Domain.Write.Blogs.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Blogs.Blog", b =>
                {
                    b.HasOne("PracticalTest.Domain.Write.Users.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Blogs.BlogPost", b =>
                {
                    b.HasOne("PracticalTest.Domain.Write.Blogs.Blog", null)
                        .WithMany("BlogPosts")
                        .HasForeignKey("BlogId");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Blogs.Blog", b =>
                {
                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("PracticalTest.Domain.Write.Users.User", b =>
                {
                    b.Navigation("Blogs");
                });
#pragma warning restore 612, 618
        }
    }
}
