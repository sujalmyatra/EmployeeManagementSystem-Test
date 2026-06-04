namespace EmployeeManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService service) : ControllerBase
{
    private readonly IEmployeeService _service = service;

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        var result = await _service.CreateEmployeeAsync(dto);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeDto dto)
    {
        var result = await _service.UpdateEmployeeAsync(dto);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteEmployeeAsync(id);

        if(!result)
            return NotFound();
        
        return Ok("Employee Deactivated");
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid? id)
    {
        var result = await _service.GetEmployeesAsync(id);

        return Ok(result);
    }
}