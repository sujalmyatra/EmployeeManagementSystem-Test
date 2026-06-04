namespace EmployeeManagementSystem.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public IGenericRepository<Employee> Employees { get; } =
        new GenericRepository<Employee>(context);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
    
}