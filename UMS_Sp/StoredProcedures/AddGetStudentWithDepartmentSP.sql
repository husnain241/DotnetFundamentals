CREATE OR ALTER PROCEDURE GetStudentWithDepartment
    @StudentId INT
AS
BEGIN
    -- Performance ke liye
    SET NOCOUNT ON;
    
    -- Student aur Department table ko JOIN karna
    SELECT 
        s.Id, 
        s.Name, 
        s.Email, 
        d.Name AS DepartmentName  -- Department ka naam
    FROM Students s
    INNER JOIN Departments d ON s.DepartmentId = d.Id
    WHERE s.Id = @StudentId;
END