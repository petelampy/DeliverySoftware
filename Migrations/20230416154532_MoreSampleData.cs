using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class MoreSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92fb2a53-69bb-4243-8cab-710b533ddee8", "AQAAAAIAAYagAAAAEP2RMhiG3ctDeFhZdo7HyZDUt8Q7h/9PS6sfShPLf3U0fG3kXiUXJZ6BAtGFd4df+A==", "29518f88-0a04-45c2-b708-1c1d3d040191" });

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "Forename", "HouseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostCode", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "52fac4db-3d93-46bc-81ef-f33aed467669", 0, "3 Bond End, Monks Kirby, Rugby, UK", "61afc09e-f291-413d-8feb-3f6d311ea391", "petelampy@gmail.com", true, "Amos", 3, false, null, "PETELAMPY@GMAIL.COM", "CUSTOMERLOGON2", "AQAAAAIAAYagAAAAEAlcWm6kEF6Nsgu1CfcJjDfmJ/IfYKKe5aa6hwmjn1KrBKOxgjlxEaTs+yJjzd4dQg==", null, false, "CV23 0RD", "4902831b-58ab-4fdd-985b-ef2f874f3724", "Burton", false, "customerlogon2", 1 });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerUID", "UID" },
                values: new object[] { new Guid("52fac4db-3d93-46bc-81ef-f33aed467669"), new Guid("cf1e3bdc-bcb0-447e-b4c5-0157dc9295e0") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52fac4db-3d93-46bc-81ef-f33aed467669");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0edf419e-4a5a-46c7-9ec1-eb3d4dae6335", "AQAAAAIAAYagAAAAELZqtzUYyOZiEJl6Xr1+kNA4vTsS0M7XipD9rGNSwpxLvqgyLrO70y1+TEX9wS3v4Q==", "63506521-3337-4a71-b4d1-725bc5e92a9b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0262e3ce-88c5-4239-b4c6-43862bac3e76", "AQAAAAIAAYagAAAAEAAZnyAK+/tUR8k1FXr76FbAX3gjrZMMrzVJm66cSFP7tfCAhGg6L6eMDDpuw+xO4A==", "abbf8917-69b9-48ce-96a9-c23d25cad38e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9242f408-0c51-4df5-a59c-d609079d3c9d", "AQAAAAIAAYagAAAAENS89UTim4f5m6MMbyAu3KimQ+8C4hiNihguRCNwvyJd5FB1mDRLI7ODhYhyxalCWA==", "3d5a173b-0941-4078-b9c7-51c62d848371" });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerUID", "UID" },
                values: new object[] { new Guid("751e25c8-68ba-49dc-b1de-15fcb0bf210f"), new Guid("f8f2d976-795f-499f-a294-38412adfcc53") });
        }
    }
}
