using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTareas.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPropiedadesAvanzadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Tareas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Etiquetas",
                table: "Tareas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Tareas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Tareas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TiempoEstimado",
                table: "Tareas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlReferencia",
                table: "Tareas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Etiquetas",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "TiempoEstimado",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "UrlReferencia",
                table: "Tareas");
        }
    }
}
