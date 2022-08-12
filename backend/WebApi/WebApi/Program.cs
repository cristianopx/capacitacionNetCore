using Core.Interfaces;
using Core.Managers;
using Model.Context;
using Model.Mappings;

var builder = WebApplication.CreateBuilder(args);
var connecyionString = builder.Configuration.GetConnectionString("DefaultConnection");
//agregar cadena de connexion
builder.Services.AddDbContext<AppDbContext>();
// Add services to the container.
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddAutoMapper(typeof(MappingsProfiles));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
