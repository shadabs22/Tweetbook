using Microsoft.EntityFrameworkCore.Migrations;

namespace Tweetbook.Data.Migrations
{
    public partial class updatedtag1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_UserId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tags",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                newName: "IX_Tags_userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_userid",
                table: "Tags",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_userid",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Tags",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_userid",
                table: "Tags",
                newName: "IX_Tags_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_UserId",
                table: "Tags",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
