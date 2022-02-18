using Microsoft.EntityFrameworkCore.Migrations;

namespace MZHealthManagement.Server.Data.Migrations
{
    public partial class addDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Departments_DepartmentID",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_DepartmentID",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Staffs");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_DepartmentId",
                table: "Staffs",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Departments_DepartmentId",
                table: "Staffs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Departments_DepartmentId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_DepartmentId",
                table: "Staffs");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_DepartmentID",
                table: "Staffs",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Departments_DepartmentID",
                table: "Staffs",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
