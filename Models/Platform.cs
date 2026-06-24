public class Platform
{
    public int Id { get; set; }

    public string PlatformName { get; set; }

    public string PlatformCode { get; set; }

    public DateTime UpdatedDate { get; set; }

    public ICollection<Well> Wells { get; set; }
}