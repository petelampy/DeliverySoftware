using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class SampleDataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f8130406-5e90-459b-b4d2-564e2baf29ab", true, "PETELAMPY@GMAIL.COM", "DRIVERLOGON", "AQAAAAIAAYagAAAAEISrd1O2BFKeF8/h+LRnYQhMAmBI+Vm6TtA0sHaScwdpPLSdakCE037ngA35TKs0qQ==", "59672046-6d5c-42bd-b99f-962b248d662e", "driverlogon" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "3fe53337-68fc-4ebf-9c2a-70b3e5e84d34", true, "PETELAMPY@GMAIL.COM", "CUSTOMERLOGON", "AQAAAAIAAYagAAAAEGkC1RW3x6M4loVLP5X7504Sr4DhUV3lJ6YCS6KQVFm3uPzI6rQFrGSnncEGp+j8CQ==", "06f19558-fe00-48e1-bfa3-61655bd9ff5a", "customerlogon" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "af702541-4939-4bb7-9895-e246c0ec292f", true, "PETELAMPY@GMAIL.COM", "EMPLOYEELOGON", "AQAAAAIAAYagAAAAEMHaH1GpZL+Yvzax1VDnJkj5QnUIBBFSLEQaMHputEYiafWop6f0T4RQALyxIri+uQ==", "7bb79ccf-8cf0-46c0-9fa1-31074f58482f", "employeelogon" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "38c1da5e-a21e-446b-8b1e-ecf30cbc6619", false, null, null, null, "5275e674-79ae-416f-b409-7f49aaed3ecc", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "25c870cd-a7a3-4f9f-8173-1e5bfffd4ec6", false, null, null, null, "ffbca79f-52b4-4c06-9436-7b9b04cd459f", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f5405056-fd56-46c4-a847-609f52cde9ea", false, null, null, null, "6377b8da-38a6-4839-98f5-c89b623d5dfc", null });
        }
    }
}
