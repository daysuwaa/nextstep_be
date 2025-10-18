using nextstep.Endpoints.AuthenticationEndpoints;
using nextstep.Endpoints.EntriesEndpoints;
//using nextstep.Endpoints.UserEndpoints;
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
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load("../.env");
builder.Configuration.AddEnvironmentVariables();

var config = builder.Configuration;


var dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? throw new Exception("DB_CONNECTION not found in environment variables");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dbConnection)
);


builder.Services.Configure<nextstep.application.Configurations.CloudinarySettings>(builder.Configuration.GetSection(nameof(Cloudinary)));

//  CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "https://nextstep-fe.onrender.com",
            "http://localhost:3000",
            "https://localhost:3000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// configure Cloudinary account and register Cloudinary client
var cloudConfig = builder.Configuration.GetSection("Cloudinary");

var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

Console.WriteLine($"Cloud name: {cloudName}");

var account = new Account(cloudName, apiKey, apiSecret);
builder.Services.AddSingleton(new CloudinaryDotNet.Cloudinary(account));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthHandler, AuthHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IUserHandler, UserHandler>();


builder.Services.AddValidatorsFromAssemblyContaining<LoginReqValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();


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
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("JWT_KEY not found in environment variables"))
            )
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
app.UseCors("AllowFrontend");

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
//app.AddUserEndpoint();

app.Run();


