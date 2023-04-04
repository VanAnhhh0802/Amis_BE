using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL;
using MISA.Amis.BL.AccountBL;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL.ObjectBL;
using MISA.Amis.BL.PaymentBL;
using MISA.Amis.BL.PaymentDetailBL;
using MISA.Amis.Common.Entities;
using MISA.Amis.DL;
using MISA.Amis.DL.AccontDL;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.EmployeeDL;
using MISA.Amis.DL.ObjectDL;
using MISA.Amis.DL.PaymentDL;
using MISA.Amis.PaymentBL;

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
builder.Services.AddScoped<IAccountBL ,AccountBL>();
builder.Services.AddScoped<IAccountDL, AccountDL>();
builder.Services.AddScoped<IPaymentBL, PaymentBL>();
builder.Services.AddScoped<IPaymentDL, PaymentDL>();
builder.Services.AddScoped<IPaymentDetailBL, PaymentDetailBL>();
builder.Services.AddScoped<IPaymentDetailDL, PaymentDetailDL>();
builder.Services.AddScoped<IObjectBL, ObjectBL>();
builder.Services.AddScoped<IObjectDL, ObjectDL>();
builder.Services.AddScoped<IDatabaseConnection, DatabaseConnection>();

// Lấy giá trị connect tới database
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", bulid =>
{
    bulid.WithOrigins("http://localhost:8080", "https://localhost:7232").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null); // chuyển pascal case

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
