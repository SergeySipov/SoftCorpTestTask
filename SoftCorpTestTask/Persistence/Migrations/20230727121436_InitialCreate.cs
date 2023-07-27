using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "costCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_costCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "families",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_families", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    passwordHash = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: false),
                    passwordSalt = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usersCosts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: false),
                    currencyCode = table.Column<int>(type: "int", nullable: false, defaultValue: 11),
                    comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    date = table.Column<DateTime>(type: "SmallDateTime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    costCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_usersCosts", x => x.id);
                    table.ForeignKey(
                        name: "fK_usersCosts_costCategories_costCategoryId",
                        column: x => x.costCategoryId,
                        principalTable: "costCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_usersCosts_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersFamilies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    familyId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_usersFamilies", x => x.id);
                    table.ForeignKey(
                        name: "fK_usersFamilies_families_familyId",
                        column: x => x.familyId,
                        principalTable: "families",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_usersFamilies_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersRefreshTokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expirationDateTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_usersRefreshTokens", x => x.id);
                    table.ForeignKey(
                        name: "fK_usersRefreshTokens_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "costCategories",
                columns: new[] { "id", "color", "description", "name" },
                values: new object[,]
                {
                    { 1, "#eb4034", null, "Groceries" },
                    { 2, "#ebde34", null, "Transport" },
                    { 3, "#4feb34", null, "Mobile Communication" },
                    { 4, "#1732ff", null, "Internet" },
                    { 5, "#6017ff", null, "Entertainment" }
                });

            migrationBuilder.CreateIndex(
                name: "iX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_usersCosts_costCategoryId",
                table: "usersCosts",
                column: "costCategoryId");

            migrationBuilder.CreateIndex(
                name: "iX_usersCosts_userId",
                table: "usersCosts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_usersFamilies_familyId",
                table: "usersFamilies",
                column: "familyId");

            migrationBuilder.CreateIndex(
                name: "iX_usersFamilies_userId",
                table: "usersFamilies",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_usersRefreshTokens_userId",
                table: "usersRefreshTokens",
                column: "userId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersCosts");

            migrationBuilder.DropTable(
                name: "usersFamilies");

            migrationBuilder.DropTable(
                name: "usersRefreshTokens");

            migrationBuilder.DropTable(
                name: "costCategories");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
