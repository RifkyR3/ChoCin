using ChoCin.Entities;
using ChoCin.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//*********************** Add services to the container.***********************
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
//*********************** Add services to the container end.***********************

//*********************** Register DbContext and provide ConnectionString .***********************
var connectionString = builder.Configuration.GetConnectionString("ChocinDbContext");
builder.Services.AddDbContext<ChocinDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
//*********************** Register DbContext end.***********************

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();