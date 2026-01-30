using ARMS.Application.Interfaces.Repository;
using AutoMapper;

namespace ARMS.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        Task SaveChangesAsync();

        Task SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();

        TRepository GetCustomRepository<TRepository>();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        IMapper GetMapper();
    }
}
