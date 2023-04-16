using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentDrop",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDrop",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "TrackingCode", "UID" },
                values: new object[] { "Big box of rocks", "TESTPKG666", new Guid("f8f2d976-795f-499f-a294-38412adfcc53") });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CustomerUID", "DeliveryUID", "Description", "DropNumber", "IsAssignedToDelivery", "IsDelivered", "Size", "TrackingCode", "UID" },
                values: new object[] { 2, new Guid("751e25c8-68ba-49dc-b1de-15fcb0bf210f"), new Guid("3f3cf1cd-131d-4d05-93b9-21dbd082fcac"), "Big box of bolts", 2, true, false, 10, "TESTPKG123", new Guid("721a28e5-03b7-4c9a-863a-53d58e23d64a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CurrentDrop",
                table: "Deliveries");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a6421e4-aeaf-4cca-889e-94512fa80c09", "AQAAAAIAAYagAAAAELLifaxdN8NWiurc+9reGtupvA8OyVM3lfwBqCu9yHz3Xo0lPx7tCqgwFln79+A+VQ==", "b2e03a58-e462-4f1a-ba50-30d61d199b5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e234a9ef-0fa0-4c13-a63d-b8fb730a2354", "AQAAAAIAAYagAAAAEJR0Ed8slQ+JDxAacKhQvac80+nggQ0uLv2/DWAVvmhtgId7trUcuN04eco8jbKZOQ==", "c5a6bdc7-b6ca-43b6-95ad-79a6577631a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e93b0145-3275-4f87-b359-828dcb42a24a", "AQAAAAIAAYagAAAAED78iVOa3djAmOq6u2MreqWhhs1HTDvfgNzyFx5i50JdqnScwV6R0mEpXv1wx1zgXg==", "47efe106-1a16-4474-b40b-e68112d2e808" });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "TrackingCode", "UID" },
                values: new object[] { "Big box of bolts", "TESTPKG123", new Guid("721a28e5-03b7-4c9a-863a-53d58e23d64a") });
        }
    }
}
