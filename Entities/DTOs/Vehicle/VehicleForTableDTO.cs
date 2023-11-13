namespace Entities.DTOs;

public class VehicleForTableDTO
{
    public int Id { get; set; }
    public string? Plate { get; set; } 
    
    public string? Type { get; set; } 
    public string? Brand { get; set; } 
    
    public string Owner { get; set; }
    public string? Status { get; set; }
    
    

    public string? ModelName { get; set; }
}