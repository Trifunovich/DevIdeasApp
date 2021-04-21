using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationLibrary.Migrations
{
    public partial class addingrefreshtokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    ExpiresOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AuthenticationUserModelId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshModel_AspNetUsers_AuthenticationUserModelId",
                        column: x => x.AuthenticationUserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshModel_AuthenticationUserModelId",
                table: "RefreshModel",
                column: "AuthenticationUserModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshModel");
        }
    }
}
