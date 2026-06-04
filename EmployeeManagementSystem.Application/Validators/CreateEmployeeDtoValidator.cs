namespace EmployeeManagementSystem.Application.Validators;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name is Required")
            .MinimumLength(3)
            .MaximumLength(100);
        RuleFor(e => e.Salary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than 0");
        RuleFor(x => x.DepartmentId)
            .InclusiveBetween(1, 5)
            .WithMessage("Invalid Department");
        RuleFor(x => x.EmailId)
           .NotEmpty()
           .EmailAddress()
           .WithMessage("Invalid Email Address");
    }
}
