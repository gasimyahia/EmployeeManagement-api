using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TweetBook4.Migrations
{
    public partial class AddEmployeeAndDeptTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBrith = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DeptId", "DeptName" },
                values: new object[] { 1, "HR" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DeptId", "DeptName" },
                values: new object[] { 2, "IT" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBrith", "DeptId", "Email", "FirstName", "Gender", "LastName", "PhotoPath" },
                values: new object[] { 1, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ahmed@gmail.com", "ahmed", 0, "omer", "Image/emp.jpg" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBrith", "DeptId", "Email", "FirstName", "Gender", "LastName", "PhotoPath" },
                values: new object[] { 2, new DateTime(1994, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "oasman@gmail.com", "oasman", 0, "ali", "Image/emp.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptId",
                table: "Employees",
                column: "DeptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
