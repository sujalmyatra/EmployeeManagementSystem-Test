namespace EmployeeManagementSystem.Application.DTOs;

public class UpdateEmployeeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }

    public string EmailId { get; set; } = string.Empty;
}