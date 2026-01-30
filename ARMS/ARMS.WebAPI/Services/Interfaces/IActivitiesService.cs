using ARMS.Core.Entities;
using ARMS.WebAPI.Models;

namespace ARMS.WebAPI.Services.Interfaces
{
    public interface IActivitiesService
    {
        Task<List<ActivityLogResult>> GetActivityLog(int id, string username = null);

        //Task<List<ActivityLogResult>> GetActivityLog(int id, DateTime startDate, DateTime endDate, string username=null);
    }
}
