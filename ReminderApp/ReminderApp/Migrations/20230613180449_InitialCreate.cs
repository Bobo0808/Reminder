using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReminderApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    U_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.U_Id);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    R_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_Id = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Add_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finish = table.Column<bool>(type: "bit", nullable: false),
                    UserU_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.R_Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Users_U_Id",
                        column: x => x.U_Id,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reminders_Users_UserU_Id",
                        column: x => x.UserU_Id,
                        principalTable: "Users",
                        principalColumn: "U_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_U_Id",
                table: "Reminders",
                column: "U_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserU_Id",
                table: "Reminders",
                column: "UserU_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
