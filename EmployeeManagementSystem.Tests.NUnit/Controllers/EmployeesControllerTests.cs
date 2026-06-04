namespace EmployeeManagementSystem.Tests.NUnit.Controllers;

[TestFixture]
public class EmployeesControllerTests
{
    private Mock<IEmployeeService> _serviceMock;
    private EmployeesController _controller;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IEmployeeService>();

        _controller = new EmployeesController(_serviceMock.Object);
    }


    [Test]
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

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;

        Assert.That(okResult!.Value, Is.TypeOf<EmployeeResponseDto>());

        var value = okResult.Value as EmployeeResponseDto;

        Assert.That(value!.Name, Is.EqualTo(response.Name));
        Assert.That(value.EmailId, Is.EqualTo(response.EmailId));

        _serviceMock.Verify(x =>
            x.CreateEmployeeAsync(dto),
            Times.Once);
    }


    [Test]
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

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;

        Assert.That(okResult!.Value, Is.TypeOf<EmployeeResponseDto>());

        var value = okResult.Value as EmployeeResponseDto;


        Assert.That(value!.Name, Is.EqualTo(dto.Name));
        Assert.That(value.Salary, Is.EqualTo(dto.Salary));

        _serviceMock.Verify(x =>
            x.UpdateEmployeeAsync(dto),
            Times.Once);
    }

    [Test]
    public async Task Delete_ShouldReturnOkResult_WhenEmployeeDeleted()
    {
        var employeeId = Guid.NewGuid();

        _serviceMock.Setup(x =>
            x.DeleteEmployeeAsync(employeeId))
            .ReturnsAsync(true);

        var result = await _controller.Delete(employeeId);

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;

        Assert.That(okResult!.Value, Is.EqualTo("Employee Deactivated"));

        _serviceMock.Verify(x =>
            x.DeleteEmployeeAsync(employeeId),
            Times.Once);
    }

    [Test]
    public async Task Delete_ShouldReturnNotFound_WhenEmployeeDoesNotExists()
    {
        var employeeId = Guid.NewGuid();

        _serviceMock.Setup(x =>
            x.DeleteEmployeeAsync(employeeId))
            .ReturnsAsync(false);

        var result = await _controller.Delete(employeeId);


        Assert.That(result, Is.TypeOf<NotFoundResult>());

        _serviceMock.Verify(x =>
            x.DeleteEmployeeAsync(employeeId),
            Times.Once);
    }

    [Test]
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

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;

        var value = okResult!.Value as IEnumerable<EmployeeResponseDto>;


        Assert.That(value.Count(), Is.EqualTo(2));

        _serviceMock.Verify(x =>
            x.GetEmployeesAsync(null),
            Times.Once);
    }


    [Test]
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

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;

        var value = okResult!.Value as IEnumerable<EmployeeResponseDto>;

        Assert.That(value.Count(), Is.EqualTo(1));

        var employee = value!.First();


        Assert.That(employee.Name, Is.EqualTo("Sujal"));

        _serviceMock.Verify(x =>
            x.GetEmployeesAsync(employeeId),
            Times.Once);
    }

    [Test]
    public async Task Get_ShouldReturnEmptyResult_WhenEmployeeNotFound()
    {
        var employeeId = Guid.NewGuid();

        _serviceMock.Setup(x =>
            x.GetEmployeesAsync(employeeId))
            .ReturnsAsync(new List<EmployeeResponseDto>());

        var result = await _controller.Get(employeeId);

        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;

        var value = okResult!.Value as IEnumerable<EmployeeResponseDto>;


        Assert.That(value, Is.Empty);

        _serviceMock.Verify(x =>
            x.GetEmployeesAsync(employeeId),
            Times.Once);
    }
}