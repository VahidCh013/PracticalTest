using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PracticalTest.Domain.Read.Users;
using PracticalTest.Infrastructure;
using PracticalTest.Infrastructure.Read;
using PracticalTest.Infrastructure.Read.Repositories;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appConfiguration = builder.Configuration;
builder.Services.AddReadDependencies(appConfiguration);
builder.Services.AddWriteDependencies(appConfiguration);


//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddAuthorization();

//Register
builder.Services.AddScoped<DbContextFactory<PracticalTestWriteDbContext>>();
builder.Services.AddScoped<DbContextFactory<PracticalTestReadDbContext>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    using (var writeDb = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PracticalTestWriteDbContext>>().CreateDbContext())
        writeDb.Database.Migrate();
    using (var readDb = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PracticalTestReadDbContext>>().CreateDbContext())
        readDb.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
