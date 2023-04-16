using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class MoreFieldsForPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DropNumber",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Packages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68ae6d70-ff17-4772-93f2-d18f931ac1af", "AQAAAAIAAYagAAAAEPYdCQa8Ezxm/sznlrFic8QPQYoqsM4JhY2EPFjXNaWtJAdZQ/lpC1zKcd8WOZubfw==", "6b167b2b-fd21-4b03-aaf2-b00b0fc3b759" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67880595-3acb-458f-a1bd-e92b94526573", "AQAAAAIAAYagAAAAEAMtMT70r+tBUOUbrvLia5SwXTn8Xc4B7KVekVyFDN4xu2jux1/b0+zs0QP2K8XWlQ==", "aa0180cc-f676-4b1d-bb05-20f30321a402" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e3541cb-1475-47cb-b613-4e60af305693", "AQAAAAIAAYagAAAAEOM9FK1BLB25Ow7S9KChdkYjZAzApi5Fe2+xrfchnm8wsIEHO2QeNVmK0daad+pQqA==", "b560a5b2-9206-4129-bdf4-dee910562f5e" });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DropNumber", "IsDelivered" },
                values: new object[] { 1, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropNumber",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Packages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4e760fa-4d16-4946-9d9a-3a3c9502b79a", "AQAAAAIAAYagAAAAEGrTqeD8JI6pbIFcyIFt0MgE+BxPYEegvMb+4H3I5apZxpl5BoYmvcW91gur6oq+ww==", "a52d47e1-92f3-4f24-82c7-fcd7ce713520" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6e5fb73-08fe-4ea6-9818-ee759acbdbf8", "AQAAAAIAAYagAAAAED6cvS8Bk9F9xQRBe64hkPwqHnGqbAd+FfutluFoeRqCFhsqe9/Fm6oL37RYyj2AYA==", "bcca59a5-5e4d-46c0-8ceb-ba779df6d9a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "729c1106-3f18-4512-b613-cabc4fc7c62f", "AQAAAAIAAYagAAAAEMHAwpxLluYmlfOeG2KOs2xEiNnuNJLtWXMjqZTlfaXIMja9n2kKbOgfrRU8HtWsRg==", "3e871615-8d90-4f4e-b406-1b5fe0192e31" });
        }
    }
}
