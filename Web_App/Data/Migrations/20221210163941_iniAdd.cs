using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppCrypto.Data.Migrations
{
    /// <inheritdoc />
    public partial class iniAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caesars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    PlainText = table.Column<string>(type: "TEXT", nullable: false),
                    CypherText = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caesars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caesars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyExchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    P = table.Column<ulong>(type: "INTEGER", nullable: false),
                    G = table.Column<ulong>(type: "INTEGER", nullable: false),
                    ASecret = table.Column<ulong>(type: "INTEGER", nullable: false),
                    BSecret = table.Column<ulong>(type: "INTEGER", nullable: false),
                    CommonSecret = table.Column<ulong>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyExchange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyExchange_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RsaKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Cypher = table.Column<string>(type: "TEXT", nullable: false),
                    KeyLength = table.Column<string>(type: "TEXT", nullable: false),
                    PPrime = table.Column<ulong>(type: "INTEGER", nullable: false),
                    QPrime = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Exponent = table.Column<ulong>(type: "INTEGER", nullable: false),
                    RsaCypher = table.Column<string>(type: "TEXT", nullable: false),
                    KeySecret = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RsaKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RsaKeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caesars_UserId",
                table: "Caesars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyExchange_UserId",
                table: "KeyExchange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RsaKeys_UserId",
                table: "RsaKeys",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caesars");

            migrationBuilder.DropTable(
                name: "KeyExchange");

            migrationBuilder.DropTable(
                name: "RsaKeys");
        }
    }
}
