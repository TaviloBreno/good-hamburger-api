using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoodHamburger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "DisplayOrder", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { "fries", "SideDish", "Batata frita crocante (150g)", 1, true, "Batata Frita", 2.00m },
                    { "soda", "Drink", "Lata 350ml", 1, true, "Refrigerante", 2.50m },
                    { "xbacon", "Sandwich", "Hambúrguer com bacon crocante e queijo", 3, true, "X Bacon", 7.00m },
                    { "xburger", "Sandwich", "Hambúrguer 150g com queijo derretido", 1, true, "X Burger", 5.00m },
                    { "xegg", "Sandwich", "Hambúrguer com ovo frito e queijo", 2, true, "X Egg", 4.50m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AppliedDiscount", "CreatedAt", "Discount", "Drink", "Sandwich", "SideDish", "Subtotal", "Total", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Combo", new DateTime(2026, 4, 13, 5, 43, 48, 155, DateTimeKind.Utc).AddTicks(5095), 1.90m, "Soda", "XBurger", "Fries", 9.50m, 7.60m, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "SandwichDrink", new DateTime(2026, 4, 15, 5, 43, 48, 155, DateTimeKind.Utc).AddTicks(5105), 1.425m, "Soda", "XBacon", null, 9.50m, 8.075m, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "SandwichSide", new DateTime(2026, 4, 16, 5, 43, 48, 155, DateTimeKind.Utc).AddTicks(5108), 0.65m, null, "XEgg", "Fries", 6.50m, 5.85m, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "None", new DateTime(2026, 4, 17, 5, 43, 48, 155, DateTimeKind.Utc).AddTicks(5110), 0m, null, "XBurger", null, 5.00m, 5.00m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: "fries");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: "soda");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: "xbacon");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: "xburger");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: "xegg");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}
