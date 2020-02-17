using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace MyBoards.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsibles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardLists_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CardListId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_CardLists_CardListId",
                        column: x => x.CardListId,
                        principalTable: "CardLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardResponsible",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false),
                    ResponsibleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardResponsible", x => new { x.CardId, x.ResponsibleId });
                    table.ForeignKey(
                        name: "FK_CardResponsible_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardResponsible_Responsibles_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "Responsibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTags",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTags", x => new { x.CardId, x.TagId });
                    table.ForeignKey(
                        name: "FK_CardTags_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, null, "Principal" });

            migrationBuilder.InsertData(
                table: "Responsibles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Samuel" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "En desarrollo" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[] { 1, "Amarillo", "Issue" });

            migrationBuilder.InsertData(
                table: "CardLists",
                columns: new[] { "Id", "BoardId", "Name" },
                values: new object[] { 1, 1, "Infinity Seguros" });

            migrationBuilder.CreateIndex(
                name: "IX_CardLists_BoardId",
                table: "CardLists",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardResponsible_ResponsibleId",
                table: "CardResponsible",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardListId",
                table: "Cards",
                column: "CardListId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_StateId",
                table: "Cards",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTags_TagId",
                table: "CardTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardResponsible");

            migrationBuilder.DropTable(
                name: "CardTags");

            migrationBuilder.DropTable(
                name: "Responsibles");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "CardLists");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
