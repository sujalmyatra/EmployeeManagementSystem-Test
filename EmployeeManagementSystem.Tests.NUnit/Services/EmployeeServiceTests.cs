namespace EmployeeManagementSystem.Tests.NUnit.Services;

[TestFixture]
public class EmployeeServiceTests
{
    private Mock<IUnitOfWork> _uowMock;
    private Mock<IGenericRepository<Employee>> _repoMock;
    private IMapper _mapper;
    private EmployeeService _service;

    [SetUp]
    public void Setup()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _repoMock = new Mock<IGenericRepository<Employee>>();

        _mapper = AutoMapperProfileHelper.GetMapper();

        _uowMock.Setup(x => x.Employees)
            .Returns(_repoMock.Object);

        _service = new EmployeeService(_uowMock.Object, _mapper);
    }


    [Test]
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


        Assert.That(result, Is.Not.Null);

        Assert.That(addedEmployee, Is.Not.Null);

        Assert.That(result.Name, Is.EqualTo(dto.Name));
        Assert.That(addedEmployee!.Name, Is.EqualTo(dto.Name));
        Assert.That(result.EmailId, Is.EqualTo(dto.EmailId));
        Assert.That(result.Salary, Is.EqualTo(dto.Salary));

        _repoMock.Verify(x =>
            x.AddAsync(It.IsAny<Employee>()),
            Times.Once);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Once);
    }

    [Test]
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


        Assert.That(result, Is.Not.Null);

        Assert.That(result.Name, Is.EqualTo(dto.Name));
        Assert.That(result.Salary, Is.EqualTo(dto.Salary));
        Assert.That(result.EmailId, Is.EqualTo(dto.EmailId));

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Once);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Once);
    }

    [Test]
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

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            await _service.UpdateEmployeeAsync(dto));


        Assert.That(exception!.Message, Is.EqualTo("Employee Not Found"));

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Never);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Never);
    }

    [Test]
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


        Assert.That(result, Is.True);

        Assert.That(employee.Status, Is.False);

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Once);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Once);
    }

    [Test]
    public async Task DeleteEmployeeAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
    {
        var id = Guid.NewGuid();

        _repoMock.Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var result = await _service.DeleteEmployeeAsync(id);


        Assert.That(result, Is.False);

        _repoMock.Verify(x =>
            x.Update(It.IsAny<Employee>()),
            Times.Never);

        _uowMock.Verify(x =>
            x.SaveChangesAsync(),
            Times.Never);
    }

    [Test]
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


        Assert.That(result, Is.Not.Null);

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
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


        Assert.That(result.Count(), Is.EqualTo(1));
        var emp = result.First();

        Assert.That(emp.Name, Is.EqualTo(employee.Name));
        Assert.That(emp.EmailId, Is.EqualTo(employee.EmailId));
    }

    [Test]
    public async Task GetEmployeesAsync_ShouldReturnEmptyList_WhenEmployeeNotFound()
    {
        var id = Guid.NewGuid();

        _repoMock.Setup(x => x.GetByIdAsync(id))
            .ReturnsAsync((Employee?)null);

        var result = await _service.GetEmployeesAsync(id);


        Assert.That(result, Is.Empty);
    }
}