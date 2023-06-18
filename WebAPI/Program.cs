using WebAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string dbFolderPath = Path.Combine(builder.Environment.ContentRootPath, "DB");
string dbFilePath = Path.Combine(dbFolderPath, "API.db");

// Create the DB folder if it doesn't exist
if (!Directory.Exists(dbFolderPath))
{
    Directory.CreateDirectory(dbFolderPath);
}

// Create the database file if it doesn't exist
if (!File.Exists(dbFilePath))
{
    File.Create(dbFilePath).Close();
}
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite($"Data Source={dbFilePath}");
});

var app = builder.Build();
app.UseHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
dbContext.InitializeDatabase(); // Seed data if necessary
app.Run();
