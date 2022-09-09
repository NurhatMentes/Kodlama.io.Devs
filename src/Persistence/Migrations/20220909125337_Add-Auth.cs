using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Technologies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ProgrammingLanguages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Github = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreationTime", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8922), "Admin" },
                    { 2, new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8923), "Editor" }
                });

            migrationBuilder.UpdateData(
                table: "ProgrammingLanguages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8768));

            migrationBuilder.UpdateData(
                table: "ProgrammingLanguages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8770));

            migrationBuilder.UpdateData(
                table: "ProgrammingLanguages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8771));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8904));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8906));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8906));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8907));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8907));

            migrationBuilder.UpdateData(
                table: "Technologies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationTime",
                value: new DateTime(2022, 9, 9, 12, 53, 37, 159, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ProgrammingLanguages");
        }
    }
}
