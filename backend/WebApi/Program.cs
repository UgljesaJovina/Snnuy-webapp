using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Repositories.DAL;
using Repositories.Interfaces;
using Repositories.Repositories;
using Repositories.Utility;
using Services.Interfaces;
using Services.Services;
using Services.Utility.Hashing;
using Services.Utility.JWT;
using WebApi.Utils.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependency Injection

    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings")); 
    // ovo je forma DI gde u konstruktor ide IOptions<AppSettings> i onda trazim value property od objekta 

    builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
    builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
    builder.Services.AddScoped<ICustomCardRepo, CustomCardRepo>();
    builder.Services.AddScoped<IDeckRepo, DeckRepo>();
    builder.Services.AddScoped<IUserRepo, UserRepo>();  
    builder.Services.AddScoped<ICustomCardService, CustomCardService>();
    builder.Services.AddScoped<IDeckService, DeckService>();
    builder.Services.AddScoped<IUserService, UserService>();  

    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            x => { 
                x.MigrationsAssembly("Repositories"); 
                x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }
        )
    );

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseMiddleware<JWTMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions() {
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = new PathString("/public")
});

app.Run();
