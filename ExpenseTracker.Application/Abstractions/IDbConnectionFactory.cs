using System.Data;

namespace ExpenseTracker.Application.Abstractions;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}