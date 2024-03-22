using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogDb.API.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Almoço', 'almoco.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Bebidas Acoólicas', 'bebidas_acoolicas.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Bebidas Frias', 'bebidas_frias.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Bebidas Quentes', 'bebidas_quentes.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Café da Manhã', 'cafe_da_manha.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Janta', 'janta.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Lanches', 'lanches.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Petiscos', 'petiscos.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Sobremesas', 'sobremesas.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Sopas e Caldos', 'sopas_e_caldos.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Vegetarianos e Veganos', 'vegetarianos_e_veganos.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
