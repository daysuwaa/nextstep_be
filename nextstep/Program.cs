using nextstep.Endpoints.AuthenticationEndpoints;
using nextstep.Endpoints.EntriesEndpoints;
using nextstep.Endpoints.UserEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using nextstep.Models.Requests;
using nextstep.application.Abstractions.Service;
using nextstep.application.Services;
using nextstep.application.Abstractions.Core;
using nextstep.application.Handlers;
using nextstep.application.InterFaces;
using nextstep.infrastructure.Repository;
using nextstep.application.Interfaces;
using nextstep.infrastructure;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.Configure<nextstep.application.Configurations.Cloudinary>(builder.Configuration.GetSection(nameof(Cloudinary)));

//  CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// configure Cloudinary account and register Cloudinary client
var cloudConfig = builder.Configuration.GetSection("Cloudinary");
var account = new Account(
    cloudConfig["CloudName"],
    cloudConfig["ApiKey"],
    cloudConfig["ApiSecret"]
);

builder.Services.AddSingleton(new Cloudinary(account));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthHandler, AuthHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IUserHandler, UserHandler>();


builder.Services.AddValidatorsFromAssemblyContaining<LoginReqValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

//builder.Services.AddControllers(options =>
//{
//    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
//    options.Filters.Clear(); // remove automatic antiforgery filters
//});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAntiforgery();

builder.Services.AddControllers(options =>
{
    // Ensures antiforgery filters aren’t applied implicitly
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Filters.Clear();
});

var app = builder.Build();

app.UseCors("AllowNextJs");

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.AddEntriesEndpoints();
app.AddAuthenticationEndpoint();
app.AddUserEndpoint();

app.Run();


