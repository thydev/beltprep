using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace beltprep.Migrations
{
    public partial class DbSetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auction_Users_UserId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Auction_AuctionId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Users_UserId",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auction",
                table: "Auction");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "Bids");

            migrationBuilder.RenameTable(
                name: "Auction",
                newName: "Auctions");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_UserId",
                table: "Bids",
                newName: "IX_Bids_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionId",
                table: "Bids",
                newName: "IX_Bids_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_UserId",
                table: "Auctions",
                newName: "IX_Auctions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "BidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_UserId",
                table: "Auctions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_UserId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "Bid");

            migrationBuilder.RenameTable(
                name: "Auctions",
                newName: "Auction");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_UserId",
                table: "Bid",
                newName: "IX_Bid_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionId",
                table: "Bid",
                newName: "IX_Bid_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_UserId",
                table: "Auction",
                newName: "IX_Auction_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "BidId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auction",
                table: "Auction",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_Users_UserId",
                table: "Auction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Auction_AuctionId",
                table: "Bid",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Users_UserId",
                table: "Bid",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
