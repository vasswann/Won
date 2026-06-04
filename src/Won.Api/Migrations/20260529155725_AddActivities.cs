using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Won.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeatherDependency = table.Column<int>(type: "int", nullable: false),
                    EnergyIntensity = table.Column<int>(type: "int", nullable: false),
                    MinimumGroupSize = table.Column<int>(type: "int", nullable: false),
                    MaximumGroupSize = table.Column<int>(type: "int", nullable: false),
                    ActivityDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.CheckConstraint("CK_Activities_EnergyIntensity", "\"EnergyIntensity\" >= 1 AND \"EnergyIntensity\" <= 10");
                    table.CheckConstraint("CK_Activities_GroupSize", "\"MinimumGroupSize\" <= \"MaximumGroupSize\"");
                    table.CheckConstraint("CK_Activities_WeatherDependency", "\"WeatherDependency\" >= 1 AND \"WeatherDependency\" <= 10");
                    table.ForeignKey(
                        name: "FK_Activities_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TripId",
                table: "Activities",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
