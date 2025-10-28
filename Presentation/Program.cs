using DataAccess.DatabaseContext;
using Presentation.Profiles;
using BusinessLogic.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Presentation")
    ));

builder.Services.AddScoped<AuthorRepository>(provider =>
{
    var contextOptions = provider.GetRequiredService<DbContextOptions<LibraryContext>>();
    return new AuthorRepository(contextOptions);
});

builder.Services.AddScoped<BookRepository>(provider =>
{
    var contextOptions = provider.GetRequiredService<DbContextOptions<LibraryContext>>();
    return new BookRepository(contextOptions);
});

builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Library API",
        Description = "ASP.NET Core Web API for Library Management"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
