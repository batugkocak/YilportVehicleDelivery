namespace DataAccess.FilterQueryObjects.VehicleOnTask;

public class VotFilterRequest
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = int.MaxValue;
    public DateTime? FirstDate { get; set; }
    public DateTime? LastDate { get; set; }

    public string? DateType { get; set; }
}