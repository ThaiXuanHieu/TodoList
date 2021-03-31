using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Api.Migrations
{
    public partial class AddFieldIsImportantToTableTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsImportant",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "b1aad00f-2cca-42ed-a4dc-0eba4a0c9d17");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c57b1711-99de-48df-b31d-57337c1a2c09", "AQAAAAEAACcQAAAAELkdInxUdj1vlavvMO8h7g8zMWX9V67vFaXaqgwlkjEJVJ39+/TumGpxtahsrNHx3g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImportant",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "265f0a1f-b64d-4a11-88e5-a8e95db836f3");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e03f13f0-8f57-4cf4-9880-c22c09558e62", "AQAAAAEAACcQAAAAEAEtRdsjp1llQuQeznNVkR1iiXyOwYsFYWyzUrWODAiYvFzzJJNw9qDisupjjsUltw==" });
        }
    }
}
