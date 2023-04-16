using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliverySoftware.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeliveryStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deliveries");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16776aef-a5ff-424d-be7e-ea3fd591ce90",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8130406-5e90-459b-b4d2-564e2baf29ab", "AQAAAAIAAYagAAAAEISrd1O2BFKeF8/h+LRnYQhMAmBI+Vm6TtA0sHaScwdpPLSdakCE037ngA35TKs0qQ==", "59672046-6d5c-42bd-b99f-962b248d662e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "751e25c8-68ba-49dc-b1de-15fcb0bf210f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fe53337-68fc-4ebf-9c2a-70b3e5e84d34", "AQAAAAIAAYagAAAAEGkC1RW3x6M4loVLP5X7504Sr4DhUV3lJ6YCS6KQVFm3uPzI6rQFrGSnncEGp+j8CQ==", "06f19558-fe00-48e1-bfa3-61655bd9ff5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca6f5584-527f-4820-802f-bd329352c3e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af702541-4939-4bb7-9895-e246c0ec292f", "AQAAAAIAAYagAAAAEMHaH1GpZL+Yvzax1VDnJkj5QnUIBBFSLEQaMHputEYiafWop6f0T4RQALyxIri+uQ==", "7bb79ccf-8cf0-46c0-9fa1-31074f58482f" });
        }
    }
}
