using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace COmpStore.DAL.Migrations
{
    public partial class RemainingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                schema: "StoreComp",
                table: "Orders",
                type: "money",
                nullable: true,
                computedColumnSql: "StoreComp.GetOrderTotal([Id])",
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                schema: "StoreComp",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true,
                oldComputedColumnSql: "StoreComp.GetOrderTotal([Id])");
        }
    }
}
