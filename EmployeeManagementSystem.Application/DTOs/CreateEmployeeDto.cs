namespace EmployeeManagementSystem.Application.DTOs;

public class CreateEmployeeDto
{
    public string Name { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }

    public string EmailId { get; set; } = string.Empty;
}
