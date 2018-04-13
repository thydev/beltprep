using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace beltprep.Migrations
{
    public partial class bidupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "highestBid2",
                table: "Auctions",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "highestBid2",
                table: "Auctions");
        }
    }
}
