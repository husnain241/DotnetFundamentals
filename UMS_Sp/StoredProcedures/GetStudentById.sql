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