var db = new AppDbContext();

db.Database.EnsureCreated();

var apiService = new ApiService();

var syncService =
    new SyncService(db);

Console.WriteLine("Login...");

string token =
    await apiService.LoginAsync();

Console.WriteLine("Token received.");

Console.WriteLine("Downloading data...");

var data =
    await apiService.GetPlatformWellActual(
        token);

Console.WriteLine("Syncing...");

await syncService.SyncAsync(data);

Console.WriteLine("Completed.");