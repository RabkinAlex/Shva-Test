using Microsoft.EntityFrameworkCore;
using TCPListener;
using TCPListener.Interfaces;
using TCPListener.Persistence;
using TCPListener.Services;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionstring = configuration.GetConnectionString("DBConnectionString");
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IEncryptor, EncryptionService>();
builder.Services.AddDbContext<EncDataDbContext>(options =>
    options.UseSqlServer(connectionstring));
builder.Services.AddScoped<IDbManager, DbManager>();
var host = builder.Build();
host.Run();
