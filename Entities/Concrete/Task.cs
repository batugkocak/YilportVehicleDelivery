using Core.Entities;

namespace Entities.Concrete;

public class Task : BaseEntity
{
    public string? Name { get; set; }
    public int DepartmentId { get; set; }
    public string? Address { get; set; }
}