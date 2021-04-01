using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicFieldMapper.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MappableField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappableField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    MappableFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateField_MappableField_MappableFieldId",
                        column: x => x.MappableFieldId,
                        principalTable: "MappableField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateField_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MappableField",
                columns: new[] { "Id", "DataType", "Name" },
                values: new object[,]
                {
                    { 1, 18, "Company" },
                    { 2, 18, "Product" },
                    { 3, 18, "Quantity" },
                    { 4, 18, "EffectiveDate" },
                    { 5, 18, "LessIncluded" },
                    { 6, 18, "UnitCost" },
                    { 7, 18, "UnitPrice" },
                    { 8, 18, "CancelDate" },
                    { 9, 18, "Sequence" },
                    { 10, 18, "SerialNumber" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Template_Name_UserName",
                table: "Template",
                columns: new[] { "Name", "UserName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplateField_MappableFieldId",
                table: "TemplateField",
                column: "MappableFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateField_TemplateId",
                table: "TemplateField",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateField");

            migrationBuilder.DropTable(
                name: "MappableField");

            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}
