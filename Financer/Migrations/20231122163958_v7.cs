using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancerAPI.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Extratos",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Extratos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Extratos",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Extratos",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Extratos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Extratos",
                newName: "Data");
        }
    }
}
