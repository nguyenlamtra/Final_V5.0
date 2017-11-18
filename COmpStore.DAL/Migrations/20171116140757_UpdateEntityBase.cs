using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace COmpStore.DAL.Migrations
{
    public partial class UpdateEntityBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "SubCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "ShoppingCartRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Publishers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "ShoppingCartRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "StoreComp",
                table: "Categories");
        }
    }
}
