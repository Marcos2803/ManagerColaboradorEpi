using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestao.EpiData.Migrations
{
    /// <inheritdoc />
    public partial class epi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeEstoque",
                table: "Epis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuantidadeEstoque",
                table: "Epis",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }
    }
}
