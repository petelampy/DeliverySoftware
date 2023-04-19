using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateToDeliveries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Deliveries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Deliveries");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92fb2a53-69bb-4243-8cab-710b533ddee8", "AQAAAAIAAYagAAAAEP2RMhiG3ctDeFhZdo7HyZDUt8Q7h/9PS6sfShPLf3U0fG3kXiUXJZ6BAtGFd4df+A==", "29518f88-0a04-45c2-b708-1c1d3d040191" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52fac4db-3d93-46bc-81ef-f33aed467669",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61afc09e-f291-413d-8feb-3f6d311ea391", "AQAAAAIAAYagAAAAEAlcWm6kEF6Nsgu1CfcJjDfmJ/IfYKKe5aa6hwmjn1KrBKOxgjlxEaTs+yJjzd4dQg==", "4902831b-58ab-4fdd-985b-ef2f874f3724" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a22d4011-262e-45de-a12c-39ec30f3d98c", "AQAAAAIAAYagAAAAEF1ORTWBLYV/A829gw1rCW5aTWYPsSE+Cpt2ct15/vEecqWTP/9LXtmJI6y33oBCzQ==", "69b8cfc9-df05-4ea8-b9cc-7ed58488900b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e245578-b0d0-4bbc-ac0a-f2e114516a5b", "AQAAAAIAAYagAAAAEPtTJURpyUK+etPLZWYoxmIf2b3uDdEDrts5lR4QbQjn6Jqgt+0KF+dOWaumUybUPQ==", "520be0f3-debe-417d-b306-247ecbd71211" });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                column: "UID",
                value: new Guid("cf1e3bdc-bcb0-447e-b4c5-0157dc9295e0"));
        }
    }
}
