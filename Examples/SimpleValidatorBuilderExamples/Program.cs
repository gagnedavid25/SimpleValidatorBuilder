using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilderExamples.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(config => 
    config.UseSqlServer("Server=localhost;Database=SimpleValidatorBuilderExamples;User ID=sa;Password=YDaPKhLUsp5XuKpHWXVcBgAz3XK9Umqh"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
