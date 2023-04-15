using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalFieldsAndSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "HouseNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Forename",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfPackages = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VanUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAssignedToDelivery = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Forename", "HouseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostCode", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { "16776aef-a5ff-424d-be7e-ea3fd591ce90", 0, null, "38c1da5e-a21e-446b-8b1e-ecf30cbc6619", "petelampy@gmail.com", false, "Din", null, false, null, null, null, null, null, false, null, "5275e674-79ae-416f-b409-7f49aaed3ecc", "Djarin", false, null, 2 },
                    { "751e25c8-68ba-49dc-b1de-15fcb0bf210f", 0, "306 Sinfin Avenue, Derby, UK", "25c870cd-a7a3-4f9f-8173-1e5bfffd4ec6", "petelampy@gmail.com", false, "Annika", 306, false, null, null, null, null, null, false, "DE24 9QX", "ffbca79f-52b4-4c06-9436-7b9b04cd459f", "Hansen", false, null, 1 },
                    { "ca6f5584-527f-4820-802f-bd329352c3e5", 0, null, "f5405056-fd56-46c4-a847-609f52cde9ea", "petelampy@gmail.com", false, "William", null, false, null, null, null, null, null, false, null, "6377b8da-38a6-4839-98f5-c89b623d5dfc", "Riker", false, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "Id", "NumberOfPackages", "UID", "VanUID" },
                values: new object[] { 1, 1, new Guid("3f3cf1cd-131d-4d05-93b9-21dbd082fcac"), new Guid("f2b87d27-d4d8-425e-8df8-b3e7bf7f6460") });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CustomerUID", "DeliveryUID", "Description", "IsAssignedToDelivery", "Size", "UID" },
                values: new object[] { 1, new Guid("751e25c8-68ba-49dc-b1de-15fcb0bf210f"), new Guid("3f3cf1cd-131d-4d05-93b9-21dbd082fcac"), "Big box of bolts", true, 10, new Guid("721a28e5-03b7-4c9a-863a-53d58e23d64a") });

            migrationBuilder.InsertData(
                table: "Vans",
                columns: new[] { "Id", "Capacity", "DriverUID", "Registration", "UID" },
                values: new object[] { 1, 50, new Guid("16776aef-a5ff-424d-be7e-ea3fd591ce90"), "WK04 DHC", new Guid("f2b87d27-d4d8-425e-8df8-b3e7bf7f6460") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5");

            migrationBuilder.DeleteData(
                table: "Vans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Forename",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HouseNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
