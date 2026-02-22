using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Features.Customers;
using ExpenseTracker.Domain.Customers;
using ExpenseTracker.Infrastructure.Customers;
using ExpenseTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<
    IPasswordHasher<Customer>,
    PasswordHasher<Customer>
>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();