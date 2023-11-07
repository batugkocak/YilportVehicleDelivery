using Core.Entities;

namespace Entities.DTOs.Driver;

public class DriverDto:IDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? DepartmentName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Mission { get; set; }
}