using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Repositories.DAL;
using WebApi.Utils;
using WebApi.Utils.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings")); 
// ovo je forma DI gde u konstruktor ide IOptions<AppSettings> i onda trazim value property od objekta 

// builder.Services.Configure<IdentityOptions>(options => {
//     options.Password
// });

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly("Repositories"))
);

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
