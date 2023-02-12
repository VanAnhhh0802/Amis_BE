using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities;
using MISA.Amis.DL;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.EmployeeDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject đối tượng được khởi tạo từ class vào interface
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IDatabaseConnection, DatabaseConnection>();

// Lấy giá trị connect tới database
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", bulid =>
{
    bulid.WithOrigins("http://localhost:8080", "https://localhost:7232").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
