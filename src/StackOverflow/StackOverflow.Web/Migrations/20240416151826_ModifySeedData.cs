using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Web.Migrations
{
    /// <inheritdoc />
    public partial class ModifySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0217e256-c509-4548-a00d-816ba1207f28", "AQAAAAIAAYagAAAAEFu+id96oxKWLTBq5GwCwrRtcSNZnJSdP1gH5Rgq2ip0EYTt8pgJFusCr+CljYYVAw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "956c2ce2-1265-4212-8a9e-404fc10883e9", "AQAAAAIAAYagAAAAEMIfraAfDBytE5hsxm1wmbjvhK6LCDXOZErtUiyfb03SZETqCyz1PCfD8s8iFECOrw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"),
                column: "ImageUrl",
                value: "images/72b73e67-2e3d-4088-9c09-dbdbabd17eb5_salman2.jpg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"),
                column: "ImageUrl",
                value: "images/5b7f4f24-f43d-45d1-b0eb-d6c41d5dde95_salman1.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e1d34bc-5c32-42f0-a85e-78dee9f6a4ff", "AQAAAAIAAYagAAAAEOr3tBAbFIxyumkqheHS5xzNFMDd8VenDxDtTNAYpPStvbwJGd0X2jQ8xLJWuJQAaA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "44eb4750-7fcf-4711-b683-e96d65b2c431", "AQAAAAIAAYagAAAAEF6RIqKt41HYrmHPncxBlyZiaE77zH81pQrdKF5WswTkRocrJOEUEX7cx7QIuhXcrg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"),
                column: "ImageUrl",
                value: "images/1737a119-8183-4b9f-b6fa-f74534ef825f_16990140.jpg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"),
                column: "ImageUrl",
                value: "images/9aadd8b6-a1fe-451d-b7b3-755153682b22_salman.jpg");
        }
    }
}
