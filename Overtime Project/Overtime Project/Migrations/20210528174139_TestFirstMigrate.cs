using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Overtime_Project.Migrations
{
    public partial class TestFirstMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Kind",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Kind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Person",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Person", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Account_TB_M_Person_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Overtime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalReimburse = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    KindId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Overtime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Overtime_TB_M_Kind_KindId",
                        column: x => x.KindId,
                        principalTable: "TB_M_Kind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Overtime_TB_M_Person_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Overtime_TB_M_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TB_M_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_RoleAccount",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_RoleAccount", x => new { x.NIK, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TB_T_RoleAccount_TB_M_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_RoleAccount_TB_M_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Overtime_KindId",
                table: "TB_M_Overtime",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Overtime_NIK",
                table: "TB_M_Overtime",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Overtime_StatusId",
                table: "TB_M_Overtime",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_RoleAccount_RoleId",
                table: "TB_T_RoleAccount",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Overtime");

            migrationBuilder.DropTable(
                name: "TB_T_RoleAccount");

            migrationBuilder.DropTable(
                name: "TB_M_Kind");

            migrationBuilder.DropTable(
                name: "TB_M_Status");

            migrationBuilder.DropTable(
                name: "TB_M_Account");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_Person");
        }
    }
}
