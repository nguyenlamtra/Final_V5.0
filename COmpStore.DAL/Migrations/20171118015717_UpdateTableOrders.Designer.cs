﻿// <auto-generated />
using COmpStore.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace COmpStore.DAL.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20171118015717_UpdateTableOrders")]
    partial class UpdateTableOrders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("COmpStore.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Categories","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FullName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .IsRequired();

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasName("IX_Customers");

                    b.ToTable("Customers","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<int>("CustomerId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<decimal?>("OrderTotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("money")
                        .HasComputedColumnSql("StoreComp.GetOrderTotal([Id])");

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<DateTime>("ShipDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Status");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal?>("LineItemTotal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("money")
                        .HasComputedColumnSql("[Quantity]*[UnitCost]");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasMaxLength(3800);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFeatured");

                    b.Property<string>("Number")
                        .HasMaxLength(50);

                    b.Property<string>("ProductImage")
                        .HasMaxLength(150);

                    b.Property<string>("ProductName")
                        .HasMaxLength(50);

                    b.Property<int>("PublisherId");

                    b.Property<int>("SubCategoryId");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("money")
                        .HasMaxLength(150);

                    b.Property<int>("UnitsInStock");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PublisherName")
                        .HasMaxLength(50);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Publishers","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SubCategoryName")
                        .HasMaxLength(50);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories","StoreComp");
                });

            modelBuilder.Entity("COmpStore.Models.Entities.Order", b =>
                {
                    b.HasOne("COmpStore.Models.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("COmpStore.Models.Entities.OrderDetail", b =>
                {
                    b.HasOne("COmpStore.Models.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("COmpStore.Models.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("COmpStore.Models.Entities.Product", b =>
                {
                    b.HasOne("COmpStore.Models.Entities.Publisher", "Publisher")
                        .WithMany("Products")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("COmpStore.Models.Entities.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("COmpStore.Models.Entities.SubCategory", b =>
                {
                    b.HasOne("COmpStore.Models.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
