﻿// <auto-generated />
using System;
using Auftragsverwaltung.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210828135844_UserCanHaveMultipleRelationsWithAddresses")]
    partial class UserCanHaveMultipleRelationsWithAddresses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Auftragsverwaltung.Domain.Address.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BuildingNr")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TownId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("AddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TownId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Article.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ArticleId");

                    b.HasIndex("ArticleGroupId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.ArticleGroup.ArticleGroup", b =>
                {
                    b.Property<int>("ArticleGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ParentArticleGroupId")
                        .HasColumnType("int");

                    b.HasKey("ArticleGroupId");

                    b.HasIndex("ParentArticleGroupId");

                    b.ToTable("ArticleGroup");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Customer.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerNumber")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("binary(70)");

                    b.Property<string>("Website")
                        .HasColumnType("varchar(255)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Order.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Position.Position", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Town.Town", b =>
                {
                    b.Property<int>("TownId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Townname")
                        .IsRequired()
                        .HasColumnType("varchar(85)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("TownId");

                    b.ToTable("Town");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Address.Address", b =>
                {
                    b.HasOne("Auftragsverwaltung.Domain.Customer.Customer", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Auftragsverwaltung.Domain.Town.Town", "Town")
                        .WithMany("Addresses")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Article.Article", b =>
                {
                    b.HasOne("Auftragsverwaltung.Domain.ArticleGroup.ArticleGroup", "ArticleGroup")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleGroup");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.ArticleGroup.ArticleGroup", b =>
                {
                    b.HasOne("Auftragsverwaltung.Domain.ArticleGroup.ArticleGroup", "ParentArticleGroup")
                        .WithMany("ChildArticlesGroups")
                        .HasForeignKey("ParentArticleGroupId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ParentArticleGroup");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Order.Order", b =>
                {
                    b.HasOne("Auftragsverwaltung.Domain.Customer.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Position.Position", b =>
                {
                    b.HasOne("Auftragsverwaltung.Domain.Article.Article", "Article")
                        .WithMany("Positions")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Auftragsverwaltung.Domain.Order.Order", "Order")
                        .WithMany("Positions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Article.Article", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.ArticleGroup.ArticleGroup", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("ChildArticlesGroups");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Customer.Customer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Order.Order", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Auftragsverwaltung.Domain.Town.Town", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
