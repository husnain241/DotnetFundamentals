using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS_Sp.Migrations
{
    public partial class AddGetStudentByIdProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Yahan apni Stored Procedure ka SQL code paste karein
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE GetStudentById
                    @StudentId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    SELECT 
                        Id,
                        Name,
                        Email,
                        EnrollmentDate,
                        DepartmentId
                    FROM Students
                    WHERE Id = @StudentId;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Jab migration rollback ho, toh procedure delete ho jaye
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetStudentById");
        }
    }
}