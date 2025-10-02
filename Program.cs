using AssistedCustody.Data;
using Microsoft.EntityFrameworkCore;
using NBitcoin;
using NBitcoin.RPC;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("AssistedCustodyDatabase")
    ?? throw new InvalidOperationException("Connection string 'AssistedCustodyDatabase' not found.");

builder.Services.AddDbContext<AssistedCustodyDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>().GetSection("BitcoinRpc");
    var networkName = config["Network"];
    return Network.GetNetwork(networkName ?? string.Empty) ?? Network.RegTest;
});

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>().GetSection("BitcoinRpc");
    var credentials = new NetworkCredential(config["Username"], config["Password"]);
    var url = new Uri(config["Url"]!);
    var network = sp.GetRequiredService<Network>();
    return new RPCClient(credentials, url, network);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AssistedCustodyDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();
