using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OSS.Migrations
{
    public partial class QuestionAnswerFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionAnswer_QuestionAnswerId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionAnswerId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "QuestionAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionAnswerId",
                table: "Answers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionAnswerId",
                table: "Answers",
                column: "QuestionAnswerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionAnswer_QuestionAnswerId",
                table: "Answers",
                column: "QuestionAnswerId",
                principalTable: "QuestionAnswer",
                principalColumn: "QuestionAnswerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionAnswer_QuestionAnswerId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionAnswerId",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "QuestionAnswer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionAnswerId",
                table: "Answers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionAnswerId",
                table: "Answers",
                column: "QuestionAnswerId",
                unique: true,
                filter: "[QuestionAnswerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionAnswer_QuestionAnswerId",
                table: "Answers",
                column: "QuestionAnswerId",
                principalTable: "QuestionAnswer",
                principalColumn: "QuestionAnswerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
