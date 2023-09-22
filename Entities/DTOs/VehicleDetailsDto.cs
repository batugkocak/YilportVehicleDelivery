using Core.Entities;

namespace Entities.DTOs;

public class VehicleDetailsDto : IDto
{
    public string? Plate { get; set; }
    public string? Type { get; set; }
    public string? FuelType { get; set; }
    public string? Owner { get; set; }
    public string? Brand { get; set; }
    public string? ModelName { get; set; }
    public int ModelYear { get; set; }
    public string? Color { get; set; }
    public string? Department { get; set; }
    public string? Status { get; set; }
    public string? Note { get; set; }
}