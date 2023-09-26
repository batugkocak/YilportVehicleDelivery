using Core.Entities;

namespace Entities.Concrete;

public class Driver: BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int DepartmentId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Mission { get; set; }
}