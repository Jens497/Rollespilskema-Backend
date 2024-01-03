using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend;
using RoleplayingSchemaBackend.Commands;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers;
using RoleplayingSchemaBackend.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mediatr, CQRS testing
builder.Services.AddMediatR(mdt => mdt.RegisterServicesFromAssemblies(typeof(Program).Assembly));
//This should be changed to Scoped life time rather than singleton WHEN the database will be connected
//builder.Services.AddSingleton<UserData>();

//Done
builder.Services.AddScoped<GetUsersQuery>();
builder.Services.AddScoped<GetUsersHandler>();

//builder.Services.AddScoped<AddUserCommand>();
//builder.Services.AddScoped<AddUserHandler>();

//Database
builder.Services.AddDbContext<RoleplayingDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    //opts.UseSqlServer(builder.Configuration.GetConnectionString("RPDBConnection")));

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
