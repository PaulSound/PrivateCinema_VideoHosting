using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateCinema_VideoHosting.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_signInList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__signInList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_userList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LibraryNumb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignInId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__userList", x => x.Id);
                    table.ForeignKey(
                        name: "FK__userList__signInList_SignInId",
                        column: x => x.SignInId,
                        principalTable: "_signInList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_videoList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__videoList", x => x.Id);
                    table.ForeignKey(
                        name: "FK__videoList__userList_userId",
                        column: x => x.userId,
                        principalTable: "_userList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__userList_SignInId",
                table: "_userList",
                column: "SignInId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__videoList_userId",
                table: "_videoList",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_videoList");

            migrationBuilder.DropTable(
                name: "_userList");

            migrationBuilder.DropTable(
                name: "_signInList");
        }
    }
}
