using Core.Entities;

namespace Entities.Concrete;

public class VehicleOnTask : BaseEntity
{
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public int DepartmentId { get; set; }
    public string? AuthorizedPerson { get; set; }
    public string? Address { get; set; }
    
    public string? TaskDefinition { get; set; }
    public DateTime GivenDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public bool IsFinished { get; set; }
    
}