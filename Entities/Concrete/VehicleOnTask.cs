using Core.Entities;

namespace Entities.Concrete;

//TODO: Kullanıcı (YA) hazır görevlerden seçip, otomatik olarak  "görev niteliği, adres, departman" kısımlarını dolduracak
//TODO: (YA DA) eliyle nitelik ve adres girecek. 
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
}