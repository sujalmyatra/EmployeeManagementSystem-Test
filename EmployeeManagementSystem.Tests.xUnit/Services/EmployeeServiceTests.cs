namespace EmployeeManagementSystem.Tests.Services;

public class EmployeeServiceTests
{
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<IGenericRepository<Employee>> _repoMock;
    private readonly IMapper _mapper;
    private readonly EmployeeService _service;

    public EmployeeServiceTests()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _repoMock = new Mock<IGenericRepository<Employee>>();

        _mapper = AutoMapperProfileHelper.GetMapper();

        _uowMock.Setup(x => x.Employees)
            .Returns(_repoMock.Object);

        _service = new EmployeeService(_uowMock.Object, _mapper);
    }

    [Fact]
    public async Task CreateEmployeeAsync_ShouldCreateEmployee()
    {
        var dto = new CreateEmployeeDto
        {
            Name = "Sujal",
            Salary = 50000,
            DepartmentId = 1,
            EmailId = "sujal@gmail.com"
        };

        Employee? addedEmployee = null;

        _repoMock.Setup(x => x.AddAsync(It.IsAny<Employee>()))
            .Callback<Employee>(emp =>
            {
                addedEmployee = emp;
            })
            .Returns(Task.CompletedTask);

        _uowMock.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var result = await _service.CreateEmployeeAsync(dto);

        Assert.NotNull(result);

        Assert.NotNull(addedEmployee);

        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Name, addedEmployee!.Name);
        Assert.Equal(dto.EmailId, result.EmailId);
        Assert.Equal(dto.Salary, result.Salary);

        _repoMock.Verify(x =>
            x.AddAsync(It.IsAny<Employee>()),
            Times.Once);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ShouldUpdateEmployee_WhenEmployeeExists()
    {
        var employeeId = Guid.NewGuid();

        var existingEmployee = new Employee
        {
            Id = employeeId,
            Name = "Old Name",
            Salary = 30000,
            DepartmentId = Department.IT,
            EmailId = "old@gmail.com"
        };

        var dto = new UpdateEmployeeDto
        {
            Id = employeeId,
            Name = "New Name",
            Salary = 70000,
            DepartmentId = 2,
            EmailId = "new@gmail.com"
        };

        _repoMock.Setup(x => x.GetByIdAsync(employeeId))
            .ReturnsAsync(existingEmployee);

        _uowMock.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var result = await _service.UpdateEmployeeAsync(dto);

        Assert.NotNull(result);

        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Salary, result.Salary);
        Assert.Equal(dto.EmailId, result.EmailId);

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Once);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ShouldThrowException_WhenEmployeeDoesNotExist()
    {
        var dto = new UpdateEmployeeDto
        {
            Id = Guid.NewGuid(),
            Name = "Sujal",
            Salary = 50000,
            DepartmentId = 1,
            EmailId = "sujal@gmail.com"
        };

        _repoMock.Setup(x => x.GetByIdAsync(dto.Id))
            .ReturnsAsync((Employee?)null);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.UpdateEmployeeAsync(dto));

        Assert.Equal("Employee Not Found", exception.Message);

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Never);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_ShouldReturnTrue_WhenEmployeeExists()
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Sujal",
            Salary = 50000,
            DepartmentId = Department.IT,
            EmailId = "sujal@gmail.com",
            Status = true
        };

        _repoMock.Setup(x => x.GetByIdAsync(employee.Id))
            .ReturnsAsync(employee);

        _uowMock.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var result = await _service.DeleteEmployeeAsync(employee.Id);

        Assert.True(result);

        Assert.False(employee.Status);

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Once);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
    {
        var id = Guid.NewGuid();

        _repoMock.Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var result = await _service.DeleteEmployeeAsync(id);

        Assert.False(result);

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Never);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task GetEmployeesAsync_ShouldReturnAllEmployees()
    {
        var employees = new List<Employee>
        {
            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Sujal",
                Salary = 50000,
                DepartmentId = Department.IT,
                EmailId = "sujal@gmail.com"
            },

            new Employee
            {
                Id = Guid.NewGuid(),
                Name = "Vraj",
                Salary = 60000,
                DepartmentId = Department.HR,
                EmailId = "vraj@gmail.com"
            }
        };

        _repoMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(employees);

        var result = await _service.GetEmployeesAsync(null);

        Assert.NotNull(result);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetEmployeesAsync_ShouldReturnEmployee_WhenIdExists()
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Sujal",
            Salary = 50000,
            DepartmentId = Department.IT,
            EmailId = "sujal@gmail.com"
        };

        _repoMock.Setup(x => x.GetByIdAsync(employee.Id))
            .ReturnsAsync(employee);

        var result = await _service.GetEmployeesAsync(employee.Id);

        Assert.Single(result);

        var emp = result.First();

        Assert.Equal(employee.Name, emp.Name);
        Assert.Equal(employee.EmailId, emp.EmailId);
    }

    [Fact]
    public async Task GetEmployeesAsync_ShouldReturnEmptyList_WhenEmployeeNotFound()
    {
        var id = Guid.NewGuid();

        _repoMock.Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var result = await _service.GetEmployeesAsync(id);

        Assert.Empty(result);
    }
}
