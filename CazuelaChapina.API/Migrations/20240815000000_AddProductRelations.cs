using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CazuelaChapina.API.Migrations
{
    public partial class AddProductRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TamaleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamaleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fillings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fillings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wrappers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wrappers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpiceLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpiceLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeverageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeverageSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sweeteners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sweeteners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Toppings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toppings", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "TamaleTypeId",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FillingId",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrapperId",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpiceLevelId",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "MasaType",
                table: "Tamales");
            migrationBuilder.DropColumn(
                name: "Filling",
                table: "Tamales");
            migrationBuilder.DropColumn(
                name: "Wrapper",
                table: "Tamales");
            migrationBuilder.DropColumn(
                name: "SpiceLevel",
                table: "Tamales");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SweetenerId",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Beverages");
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Beverages");
            migrationBuilder.DropColumn(
                name: "Sweetener",
                table: "Beverages");
            migrationBuilder.DropColumn(
                name: "Toppings",
                table: "Beverages");

            migrationBuilder.CreateTable(
                name: "BeverageToppings",
                columns: table => new
                {
                    BeverageId = table.Column<int>(type: "integer", nullable: false),
                    ToppingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageToppings", x => new { x.BeverageId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_BeverageToppings_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeverageToppings_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboItemTamales",
                columns: table => new
                {
                    ComboId = table.Column<int>(type: "integer", nullable: false),
                    TamaleId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItemTamales", x => new { x.ComboId, x.TamaleId });
                    table.ForeignKey(
                        name: "FK_ComboItemTamales_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboItemTamales_Tamales_TamaleId",
                        column: x => x.TamaleId,
                        principalTable: "Tamales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboItemBeverages",
                columns: table => new
                {
                    ComboId = table.Column<int>(type: "integer", nullable: false),
                    BeverageId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItemBeverages", x => new { x.ComboId, x.BeverageId });
                    table.ForeignKey(
                        name: "FK_ComboItemBeverages_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboItemBeverages_Combos_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tamales_TamaleTypeId",
                table: "Tamales",
                column: "TamaleTypeId");
            migrationBuilder.CreateIndex(
                name: "IX_Tamales_FillingId",
                table: "Tamales",
                column: "FillingId");
            migrationBuilder.CreateIndex(
                name: "IX_Tamales_WrapperId",
                table: "Tamales",
                column: "WrapperId");
            migrationBuilder.CreateIndex(
                name: "IX_Tamales_SpiceLevelId",
                table: "Tamales",
                column: "SpiceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Beverages_TypeId",
                table: "Beverages",
                column: "TypeId");
            migrationBuilder.CreateIndex(
                name: "IX_Beverages_SizeId",
                table: "Beverages",
                column: "SizeId");
            migrationBuilder.CreateIndex(
                name: "IX_Beverages_SweetenerId",
                table: "Beverages",
                column: "SweetenerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeverageToppings_ToppingId",
                table: "BeverageToppings",
                column: "ToppingId");
            migrationBuilder.CreateIndex(
                name: "IX_ComboItemTamales_TamaleId",
                table: "ComboItemTamales",
                column: "TamaleId");
            migrationBuilder.CreateIndex(
                name: "IX_ComboItemBeverages_BeverageId",
                table: "ComboItemBeverages",
                column: "BeverageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tamales_Fillings_FillingId",
                table: "Tamales",
                column: "FillingId",
                principalTable: "Fillings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Tamales_SpiceLevels_SpiceLevelId",
                table: "Tamales",
                column: "SpiceLevelId",
                principalTable: "SpiceLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Tamales_TamaleTypes_TamaleTypeId",
                table: "Tamales",
                column: "TamaleTypeId",
                principalTable: "TamaleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Tamales_Wrappers_WrapperId",
                table: "Tamales",
                column: "WrapperId",
                principalTable: "Wrappers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beverages_BeverageSizes_SizeId",
                table: "Beverages",
                column: "SizeId",
                principalTable: "BeverageSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Beverages_BeverageTypes_TypeId",
                table: "Beverages",
                column: "TypeId",
                principalTable: "BeverageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Beverages_Sweeteners_SweetenerId",
                table: "Beverages",
                column: "SweetenerId",
                principalTable: "Sweeteners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tamales_Fillings_FillingId",
                table: "Tamales");
            migrationBuilder.DropForeignKey(
                name: "FK_Tamales_SpiceLevels_SpiceLevelId",
                table: "Tamales");
            migrationBuilder.DropForeignKey(
                name: "FK_Tamales_TamaleTypes_TamaleTypeId",
                table: "Tamales");
            migrationBuilder.DropForeignKey(
                name: "FK_Tamales_Wrappers_WrapperId",
                table: "Tamales");
            migrationBuilder.DropForeignKey(
                name: "FK_Beverages_BeverageSizes_SizeId",
                table: "Beverages");
            migrationBuilder.DropForeignKey(
                name: "FK_Beverages_BeverageTypes_TypeId",
                table: "Beverages");
            migrationBuilder.DropForeignKey(
                name: "FK_Beverages_Sweeteners_SweetenerId",
                table: "Beverages");

            migrationBuilder.DropTable(
                name: "BeverageToppings");
            migrationBuilder.DropTable(
                name: "ComboItemTamales");
            migrationBuilder.DropTable(
                name: "ComboItemBeverages");
            migrationBuilder.DropTable(
                name: "Fillings");
            migrationBuilder.DropTable(
                name: "SpiceLevels");
            migrationBuilder.DropTable(
                name: "TamaleTypes");
            migrationBuilder.DropTable(
                name: "Wrappers");
            migrationBuilder.DropTable(
                name: "BeverageSizes");
            migrationBuilder.DropTable(
                name: "BeverageTypes");
            migrationBuilder.DropTable(
                name: "Sweeteners");
            migrationBuilder.DropTable(
                name: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Tamales_TamaleTypeId",
                table: "Tamales");
            migrationBuilder.DropIndex(
                name: "IX_Tamales_FillingId",
                table: "Tamales");
            migrationBuilder.DropIndex(
                name: "IX_Tamales_WrapperId",
                table: "Tamales");
            migrationBuilder.DropIndex(
                name: "IX_Tamales_SpiceLevelId",
                table: "Tamales");
            migrationBuilder.DropIndex(
                name: "IX_Beverages_TypeId",
                table: "Beverages");
            migrationBuilder.DropIndex(
                name: "IX_Beverages_SizeId",
                table: "Beverages");
            migrationBuilder.DropIndex(
                name: "IX_Beverages_SweetenerId",
                table: "Beverages");

            migrationBuilder.DropColumn(
                name: "TamaleTypeId",
                table: "Tamales");
            migrationBuilder.DropColumn(
                name: "FillingId",
                table: "Tamales");
            migrationBuilder.DropColumn(
                name: "WrapperId",
                table: "Tamales");
            migrationBuilder.DropColumn(
                name: "SpiceLevelId",
                table: "Tamales");
            migrationBuilder.AddColumn<int>(
                name: "MasaType",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "Filling",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "Wrapper",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "SpiceLevel",
                table: "Tamales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Beverages");
            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Beverages");
            migrationBuilder.DropColumn(
                name: "SweetenerId",
                table: "Beverages");
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "Sweetener",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "Toppings",
                table: "Beverages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
