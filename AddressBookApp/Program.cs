using BusinessLayer.Helper;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using ModelLayer.Validators;
using RepositoryLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddressBookEntryValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<JwtTokenGenerator>();


// Configure Entity Framework Core
builder.Services.AddDbContext<AddressBookDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository and Service in Dependency Injection
builder.Services.AddScoped<IAddressBookRL, AddressBookRL>();  // Repository Layer
builder.Services.AddScoped<IAddressBookBL, AddressBookBL>();  // Business Logic Layer
builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IUserBL, UserBL>();




// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssemblyContaining<AddressBookEntryValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
