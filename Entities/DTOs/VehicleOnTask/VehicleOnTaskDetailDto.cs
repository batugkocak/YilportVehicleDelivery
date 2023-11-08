using Core.Entities;

namespace Entities.DTOs;

public class VehicleOnTaskDetailDto : IDto
{
    public int VehicleOnTaskId { get; set; }
    public int VehicleId { get; set; }
    public string? VehiclePlate { get; set; }
    public string? DriverName { get; set; }
    public string? DepartmentName { get; set; }
    public string? AuthorizedPerson { get; set; }
    public string? Address { get; set; }
    public string? TaskDefinition { get; set; }
    public DateTime GivenDate { get; set; }
    public string? GivenDateFormatted => GivenDate != null ? GivenDate.ToString("dd-MM-yyyy HH:mm") : "";
    public DateTime ReturnDate { get; set; }
    public string? ReturnDateFormatted => ReturnDate != null ? ReturnDate.ToString("dd-MM-yyyy HH:mm") : "";
}