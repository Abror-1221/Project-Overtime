using Microsoft.EntityFrameworkCore.Migrations;

namespace Overtime_Project.Migrations
{
    public partial class updateColumnOvertime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Overtime_TB_M_Kind_KindId",
                table: "TB_M_Overtime");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TB_M_Overtime");

            migrationBuilder.AlterColumn<int>(
                name: "KindId",
                table: "TB_M_Overtime",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Overtime_TB_M_Kind_KindId",
                table: "TB_M_Overtime",
                column: "KindId",
                principalTable: "TB_M_Kind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Overtime_TB_M_Kind_KindId",
                table: "TB_M_Overtime");

            migrationBuilder.AlterColumn<int>(
                name: "KindId",
                table: "TB_M_Overtime",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "TB_M_Overtime",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Overtime_TB_M_Kind_KindId",
                table: "TB_M_Overtime",
                column: "KindId",
                principalTable: "TB_M_Kind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
