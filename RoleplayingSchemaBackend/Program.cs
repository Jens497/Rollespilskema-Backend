using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;
using RoleplayingSchemaBackend.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mediatr, CQRS testing
builder.Services.AddMediatR(mdt => mdt.RegisterServicesFromAssemblies(typeof(Program).Assembly));
//This should be changed to Scoped life time rather than singleton WHEN the database will be connected

//Database
builder.Services.AddDbContext<RoleplayingDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOpts =>
        {
            sqlOpts.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        }));

//Validator
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipeline<,>));
builder.Services.AddTransient<MiddlewareExcepitonHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();//.AddFluentValidationClientsideAdapters();

builder.Services.AddIdentity<Users, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<RoleplayingDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
});

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.Cookie.HttpOnly = true;
    //This should be something like 60, but is set to one for testing purposes.
    opts.ExpireTimeSpan = TimeSpan.FromMinutes(120);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<MiddlewareExcepitonHandler>();


app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(builder =>
{
    var corsSettings = app.Configuration.GetSection("Cors").Get<CorsSettings>();

    builder.WithOrigins(corsSettings.AllowedOrigins);
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();