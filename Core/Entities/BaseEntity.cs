using System.Text.Json.Serialization;

namespace Core.Entities;

public class BaseEntity : IEntity
{

    public int Id { get; set; }
    
    [JsonIgnore]
    public string? Creator { get; set; }
    
    [JsonIgnore]
    public string? Changer { get; set; }
    
    [JsonIgnore]
    public bool IsDeleted { get; set; }
    
    [JsonIgnore]
    public DateTime Created { get; set; }
    
    [JsonIgnore]
    public DateTime Changed { get; set; }
}