using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMVC.Migrations
{
    public partial class ImageTypeByte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Photo",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Photo");
        }
    }
}
