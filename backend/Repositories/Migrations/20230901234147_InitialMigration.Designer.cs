﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories.DAL;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230901234147_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repositories.Models.Card", b =>
                {
                    b.Property<string>("CardCode")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<int>("AttackPower")
                        .HasColumnType("int");

                    b.Property<string>("CardImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("HealthValue")
                        .HasColumnType("int");

                    b.Property<int>("ManaCost")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Regions")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("CardCode");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Repositories.Models.CustomCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AttackPower")
                        .HasColumnType("int");

                    b.Property<string>("CardDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("CardImageName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EffectText")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("HealthValue")
                        .HasColumnType("int");

                    b.Property<int>("ManaCost")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerAccountId");

                    b.ToTable("CustomCards");
                });

            modelBuilder.Entity("Repositories.Models.Deck", b =>
                {
                    b.Property<string>("DeckCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeckName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Eternal")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("DeckCode");

                    b.HasIndex("OwnerAccountId");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("Repositories.Models.DeckItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CardCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(7)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("DeckCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CardCode");

                    b.HasIndex("DeckCode");

                    b.ToTable("DeckItem");
                });

            modelBuilder.Entity("Repositories.Models.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("Permissions")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Repositories.Models.CustomCard", b =>
                {
                    b.HasOne("Repositories.Models.UserAccount", "OwnerAccount")
                        .WithMany("CustomCards")
                        .HasForeignKey("OwnerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerAccount");
                });

            modelBuilder.Entity("Repositories.Models.Deck", b =>
                {
                    b.HasOne("Repositories.Models.UserAccount", "OwnerAccount")
                        .WithMany()
                        .HasForeignKey("OwnerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerAccount");
                });

            modelBuilder.Entity("Repositories.Models.DeckItem", b =>
                {
                    b.HasOne("Repositories.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repositories.Models.Deck", null)
                        .WithMany("DeckContent")
                        .HasForeignKey("DeckCode");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Repositories.Models.Deck", b =>
                {
                    b.Navigation("DeckContent");
                });

            modelBuilder.Entity("Repositories.Models.UserAccount", b =>
                {
                    b.Navigation("CustomCards");
                });
#pragma warning restore 612, 618
        }
    }
}
