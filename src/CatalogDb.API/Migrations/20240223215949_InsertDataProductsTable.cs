﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogDb.API.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Coca-Cola Diet','Refrigerante de Cola 350 ml',5.45, 'cocacola.jpg', 50, GETDATE(), 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Lanche de Atum', 'Lanche de Atum com maionese', 8.50, 'atum.jpg', 10, GETDATE(), 2)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
               "VALUES('Pudim 100 g', 'Pudim de leite condensado 100g', 6.75, 'pudim.jpg', 20, GETDATE(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
