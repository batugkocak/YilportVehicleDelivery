namespace Core.Entities;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
    public string? Creator { get; set; }
    public string? Changer { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
}