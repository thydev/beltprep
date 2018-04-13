using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace beltprep.Migrations
{
    public partial class UpdateAuctionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Auctions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Auctions");
        }
    }
}
