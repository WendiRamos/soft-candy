using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Delivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDelivery_Delivery_DeliveryId",
                table: "ItemDelivery");

            migrationBuilder.DropIndex(
                name: "IX_ItemDelivery_DeliveryId",
                table: "ItemDelivery");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "ItemDelivery");

            migrationBuilder.AlterColumn<string>(
                name: "DataHoraRecebimento",
                table: "Delivery",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "NomeCliente",
                table: "Delivery",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorFrete",
                table: "Delivery",
                type: "decimal(8, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ItemDelivery_IdDelivery",
                table: "ItemDelivery",
                column: "IdDelivery");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDelivery_Delivery_IdDelivery",
                table: "ItemDelivery",
                column: "IdDelivery",
                principalTable: "Delivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDelivery_Delivery_IdDelivery",
                table: "ItemDelivery");

            migrationBuilder.DropIndex(
                name: "IX_ItemDelivery_IdDelivery",
                table: "ItemDelivery");

            migrationBuilder.DropColumn(
                name: "NomeCliente",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "ValorFrete",
                table: "Delivery");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "ItemDelivery",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraRecebimento",
                table: "Delivery",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemDelivery_DeliveryId",
                table: "ItemDelivery",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDelivery_Delivery_DeliveryId",
                table: "ItemDelivery",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
