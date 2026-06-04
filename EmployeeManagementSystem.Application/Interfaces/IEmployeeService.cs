namespace EmployeeManagementSystem.Application.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto);
    Task<EmployeeResponseDto> UpdateEmployeeAsync(UpdateEmployeeDto dto);
    Task<bool> DeleteEmployeeAsync(Guid id);
    Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync(Guid? id);
}
