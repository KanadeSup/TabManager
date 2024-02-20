using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Merge_UserInfo_UserAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserInfos_UserInfo_Id",
                table: "UserAccounts");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_UserInfo_Id",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "UserInfo_Id",
                table: "UserAccounts");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "UserAccounts",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "UserAccounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "UserAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfo_Id",
                table: "UserAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Account_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Avatar = table.Column<byte[]>(type: "bytea", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_UserAccounts_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserInfo_Id",
                table: "UserAccounts",
                column: "UserInfo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Account_Id",
                table: "UserInfos",
                column: "Account_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserInfos_UserInfo_Id",
                table: "UserAccounts",
                column: "UserInfo_Id",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
