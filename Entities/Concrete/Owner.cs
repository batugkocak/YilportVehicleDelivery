using Core.Entities;

namespace Entities.Concrete;

public class Owner : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int DepartmentId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? TCIdentityNo { get; set; }
}