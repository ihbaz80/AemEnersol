public class Well
{
    public int Id { get; set; }

    public string WellName { get; set; }

    public string WellCode { get; set; }

    public int PlatformId { get; set; }

    public Platform Platform { get; set; }

    public DateTime UpdatedDate { get; set; }
}