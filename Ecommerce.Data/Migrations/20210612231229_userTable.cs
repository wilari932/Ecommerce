using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Data.Migrations
{
    public partial class userTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTimeUTC = table.Column<DateTime>(nullable: false),
                    ModifiedUTC = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<string>(type: "char(32)", nullable: true),
                    CreatedById = table.Column<string>(type: "char(32)", nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    UserNameNormalized = table.Column<string>(maxLength: 100, nullable: false),
                    EmailNormalized = table.Column<string>(maxLength: 500, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailNormalized",
                table: "User",
                column: "EmailNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserNameNormalized",
                table: "User",
                column: "UserNameNormalized",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
