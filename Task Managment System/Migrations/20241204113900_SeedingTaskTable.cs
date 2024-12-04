using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Managment_System.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "Description", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Lorem Ipsum", "Completed", "Important Task 1" },
                    { 2, "Lorem Ipsum", "Pending", " Not Vert Important Task" },
                    { 3, "Lorem Ipsum", "Completed", "Important Task 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 3);
        }
    }
}
