﻿// <auto-generated />
using beltprep.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace beltprep.Migrations
{
    [DbContext(typeof(UserDBContext))]
    [Migration("20180413182527_DbSetMigration")]
    partial class DbSetMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("beltprep.Models.Auction", b =>
                {
                    b.Property<int>("AuctionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsEnded");

                    b.Property<decimal>("StartAmount");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("AuctionId");

                    b.HasIndex("UserId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("beltprep.Models.Bid", b =>
                {
                    b.Property<int>("BidId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuctionId");

                    b.Property<decimal>("BidAmount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsWon");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("BidId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("UserId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("beltprep.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UserName");

                    b.Property<decimal>("Wallet");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("beltprep.Models.Auction", b =>
                {
                    b.HasOne("beltprep.Models.User", "User")
                        .WithMany("Auctions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("beltprep.Models.Bid", b =>
                {
                    b.HasOne("beltprep.Models.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("beltprep.Models.User", "User")
                        .WithMany("Bids")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
