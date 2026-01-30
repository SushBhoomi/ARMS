using ARMS.Infrastructure.Contexts;
using ARMS.WebAPI.Models;
using ARMS.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ARMS.WebAPI.Services
{
    public class ActivityService : IActivitiesService
    {
        private readonly ErrorDBContext _dbcontext;
        
        public ActivityService(ErrorDBContext dbcontext)
        {
                _dbcontext = dbcontext;
        }

        public async Task<List<ActivityLogResult>> GetActivityLog(int id, string username = null)
        {
            var query = _dbcontext.ActivityLog
                          .Where(a => (id != 0 && a.UserId == id) || (!string.IsNullOrEmpty(username) && a.UserName == username))
                          .OrderByDescending(a => a.Timestamp)
                          .Join(_dbcontext.Activity,
                                a => a.ActivityId,
                                b => b.Id,
                                (a, b) => new ActivityLogResult
                                {
                                    UserId = a.UserId,
                                    UserName = a.UserName,
                                    Activity = b.Activity,
                                    Timestamp = a.Timestamp,
                                    IpAddress = a.IpAddress,
                                    Message = b.Message
                                });

            return await query.ToListAsync();
        }


        //public async Task<List<ActivityLogResult>> GetActivityLog(int id, DateTime startDate, DateTime endDate, string username)
        //{

        //    if (startDate == DateTime.MinValue && endDate == DateTime.MinValue)
        //    {
        //        var query = from a in _dbcontext.ActivityLog
        //                    join b in _dbcontext.Activity on a.ActivityId equals b.Id
        //                    where (id != 0 && a.UserId == id) || (!string.IsNullOrEmpty(username) && a.UserName == username)
        //                    orderby a.Timestamp descending
        //                    select new ActivityLogResult
        //                    {
        //                        UserId = a.UserId,
        //                        UserName = a.UserName,
        //                        Activity = b.Activity,
        //                        Timestamp = a.Timestamp,
        //                        IpAddress = a.IpAddress,
        //                        Message = b.Message
        //                    };

        //        return await query.ToListAsync();
        //    }
        //    else
        //    {
        //        var query = from a in _dbcontext.ActivityLog
        //                    join b in _dbcontext.Activity on a.ActivityId equals b.Id
        //                    where (id != 0 && a.UserId == id) || (!string.IsNullOrEmpty(username) && a.UserName == username)
        //                    orderby a.Timestamp descending
        //                    select new ActivityLogResult
        //                    {
        //                        UserId = a.UserId,
        //                        UserName = a.UserName,
        //                        Activity = b.Activity,
        //                        Timestamp = a.Timestamp,
        //                        IpAddress = a.IpAddress,
        //                        Message = b.Message
        //                    };

        //        return await query.ToListAsync();
        //    }

        //}

    }
}
