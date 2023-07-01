using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Big_Bang_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorsDoctorId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorsDoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorsDoctorId",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "Doctor_id",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Doctor_id",
                table: "Patients",
                column: "Doctor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_Doctor_id",
                table: "Patients",
                column: "Doctor_id",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_Doctor_id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Doctor_id",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Doctor_id",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "DoctorsDoctorId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorsDoctorId",
                table: "Patients",
                column: "DoctorsDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorsDoctorId",
                table: "Patients",
                column: "DoctorsDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");
        }
    }
}
