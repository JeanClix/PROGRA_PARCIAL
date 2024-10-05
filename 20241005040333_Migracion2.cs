using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PROGRA_PARCIAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEnvio",
                table: "t_remesas");

            migrationBuilder.DropColumn(
                name: "FechaRecepcion",
                table: "t_remesas");

            migrationBuilder.RenameColumn(
                name: "Remitente",
                table: "t_remesas",
                newName: "PaisOrigen");

            migrationBuilder.RenameColumn(
                name: "PaísDestino",
                table: "t_remesas",
                newName: "PaisDestino");

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "t_remesas",
                newName: "MontoFinal");

            migrationBuilder.RenameColumn(
                name: "Destinatario",
                table: "t_remesas",
                newName: "NombreRemitente");

            migrationBuilder.AlterColumn<decimal>(
                name: "TasaCambio",
                table: "t_remesas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "t_remesas",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoEnviado",
                table: "t_remesas",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NombreDestinario",
                table: "t_remesas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoEnviado",
                table: "t_remesas");

            migrationBuilder.DropColumn(
                name: "NombreDestinario",
                table: "t_remesas");

            migrationBuilder.RenameColumn(
                name: "PaisOrigen",
                table: "t_remesas",
                newName: "Remitente");

            migrationBuilder.RenameColumn(
                name: "PaisDestino",
                table: "t_remesas",
                newName: "PaísDestino");

            migrationBuilder.RenameColumn(
                name: "NombreRemitente",
                table: "t_remesas",
                newName: "Destinatario");

            migrationBuilder.RenameColumn(
                name: "MontoFinal",
                table: "t_remesas",
                newName: "Monto");

            migrationBuilder.AlterColumn<decimal>(
                name: "TasaCambio",
                table: "t_remesas",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "t_remesas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEnvio",
                table: "t_remesas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRecepcion",
                table: "t_remesas",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
