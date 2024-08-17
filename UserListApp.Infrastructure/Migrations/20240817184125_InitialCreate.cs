using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserListApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "Alice", "123-456-7890" },
                    { 2, "bob@example.com", "Bob", "234-567-8901" },
                    { 3, "charlie@example.com", "Charlie", "345-678-9012" },
                    { 4, "david@example.com", "David", "456-789-0123" },
                    { 5, "eva@example.com", "Eva", "567-890-1234" },
                    { 6, "frank@example.com", "Frank", "678-901-2345" },
                    { 7, "grace@example.com", "Grace", "789-012-3456" },
                    { 8, "hank@example.com", "Hank", "890-123-4567" },
                    { 9, "ivy@example.com", "Ivy", "901-234-5678" },
                    { 10, "jack@example.com", "Jack", "012-345-6789" },
                    { 11, "kara@example.com", "Kara", "123-456-7891" },
                    { 12, "leo@example.com", "Leo", "234-567-8902" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
