using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmerzonArticles.Migrations
{
    public partial class AddingExpirationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_People_PersonId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Units_UnitId",
                table: "Articles");

            migrationBuilder.AlterColumn<long>(
                name: "UnitId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PersonId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_People_PersonId",
                table: "Articles",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Units_UnitId",
                table: "Articles",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_People_PersonId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Units_UnitId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Articles");

            migrationBuilder.AlterColumn<long>(
                name: "UnitId",
                table: "Articles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PersonId",
                table: "Articles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_People_PersonId",
                table: "Articles",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Units_UnitId",
                table: "Articles",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
