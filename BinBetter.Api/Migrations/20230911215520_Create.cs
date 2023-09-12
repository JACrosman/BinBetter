using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BinBetter.Api.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bins",
                columns: table => new
                {
                    BinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bins", x => x.BinId);
                    table.ForeignKey(
                        name: "FK_Bins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    IsInBin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK_Goals_Bins_BinId",
                        column: x => x.BinId,
                        principalTable: "Bins",
                        principalColumn: "BinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Hash", "Salt", "Username" },
                values: new object[] { 1, "Fenki@fenki.com", new byte[] { 118, 137, 66, 175, 220, 34, 206, 219, 70, 79, 103, 58, 78, 107, 11, 82, 57, 29, 158, 109, 166, 217, 209, 230, 184, 103, 216, 199, 66, 73, 179, 54, 50, 46, 84, 107, 152, 172, 138, 70, 29, 157, 203, 48, 227, 57, 240, 175, 64, 175, 64, 124, 137, 4, 99, 120, 241, 18, 104, 78, 161, 101, 247, 160 }, new byte[] { 46, 232, 138, 5, 176, 86, 155, 64, 156, 146, 35, 108, 213, 250, 153, 66 }, "Fenki" });

            migrationBuilder.InsertData(
                table: "Bins",
                columns: new[] { "BinId", "Description", "Name", "UserId" },
                values: new object[] { 1, "My first Bin", "Bin 1", 1 });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalId", "BinId", "Description", "Frequency", "IsInBin", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "My first goal", 0, false, "Goal 1", 0 },
                    { 2, 1, "My second goal", 0, false, "Goal 2", 0 },
                    { 3, 1, "My third goal", 0, false, "Goal 3", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bins_UserId",
                table: "Bins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_BinId",
                table: "Goals",
                column: "BinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Bins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
