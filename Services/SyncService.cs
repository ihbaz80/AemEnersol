using System.Text.Json;

public class SyncService
{
    private readonly AppDbContext _db;

    public SyncService(AppDbContext db)
    {
        _db = db;
    }

    public async Task SyncAsync(JsonDocument document)
    {
        foreach (var platformJson
                 in document.RootElement.EnumerateArray())
        {
            int platformId =
                platformJson.TryGetProperty(
                    "id",
                    out var idElement)
                ? idElement.GetInt32()
                : 0;

            string platformName =
                platformJson.TryGetProperty(
                    "platformName",
                    out var nameElement)
                ? nameElement.GetString()
                : string.Empty;

            string platformCode =
                platformJson.TryGetProperty(
                    "platformCode",
                    out var codeElement)
                ? codeElement.GetString()
                : string.Empty;

            var platform =
                await _db.Platforms.FindAsync(
                    platformId);

            if (platform == null)
            {
                platform = new Platform
                {
                    Id = platformId
                };

                _db.Platforms.Add(platform);
            }

            platform.PlatformName = platformName;
            platform.PlatformCode = platformCode;
            platform.UpdatedDate = DateTime.Now;

            if (platformJson.TryGetProperty(
                    "wells",
                    out var wells))
            {
                foreach (var wellJson
                         in wells.EnumerateArray())
                {
                    int wellId =
                        wellJson.TryGetProperty(
                            "id",
                            out var wid)
                        ? wid.GetInt32()
                        : 0;

                    string wellName =
                        wellJson.TryGetProperty(
                            "wellName",
                            out var wn)
                        ? wn.GetString()
                        : "";

                    string wellCode =
                        wellJson.TryGetProperty(
                            "wellCode",
                            out var wc)
                        ? wc.GetString()
                        : "";

                    var well =
                        await _db.Wells.FindAsync(
                            wellId);

                    if (well == null)
                    {
                        well = new Well
                        {
                            Id = wellId
                        };

                        _db.Wells.Add(well);
                    }

                    well.WellName = wellName;
                    well.WellCode = wellCode;
                    well.PlatformId = platformId;
                    well.UpdatedDate = DateTime.Now;
                }
            }
        }

        await _db.SaveChangesAsync();
    }
}