using Microsoft.EntityFrameworkCore.Migrations;

namespace Overtime_Project.Migrations
{
    public partial class AddNewDescHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TB_M_Overtime",
                newName: "DescHead");

            migrationBuilder.AddColumn<string>(
                name: "DescEmp",
                table: "TB_M_Overtime",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescEmp",
                table: "TB_M_Overtime");

            migrationBuilder.RenameColumn(
                name: "DescHead",
                table: "TB_M_Overtime",
                newName: "Description");
        }
    }
}
