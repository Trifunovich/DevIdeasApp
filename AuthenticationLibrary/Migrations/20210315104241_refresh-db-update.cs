using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationLibrary.Migrations
{
    public partial class refreshdbupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshModel_AspNetUsers_AuthenticationUserModelId",
                table: "RefreshModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshModel",
                table: "RefreshModel");

            migrationBuilder.RenameTable(
                name: "RefreshModel",
                newName: "RefreshModels");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshModel_AuthenticationUserModelId",
                table: "RefreshModels",
                newName: "IX_RefreshModels_AuthenticationUserModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshModels",
                table: "RefreshModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshModels_AspNetUsers_AuthenticationUserModelId",
                table: "RefreshModels",
                column: "AuthenticationUserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshModels_AspNetUsers_AuthenticationUserModelId",
                table: "RefreshModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshModels",
                table: "RefreshModels");

            migrationBuilder.RenameTable(
                name: "RefreshModels",
                newName: "RefreshModel");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshModels_AuthenticationUserModelId",
                table: "RefreshModel",
                newName: "IX_RefreshModel_AuthenticationUserModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshModel",
                table: "RefreshModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshModel_AspNetUsers_AuthenticationUserModelId",
                table: "RefreshModel",
                column: "AuthenticationUserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
