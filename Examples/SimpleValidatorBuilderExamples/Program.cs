using Microsoft.EntityFrameworkCore;
using SimpleValidatorBuilderExamples.Domain.Aggregates.UserEntity;
using SimpleValidatorBuilderExamples.Domain.ValueObjects;
using SimpleValidatorBuilderExamples.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SimpleValidatorBuilderDbContext>(config =>
{
    config.UseSqlServer("Server=localhost;Database=SimpleValidatorBuilderExamples;User ID=sa;Password=YDaPKhLUsp5XuKpHWXVcBgAz3XK9Umqh");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetService<SimpleValidatorBuilderDbContext>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    var standardUser = new SimpleValidatorBuilderExamples.Domain.Aggregates.SettingEntity.UserType("STD", "Standard", false);
    context.Add(standardUser);
    context.SaveChanges();

    var firstName = PersonName.Create("David").Value;
    var lastName = PersonName.Create("Gagné").Value;
    var user = RegistredUser.CreateNew(firstName, lastName, standardUser.Id);

    context.RegistredUsers.Add(user);
    context.SaveChanges();

    var userLoaded = context.RegistredUsers
        .Include(e => e.UserType)
        .FirstOrDefault();
}

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
