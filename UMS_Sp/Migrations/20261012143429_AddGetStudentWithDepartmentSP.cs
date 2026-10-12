using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMS_Sp.Migrations
{
    public partial class AddGetStudentWithDepartmentSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE GetStudentWithDepartment
                    @StudentId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    SELECT 
                        s.Id, 
                        s.Name, 
                        s.Email, 
                        d.Name AS DepartmentName
                    FROM Students s
                    INNER JOIN Departments d ON s.DepartmentId = d.Id
                    WHERE s.Id = @StudentId;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetStudentWithDepartment");
        }
    }
}