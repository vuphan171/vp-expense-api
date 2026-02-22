using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Infrastructure.Persistence;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("ExpenseTrackerDb")
                                                ?? throw new InvalidOperationException(
                                                    "Connection string 'DefaultConnection' not found.");

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}