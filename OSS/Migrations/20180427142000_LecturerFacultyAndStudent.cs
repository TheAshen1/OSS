using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OSS.Migrations
{
    public partial class LecturerFacultyAndStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentIP",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Lecturers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Initials",
                table: "Lecturers",
                nullable: true,
                computedColumnSql: "SUBSTRING(FirstName,1,1) + '.' + SUBSTRING(MiddleName,1,1) + '. ' + [LastName]");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_FacultyId",
                table: "Lecturers",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Faculties_FacultyId",
                table: "Lecturers",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Faculties_FacultyId",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_FacultyId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Initials",
                table: "Lecturers");

            migrationBuilder.AddColumn<string>(
                name: "StudentIP",
                table: "Students",
                nullable: true);
        }
    }
}
