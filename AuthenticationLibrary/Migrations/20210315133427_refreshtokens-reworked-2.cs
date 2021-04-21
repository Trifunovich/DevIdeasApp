using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationLibrary.Migrations
{
    public partial class refreshtokensreworked2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshModels_AspNetUsers_AuthenticationUserModelId",
                table: "RefreshModels");

            migrationBuilder.DropIndex(
                name: "IX_RefreshModels_AuthenticationUserModelId",
                table: "RefreshModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RefreshModels_AuthenticationUserModelId",
                table: "RefreshModels",
                column: "AuthenticationUserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshModels_AspNetUsers_AuthenticationUserModelId",
                table: "RefreshModels",
                column: "AuthenticationUserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
