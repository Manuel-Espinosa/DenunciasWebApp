using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DenunciasWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddComplaintStatusAndCrimeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Complaints",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "CreatetAt",
                table: "Complaints",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "CrimeId",
                table: "Complaints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crimes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_CrimeId",
                table: "Complaints",
                column: "CrimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_StatusId",
                table: "Complaints",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintStatuses_StatusId",
                table: "Complaints",
                column: "StatusId",
                principalTable: "ComplaintStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Crimes_CrimeId",
                table: "Complaints",
                column: "CrimeId",
                principalTable: "Crimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintStatuses_StatusId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Crimes_CrimeId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintStatuses");

            migrationBuilder.DropTable(
                name: "Crimes");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_CrimeId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_StatusId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "CrimeId",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Complaints",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Complaints",
                newName: "CreatetAt");
        }
    }
}
