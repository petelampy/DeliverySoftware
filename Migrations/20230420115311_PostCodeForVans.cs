using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class PostCodeForVans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepotPostCode",
                table: "Vans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "921095c8-38f1-4bbc-ae08-2582134f7e02", "AQAAAAIAAYagAAAAEEuQ+qgsC9efh3HBwzw/VrkATl+SyR5VTDatKaiCseLcrh77kevwVdJ8Sj5OXt1Ejg==", "5a681b93-798b-47f7-b671-8d138ef1d2d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52fac4db-3d93-46bc-81ef-f33aed467669",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb3ecdc3-d7a5-400b-a152-dc045c89c267", "AQAAAAIAAYagAAAAEG85v+j3GyX8W6QWvJJXZg9qYIwT1z6nUIFUWyOPBV/Sp1Xt7ApHOTMtLT/Ur1Vzug==", "4e49cd12-29c2-4ddc-a9e1-b14fa56b4eb1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f649f970-0762-423f-8247-5b9ef0fc3265", "AQAAAAIAAYagAAAAEAob8SGv6f6jEnCbLUX1eKBMe0Halt6D0lZYj0P7Inl0C0YWb8ElYLl1iPAjS5Ubpw==", "27a32348-9443-4c76-8fd6-c371706db1d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b801cd8-677c-43c3-8caf-8c0ed791c233", "AQAAAAIAAYagAAAAEG9mQfTx/R1fHpziX1uXILwNhwXScjppmSml3qdJLgMLkTvSvLppXPgBPOUXdLdLOw==", "dbaa0977-54e1-4221-b8fe-bfa838c60aa7" });

            migrationBuilder.UpdateData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 4, 20, 12, 53, 11, 587, DateTimeKind.Local).AddTicks(6458));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UID",
                value: new Guid("2546381e-8cba-4b7f-8250-e7ce4ecc16b9"));

            migrationBuilder.UpdateData(
                table: "Vans",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepotPostCode",
                value: "DE24 9QX");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepotPostCode",
                table: "Vans");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0778bac-54bc-4acf-8a8d-bbda4f030269", "AQAAAAIAAYagAAAAEM2URZ1uWsnzu7wEbH2T3lhaO3Eb8oIoDyO1viPwJ4sumuZz5M5SDiWMKvZ5HFVsdA==", "23ba4aea-287b-412a-8b0b-283d4faf994f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52fac4db-3d93-46bc-81ef-f33aed467669",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aca93244-a896-4dba-b98b-a3ed0fe9a40a", "AQAAAAIAAYagAAAAEFBO3OjDAGk0SRerZZU6d2F0SDH/MaFTXhYG51U35lZloko73FzhWxmvYAucigLsBA==", "ae2960ac-d6e4-4f20-a436-0132d733b06a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c22d31d-3ee4-4879-9783-9857d4e9e030", "AQAAAAIAAYagAAAAEJiR++CyYvTwlQmVeYFxnVN8wQ5Qps82ShVyO+2RPwFZO+owWUAhUF1kmoeeItxCJw==", "ffc3f231-28cb-418e-abff-f697ec0bad34" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5fe5a4cf-421c-4684-b7c6-ddeaae02dcea", "AQAAAAIAAYagAAAAEDpaYaSuIjf2PP+zu5lAArK2jvRINyx3V/5sVst4C+ziInM4FrTE4fPA/ovMNlBJCA==", "b2bc1712-c6f0-4729-8165-4e38bf51b294" });

            migrationBuilder.UpdateData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 4, 19, 21, 7, 38, 942, DateTimeKind.Local).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UID",
                value: new Guid("0809ccb1-61f2-4ccc-9296-b24f6b138803"));
        }
    }
}
