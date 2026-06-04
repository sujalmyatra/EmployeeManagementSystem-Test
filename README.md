# Employee Management System - Test Projects

This repository contains two parallel test projects for the Employee Management System:

- `EmployeeManagementSystem.Tests.xUnit`
- `EmployeeManagementSystem.Tests.NUnit`

Each test project includes two main test areas:

- `Controllers` - API controller tests for `EmployeesController`
- `Services` - business logic tests for `EmployeeService`

---

## xUnit Test Project

### Location
`EmployeeManagementSystem.Tests.xUnit/`

### Controller Tests
`EmployeeManagementSystem.Tests.xUnit/Controllers/EmployeesControllerTests.cs`

Test cases:
- `Create_ShouldReturnOkResult_WithEmployee`
- `Update_ShouldReturnOkResult_WithUpdatedEmployee`
- `Delete_ShouldReturnOkResult_WhenEmployeeDeleted`
- `Delete_ShouldReturnNotFound_WhenEmployeeDoesNotExists`
- `Get_ShouldReturnOkResult_WithAllEmployees`
- `Get_ShouldReturnOkResult_WhenIdExists`
- `Get_ShouldReturnEmptyResult_WhenEmployeeNotFound`

### Service Tests
`EmployeeManagementSystem.Tests.xUnit/Services/EmployeeServiceTests.cs`

Test cases:
- `CreateEmployeeAsync_ShouldCreateEmployee`
- `UpdateEmployeeAsync_ShouldUpdateEmployee_WhenEmployeeExists`
- `UpdateEmployeeAsync_ShouldThrowException_WhenEmployeeDoesNotExist`
- `DeleteEmployeeAsync_ShouldReturnTrue_WhenEmployeeExists`
- `DeleteEmployeeAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist`
- `GetEmployeesAsync_ShouldReturnAllEmployees`
- `GetEmployeesAsync_ShouldReturnEmployee_WhenIdExists`
- `GetEmployeesAsync_ShouldReturnEmptyList_WhenEmployeeNotFound`

---

## NUnit Test Project

### Location
`EmployeeManagementSystem.Tests.NUnit/`

### Controller Tests
`EmployeeManagementSystem.Tests.NUnit/Controllers/EmployeesControllerTests.cs`

Test cases:
- `Create_ShouldReturnOkResult_WithEmployee`
- `Update_ShouldReturnOkResult_WithUpdatedEmployee`
- `Delete_ShouldReturnOkResult_WhenEmployeeDeleted`
- `Delete_ShouldReturnNotFound_WhenEmployeeDoesNotExists`
- `Get_ShouldReturnOkResult_WithAllEmployees`
- `Get_ShouldReturnOkResult_WhenIdExists`
- `Get_ShouldReturnEmptyResult_WhenEmployeeNotFound`

### Service Tests
`EmployeeManagementSystem.Tests.NUnit/Services/EmployeeServiceTests.cs`

Test cases:
- `CreateEmployeeAsync_ShouldCreateEmployee`
- `UpdateEmployeeAsync_ShouldUpdateEmployee_WhenEmployeeExists`
- `UpdateEmployeeAsync_ShouldThrowException_WhenEmployeeDoesNotExist`
- `DeleteEmployeeAsync_ShouldReturnTrue_WhenEmployeeExists`
- `DeleteEmployeeAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist`
- `GetEmployeesAsync_ShouldReturnAllEmployees`
- `GetEmployeesAsync_ShouldReturnEmployee_WhenIdExists`
- `GetEmployeesAsync_ShouldReturnEmptyList_WhenEmployeeNotFound`

---

Both projects verify the same employee controller and service behavior using different test frameworks.
