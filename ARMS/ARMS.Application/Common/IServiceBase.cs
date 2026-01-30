using ARMS.Core.Data;
using System.Linq.Expressions;

namespace ARMS.Application.Common
{
    public interface IServiceBase<TModel, TViewModel> : IDisposable
    {
        Task<DataSourceResult> ListAsync(DataSourceRequest request);

        Task<DataSourceResult> GetListAsync(DataSourceRequest request, Func<IQueryable<TModel>, IQueryable<TModel>> includes = null, Expression<Func<TModel, bool>> customFilters = null);

        Task<IEnumerable<TViewModel>> GetAllAsync();

        Task<TViewModel> GetAsync(params object[] keys);

        Task<TViewModel> CreateAsync(TViewModel viewModel);

        Task<TViewModel> UpdateAsync(TViewModel viewModel, params object[] keys);

        Task<bool> DeleteByKeysAsync(params object[] keys);
    }
}
