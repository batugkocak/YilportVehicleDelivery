namespace DataAccess.FilterQueryObjects.VehicleOnTask;

public class VotFilterRequest
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = int.MaxValue;
    public DateTime? FirstGivenDate { get; set; } =  DateTime.MinValue;
    public DateTime? LastReturnDate { get; set; } = DateTime.Now;
}