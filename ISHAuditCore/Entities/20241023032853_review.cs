using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISHAuditCore.Entities
{
    /// <inheritdoc />
    public partial class review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "review_queue",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "review_queue",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "review_queue",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "review_queue",
                newName: "Id");
        }
    }
}
