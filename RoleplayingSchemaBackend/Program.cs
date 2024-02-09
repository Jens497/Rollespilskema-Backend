using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleplayingSchemaBackend.Data;
using RoleplayingSchemaBackend.Middleware;

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

//Validator
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipeline<,>));
builder.Services.AddTransient<MiddlewareExcepitonHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();//.AddFluentValidationClientsideAdapters();

//Identity
//builder.Services.AddIdentityCore<Users>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<RoleplayingDbContext>();

/*builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = IdentityConstants.ApplicationScheme;
    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies(o => { });*/

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
    opts.ExpireTimeSpan = TimeSpan.FromMinutes(1);
});

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

app.UseMiddleware<MiddlewareExcepitonHandler>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
