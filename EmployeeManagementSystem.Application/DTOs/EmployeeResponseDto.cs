namespace EmployeeManagementSystem.Application.DTOs;

public class EmployeeResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public string Department { get; set; } = string.Empty;

    public string EmailId { get; set; } = string.Empty;

    public DateTime JoiningDate { get; set; }

    public bool Status { get; set; }
}