using RoleplayingSchemaBackend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mediatr, CQRS testing
builder.Services.AddMediatR(mdt => mdt.RegisterServicesFromAssemblies(typeof(Program).Assembly));
//This should be changed to Scoped life time rather than singleton WHEN the database will be connected
builder.Services.AddSingleton<UserData>();

//Done

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
