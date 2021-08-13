using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateWebProject.Migrations
{
    public partial class Bid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BidModel",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    propname = table.Column<string>(nullable: false),
                    customername = table.Column<string>(nullable: false),
                    bid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidModel", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidModel");
        }
    }
}
