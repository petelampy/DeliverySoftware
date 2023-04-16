using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class AddedTrackingCodeToPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackingCode",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                column: "TrackingCode",
                value: "TESTPKG123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingCode",
                table: "Packages");

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
        }
    }
}
