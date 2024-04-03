using InsuranceCalc.Business;
using InsuranceCalc.Data.Business;
using InsuranceCalc.Data.Contracts;
using InsuranceCalc.Models;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InsuranceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddScoped<CustomerManagerFactory>();
builder.Services.AddScoped<ICalcPremiumRepository, CalcPremiumRepository>();
builder.Services.AddScoped<ILookUpRepository, LookUpRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Logging.ClearProviders();
builder.WebHost.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
