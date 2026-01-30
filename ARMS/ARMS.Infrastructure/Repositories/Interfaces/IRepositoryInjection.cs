using Microsoft.EntityFrameworkCore;

namespace ARMS.Infrastructure.Repositories.Interfaces
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}
