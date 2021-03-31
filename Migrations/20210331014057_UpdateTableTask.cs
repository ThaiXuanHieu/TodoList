using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Api.Migrations
{
    public partial class UpdateTableTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "48aa13fb-e0a4-420a-9e55-0348f1665754");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8163b013-97cc-436b-8692-11ebdcda035b", "AQAAAAEAACcQAAAAENlui2I9c7lEvUvCU30Je3+hkE6JDdub6mH3mZFX6Fhq1GSl0FJ4I3RgOPCWMPrkJA==" });
        }
    }
}
