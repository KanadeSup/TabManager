using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey_Space : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userAccountId",
                table: "Spaces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_userAccountId",
                table: "Spaces",
                column: "userAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_UserAccounts_userAccountId",
                table: "Spaces",
                column: "userAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_UserAccounts_userAccountId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_userAccountId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "userAccountId",
                table: "Spaces");
        }
    }
}
