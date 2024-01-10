using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoleplayingSchemaBackend;
using RoleplayingSchemaBackend.Commands;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Handlers;
using RoleplayingSchemaBackend.Handlers.Commands;
using RoleplayingSchemaBackend.Handlers.Queries;
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

//Are each of these even needed since swaggers can do this?
//builder.Services.AddScoped<GetUsersQuery>();
//builder.Services.AddScoped<GetUsersHandler>();
//builder.Services.AddScoped<AddUserCommand>();
//builder.Services.AddScoped<AddUserHandler>();

//Database
builder.Services.AddDbContext<RoleplayingDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//opts.UseSqlServer(builder.Configuration.GetConnectionString("RPDBConnection")));

//Identity
//builder.Services.AddIdentityCore<Users>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<RoleplayingDbContext>();
builder.Services.AddIdentity<Users, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<RoleplayingDbContext>();

//Authentication
/*builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
