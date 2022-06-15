using Abstractions;
using Application;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("BackEndAdminPro"))
);

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var algo = Assembly.GetAssembly(typeof(Application<>));

builder.Services.Scan(scan =>
                scan.FromAssemblies(Assembly.GetAssembly(typeof(Application<>)))
                 .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Application")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

builder.Services.Scan(scan =>
    scan.FromAssemblies(Assembly.GetAssembly(typeof(Repository<>)))
     .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.Scan(scan =>
   scan.FromAssemblies(Assembly.GetAssembly(typeof(DbContext<>)))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("DbContext")))
   .AsImplementedInterfaces()
   .WithTransientLifetime());

builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));

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

app.UseCors("AllowWebApp");

app.Run();
