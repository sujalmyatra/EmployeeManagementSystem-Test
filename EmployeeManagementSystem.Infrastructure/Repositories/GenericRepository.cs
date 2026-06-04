namespace EmployeeManagementSystem.Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
         => await _dbSet.AsNoTracking().ToListAsync();
    

    public async Task<T?> GetByIdAsync(Guid id)
    
        => await _dbSet.FindAsync(id);
    
    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);
   

    public void Update(T entity)
       => _dbSet.Update(entity);
    

    public void Delete(T entity)
    {
        entity.Status = false;
        _dbSet.Update(entity);
    }
}
