using Ivan.DianPing.AutoMapper;
using Ivan.DianPing.Repository;
using Ivan.DianPing.Services;
using Ivan.DianPing.Services.Impl;
using Ivan.DianPing.V2.Filters;
using Ivan.DianPing.V2.Repository;
using Ivan.DianPing.V2.Services;
using Ivan.DianPing.V2.Services.Impl;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<RedisService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ShopTypeRepository>();
builder.Services.AddScoped<IShopTypeService, ShopTypeService>();
builder.Services.AddScoped<ShopRepository>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddAutoMapper(typeof(AppProfile).Assembly);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();


// 添加 HttpContext 访问（用于访问 Session）
builder.Services.AddHttpContextAccessor();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RefreshTokenFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
