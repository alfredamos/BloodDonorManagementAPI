using BloodDonorDataAccessEF.Contracts;
using BloodDonorDataAccessEF.Data;
using BloodDonorDataAccessEF.Repositories;
using BloodDonorManagementAPI.Mapings;
using BloodDonorModels.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(policy => policy.AddPolicy("CorsPolicy", builder =>
               builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()
               ));

builder.Services.AddDbContext<DonorDB>(options => {  
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDonorDataRepository, DonorDataRepository>();
builder.Services.AddScoped<IRepository<DonorData>, Repository<DonorData>>();

builder.Services.AddAutoMapper(typeof(Map));

builder.Services.Configure<RouteOptions>(option => {
    option.LowercaseUrls = true;
    option.LowercaseQueryStrings = true;
    option.AppendTrailingSlash = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
