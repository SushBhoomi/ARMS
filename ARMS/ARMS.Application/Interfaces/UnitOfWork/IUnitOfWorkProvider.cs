namespace ARMS.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false);

        IUnitOfWork CreateUnitOfWorkForBackgroundJob(bool trackChanges = true, bool enableLogging = false);
    }
}
