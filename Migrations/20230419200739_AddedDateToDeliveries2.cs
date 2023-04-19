using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateToDeliveries2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3929b31-b685-44bf-ac71-98e5e3c55d06", "AQAAAAIAAYagAAAAEBpRjXR9+DWnANyKMEjx09ihiKlLPPM45pJECx40l/9/no9iq4nKl/fbrsmgmliLlg==", "f1382060-af81-462b-aa95-9f9f3f9e62bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52fac4db-3d93-46bc-81ef-f33aed467669",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9267654-cdcb-4fbb-ba69-f415a8e756b0", "AQAAAAIAAYagAAAAEK9zBNbYFJSQKmEjSlCd19b1Kb6jspOiFu/5KrSmijMDmqLZV8dIZFB57PdQxyxDGA==", "151c723c-adae-4a92-b480-8810989c8a63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ae561fd-097a-40f1-b50a-8ff5991283d2", "AQAAAAIAAYagAAAAEO1q1uAQjDojgvDFpLRhoWkiBhob08cLnZxNIPtdHMmmjhaTHfiJWF2bhqT1LvLqZg==", "9101752d-2ccf-417e-a68f-e981881a3d8c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3f2457f-7b03-48fd-a7d4-0ae19d961a7e", "AQAAAAIAAYagAAAAEJhDrvQdbWqnWLcJlcaIHpFgWUxmEb0/Y+AJNBOQgAn1kETGdk3dJwBUOCzcnyjNGQ==", "5b52ffb3-831c-4984-b7ba-952836cdc2c3" });

            migrationBuilder.UpdateData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UID",
                value: new Guid("ca16ba14-4709-4ad1-8e58-398bbfc6c5a8"));
        }
    }
}
