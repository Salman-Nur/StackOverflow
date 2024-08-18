using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "134f9fd1-1f4d-4a02-bbb8-786d107bbbf0", "AQAAAAIAAYagAAAAEPpTYIJjY1WdbgzWYHa5/1nZCNcPHqaaKtEJY0SFcaizfZdjS01JO+QTFbRmv0WH+A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3699094-c536-4dfd-9574-2c8e51ae0160", "AQAAAAIAAYagAAAAENmfBGHTMbfknndyr0gNKZvGCRPQwIdzztYVMq2qdhtIvXuBbVYXb6BRHH7HFTVGVQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"),
                column: "ImageUrl",
                value: null);
        }
    }
}
