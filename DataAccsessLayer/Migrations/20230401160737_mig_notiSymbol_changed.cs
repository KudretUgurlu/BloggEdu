using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccsessLayer.Migrations
{
    public partial class mig_notiSymbol_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationTypeSynbol",
                table: "Notifications",
                newName: "NotificationTypeSymbol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationTypeSymbol",
                table: "Notifications",
                newName: "NotificationTypeSynbol");
        }
    }
}
