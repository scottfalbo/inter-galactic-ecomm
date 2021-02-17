using Microsoft.EntityFrameworkCore.Migrations;

namespace InterGalacticEcomm.Migrations
{
    public partial class adminrole2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "34234gety45tb45v45", 0, "71cadef9-e7c4-4d56-bc29-e9104c64cf54", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEG+3i9D03iBhtBfU2KEPFEjOOn4ypzC0uT+RZk8mkPEiiW9WxXmNQEwqCnYImSMA2g==", null, false, "01b800c0-048a-4582-9274-b3e3add66f55", false, "SuperAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "34234gety45tb45v45", "admin_permission" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34234gety45tb45v45");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "34234gety45tb45v45", "admin_permission" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", 0, "c96c51bd-a204-4819-8350-07c725e0dfcb", null, false, false, null, null, null, "AQAAAAEAACcQAAAAEG/Xfv1UVgBTtAPK9JZ83X9kAz/9bjAFQXazdm9QCXu3pu96kRVhU7d3qX2VaijLaQ==", null, false, "06abb51f-a321-4ab8-bfe8-62fde4b54bda", false, "SuperAdmin" });
        }
    }
}
