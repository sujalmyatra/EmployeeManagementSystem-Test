namespace EmployeeManagementSystem.Tests.Controllers;

public class EmployeesControllerTests
{
    private readonly Mock<IEmployeeService> _serviceMock;
    private readonly EmployeesController _controller;

    public EmployeesControllerTests()
    {
        _serviceMock = new Mock<IEmployeeService>();

        _controller = new EmployeesController(_serviceMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnOkResult_WithEmployee()
    {
        var dto = new CreateEmployeeDto
        {
            Name = "Sujal",
            Salary = 50000,
            DepartmentId = 1,
            EmailId = "sujal@gmail.com"
        };

        var response = new EmployeeResponseDto
        {
            Id = Guid.NewGuid(),
            Name = "Sujal",
            Salary = 50000,
            Department = "IT",
            EmailId = "sujal@gmail.com",
            JoiningDate = DateTime.UtcNow,
            Status = true
        };

        _serviceMock.Setup(x =>
            x.CreateEmployeeAsync(dto))
            .ReturnsAsync(response);

        var result = await _controller.Create(dto);

        var okResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsType<EmployeeResponseDto>(okResult.Value);


        Assert.Equal(response.Name, value.Name);
        Assert.Equal(response.EmailId, value.EmailId);

        _serviceMock.Verify(x =>
            x.CreateEmployeeAsync(dto),
            Times.Once);
    }

    [Fact]
    public async Task Update_ShouldReturnOkResult_WithUpdatedEmployee()
    {
        var employeeId = Guid.NewGuid();

        var dto = new UpdateEmployeeDto
        {
            Id = employeeId,
            Name = "Updated Name",
            Salary = 70000,
            DepartmentId = 2,
            EmailId = "updated@gmail.com"
        };

        var response = new EmployeeResponseDto
        {
            Id = employeeId,
            Name = "Updated Name",
            Salary = 70000,
            Department = "HR",
            EmailId = "updated@gmail.com",
            JoiningDate = DateTime.UtcNow,
            Status = true
        };

        _serviceMock.Setup(x =>
            x.UpdateEmployeeAsync(dto))
            .ReturnsAsync(response);

        var result = await _controller.Update(dto);

        var okResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsType<EmployeeResponseDto>(okResult.Value);


        Assert.Equal(dto.Name, value.Name);
        Assert.Equal(dto.Salary, value.Salary);

        _serviceMock.Verify(x =>
            x.UpdateEmployeeAsync(dto),
            Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldReturnOkResult_WhenEmployeeDeleted()
    {
        var employeeId = Guid.NewGuid();

        _serviceMock.Setup(x =>
            x.DeleteEmployeeAsync(employeeId))
            .ReturnsAsync(true);

        var result = await _controller.Delete(employeeId);

        var okResult = Assert.IsType<OkObjectResult>(result);


        Assert.Equal("Employee Deactivated", okResult.Value);

        _serviceMock.Verify(x =>
            x.DeleteEmployeeAsync(employeeId),
            Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenEmployeeDoesNotExists()
    {
        var employeeId = Guid.NewGuid();

        _serviceMock.Setup(x =>
            x.DeleteEmployeeAsync(employeeId))
            .ReturnsAsync(false);

        var result = await _controller.Delete(employeeId);


        Assert.IsType<NotFoundResult>(result);

        _serviceMock.Verify(x =>
            x.DeleteEmployeeAsync(employeeId),
            Times.Once);
    }

    [Fact]
    public async Task Get_ShouldReturnOkResult_WithAllEmployees()
    {
        var employees = new List<EmployeeResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sujal",
                Salary = 50000,
                Department = "IT",
                EmailId = "sujal@gmail.com",
                JoiningDate = DateTime.UtcNow,
                Status = true
            },

            new()
            {
                Id = Guid.NewGuid(),
                Name = "Vraj",
                Salary = 60000,
                Department = "HR",
                EmailId = "vraj@gmail.com",
                JoiningDate = DateTime.UtcNow,
                Status = true
            }
        };

        _serviceMock.Setup(x =>
            x.GetEmployeesAsync(null))
            .ReturnsAsync(employees);

        var result = await _controller.Get(null);

        var okResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<EmployeeResponseDto>>(okResult.Value);


        Assert.Equal(2, value.Count());

        _serviceMock.Verify(x =>
            x.GetEmployeesAsync(null),
            Times.Once);
    }

    [Fact]
    public async Task Get_ShouldReturnOkResult_WhenIdExists()
    {
        var employeeId = Guid.NewGuid();

        var employees = new List<EmployeeResponseDto>
        {
            new()
            {
                Id = employeeId,
                Name = "Sujal",
                Salary = 50000,
                Department = "IT",
                EmailId = "sujal@gmail.com",
                JoiningDate = DateTime.UtcNow,
                Status = true
            }
        };

        _serviceMock.Setup(x =>
            x.GetEmployeesAsync(employeeId))
            .ReturnsAsync(employees);

        var result = await _controller.Get(employeeId);

        var okResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<EmployeeResponseDto>>(okResult.Value);

        Assert.Single(value);

        var employee = value.First();


        Assert.Equal("Sujal", employee.Name);

        _serviceMock.Verify(x =>
            x.GetEmployeesAsync(employeeId),
            Times.Once);
    }

    [Fact]
    public async Task Get_ShouldReturnEmptyResult_WhenEmployeeNotFound()
    {
        var employeeId = Guid.NewGuid();

        _serviceMock.Setup(x =>
            x.GetEmployeesAsync(employeeId))
            .ReturnsAsync(new List<EmployeeResponseDto>());

        var result = await _controller.Get(employeeId);

        var okResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<EmployeeResponseDto>>(okResult.Value);


        Assert.Empty(value);

        _serviceMock.Verify(x =>
            x.GetEmployeesAsync(employeeId),
            Times.Once);
    }
}

