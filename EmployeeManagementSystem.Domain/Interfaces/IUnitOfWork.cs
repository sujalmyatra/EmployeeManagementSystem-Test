namespace EmployeeManagementSystem.Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Employee> Employees { get; }
    Task<int> SaveChangesAsync();
}
