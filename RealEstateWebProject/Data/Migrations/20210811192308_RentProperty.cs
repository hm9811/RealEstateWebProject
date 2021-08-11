using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateWebProject.Migrations
{
    public partial class RentProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentPropertyModel",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    image = table.Column<string>(nullable: false),
                    propertyType = table.Column<int>(nullable: false),
                    address = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    owner = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentPropertyModel", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentPropertyModel");
        }
    }
}
