using Core.Entities;

namespace Entities.DTOs;

public class VehicleOnTaskForTableDto : IDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public string? VehiclePlate { get; set; }
    public string? DepartmentName { get; set; }
    public string? TaskDefinition { get; set; }
    public DateTime GivenDate { get; set; }
    public DateTime ReturnDate { get; set; }
}