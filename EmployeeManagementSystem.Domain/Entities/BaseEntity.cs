namespace EmployeeManagementSystem.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool Status { get; set; } = true;
}
