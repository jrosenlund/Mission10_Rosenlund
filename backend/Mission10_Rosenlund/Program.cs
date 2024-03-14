using Microsoft.EntityFrameworkCore;
using Mission10_Rosenlund.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Open access to data
builder.Services.AddCors();

// Add DbContext
builder.Services.AddDbContext<BowlingLeagueContext>(options =>
    {
        options.UseSqlite(builder.Configuration["ConnectionStrings:BowlingConnection"]);
    }
);

// Use repository pattern
builder.Services.AddScoped<IBowlingRepository, EFBowlingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
