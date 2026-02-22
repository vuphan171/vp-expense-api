using System.Data;
using System.Data.Common;

namespace ExpenseTracker.Application.Abstractions;


public interface IUnitOfWork
{
    IDbConnection Connection { get; }

    IDbTransaction? Transaction { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
}