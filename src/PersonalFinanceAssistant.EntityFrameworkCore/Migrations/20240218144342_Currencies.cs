using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalFinanceAssistant.Migrations
{
    /// <inheritdoc />
    public partial class Currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "FinanceAccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Alpha3Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccounts_CurrencyId",
                table: "FinanceAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "UX_Currencies_Alpha3Code",
                table: "Currencies",
                column: "Alpha3Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Currencies_Name",
                table: "Currencies",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceAccounts_Currencies_CurrencyId",
                table: "FinanceAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceAccounts_Currencies_CurrencyId",
                table: "FinanceAccounts");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_FinanceAccounts_CurrencyId",
                table: "FinanceAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "FinanceAccounts");
        }
    }
}
