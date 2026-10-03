using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGlobalIdentityIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_TenantId_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_TenantId_NormalizedEmail",
                table: "AspNetUsers",
                columns: new[] { "TenantId", "NormalizedEmail" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [NormalizedEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_TenantId_NormalizedUserName",
                table: "AspNetUsers",
                columns: new[] { "TenantId", "NormalizedUserName" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_TenantId_NormalizedEmail",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_TenantId_NormalizedUserName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_TenantId_Email",
                table: "AspNetUsers",
                columns: new[] { "TenantId", "Email" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }
    }
}
