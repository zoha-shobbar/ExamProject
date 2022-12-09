using ExamProject.Application.Contracts;
using ExamProject.Infrastructure.Extensions;
using ExamProject.Infrastructure.Persistence;
using ExamProject.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"))
    );

builder.Services.AddCustomIdentity(builder.Configuration);

builder.Services.AddScoped<IDataInitializer, UserDataInitializer>();

var app = builder.Build();

app.IntializeDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
