using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagement.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserIdToCustomerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "UserId");
        }
    }
}
