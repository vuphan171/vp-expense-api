using System.Data;
using System.Data.Common;
using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Infrastructure.Persistence;

public class UnitOfWork(IDbConnectionFactory factory) : IUnitOfWork, IDisposable
{
    private readonly IDbConnection _connection = factory.CreateConnection();
    private IDbTransaction? _transaction;

    public IDbConnection Connection => _connection;
    public IDbTransaction? Transaction => _transaction;

    public void BeginTransaction()
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
        DisposeTransaction();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        DisposeTransaction();
    }

    private void DisposeTransaction()
    {
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}