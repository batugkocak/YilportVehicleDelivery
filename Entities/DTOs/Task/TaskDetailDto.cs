using Core.Entities;

namespace Entities.DTOs.Task;

public class TaskDetailDto : IDto
{
    public string? Name { get; set; }
    public string? DepartmentName { get; set; }
    public string? Address { get; set; }
}