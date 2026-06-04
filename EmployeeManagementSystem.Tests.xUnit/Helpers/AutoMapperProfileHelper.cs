namespace EmployeeManagementSystem.Tests.Helpers;

public static class AutoMapperProfileHelper
{
    public static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        return config.CreateMapper();
    }
}