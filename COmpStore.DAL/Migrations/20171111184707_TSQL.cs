using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace COmpStore.DAL.Migrations
{
    public partial class TSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = "CREATE FUNCTION StoreComp.GetOrderTotal ( @OrderId INT ) " +
              "RETURNS MONEY WITH SCHEMABINDING " +
              "BEGIN " +
              "DECLARE @Result MONEY; " +
              "SELECT @Result = SUM([Quantity]*[UnitCost]) FROM StoreComp.OrderDetails " +
              " WHERE OrderId = @OrderId; RETURN @Result END";
            migrationBuilder.Sql(sql);

            sql = "CREATE PROCEDURE [StoreComp].[PurchaseItemsInCart](@customerId INT = 0, @orderId INT OUTPUT) AS BEGIN " +
              " SET NOCOUNT ON; " +
              " INSERT INTO StoreComp.Orders (CustomerId, OrderDate, ShipDate) " +
              "    VALUES(@customerId, GETDATE(), GETDATE()); " +
              " SET @orderId = SCOPE_IDENTITY(); " +
              " DECLARE @TranName VARCHAR(20);SELECT @TranName = 'CommitOrder'; " +
              "   BEGIN TRANSACTION @TranName; " +
              "   BEGIN TRY " +
              "       INSERT INTO StoreComp.OrderDetails (OrderId, ProductId, Quantity, UnitCost) " +
              "       SELECT @orderId, ProductId, Quantity, p.CurrentPrice " +
              "       FROM StoreComp.ShoppingCartRecords scr " +
              "          INNER JOIN StoreComp.Products p ON p.Id = scr.ProductId " +
              "       WHERE CustomerId = @customerId; " +
              "       DELETE FROM StoreComp.ShoppingCartRecords WHERE CustomerId = @customerId; " +
              "       COMMIT TRANSACTION @TranName; " +
              "   END TRY " +
              "   BEGIN CATCH " +
              "       ROLLBACK TRANSACTION @TranName; " +
              "       SET @OrderId = -1; " +
              "   END CATCH; " +
              "END;";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION [StoreComp].[GetOrderTotal]");
            migrationBuilder.Sql("DROP PROCEDURE [StoreComp].[PurchaseItemsInCart]");
        }
    }
}
