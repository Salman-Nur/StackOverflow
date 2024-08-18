using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StackOverflow.Web.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Description", "DisplayName", "Email", "EmailConfirmed", "ImageUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"), 0, null, "134f9fd1-1f4d-4a02-bbb8-786d107bbbf0", null, null, "salman.qubit@gmail.com", true, null, true, null, "SALMAN.QUBIT@GMAIL.COM", "SALMAN.QUBIT", "AQAAAAIAAYagAAAAEPpTYIJjY1WdbgzWYHa5/1nZCNcPHqaaKtEJY0SFcaizfZdjS01JO+QTFbRmv0WH+A==", null, false, "SVSGSD3LW72US32MCRKAL5GMUUL23YIB", null, false, "hasan" },
                    { new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"), 0, null, "d3699094-c536-4dfd-9574-2c8e51ae0160", null, null, "salman.installer@gmail.com", true, null, true, null, "SALMAN.INSTALLER@GMAIL.COM", "SALMAN.INSTALLER", "AQAAAAIAAYagAAAAENmfBGHTMbfknndyr0gNKZvGCRPQwIdzztYVMq2qdhtIvXuBbVYXb6BRHH7HFTVGVQ==", null, false, "FLNGVXFQGWE6JKWCRVP3664L4ESTP4VU", null, false, "salman" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Answers", "Country", "Description", "ImageUrl", "Questions", "Title", "UserName" },
                values: new object[,]
                {
                    { new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"), null, "Bangladesh", "Studying at Department of CSE Islamic University, Kushtia-7003, Bangladesh", null, null, "Student", "hasan" },
                    { new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"), null, "Bangladesh", "Studying at Department of Mathematics Islamic University, Kushtia-7003, Bangladesh", null, null, "Student", "salman" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "PostQuestion", "true", new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc") },
                    { 2, "PostAnswer", "true", new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc") },
                    { 3, "PostQuestion", "true", new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6") },
                    { 4, "PostAnswer", "true", new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2dc6fb4f-3229-491f-bacb-0eb9926052c6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42af1478-6ef5-494e-aa5f-9e99b959dacc"));
        }
    }
}
