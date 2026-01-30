using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ARMS.Infrastructure.UnitOfWork
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false)
        {
            var context = (DbContext)_serviceProvider.GetService(typeof(ApplicationDbContext));

            if (!trackChanges)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            var uow = new UnitOfWork(context, _serviceProvider);
            return uow;
        }

        public IUnitOfWork CreateUnitOfWorkForBackgroundJob(bool trackChanges = true, bool enableLogging = false)
        {
            var context = (DbContext)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(ApplicationDbContext));

            if (!trackChanges)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            var uow = new UnitOfWork(context, _serviceProvider);
            return uow;
        }
    }
}
