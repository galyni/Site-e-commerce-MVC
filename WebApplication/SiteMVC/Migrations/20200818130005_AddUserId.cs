using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMVC.Migrations
{
    public partial class AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mail",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "mail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
