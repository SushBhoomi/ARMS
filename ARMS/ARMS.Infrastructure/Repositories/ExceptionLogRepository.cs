using ARMS.Application.Interfaces.Repository;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using ARMS.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Infrastructure.Repositories
{
    public class ExceptionLogRepository : Repository<ApplicationDbContext, ExceptionLogModel>, IExceptionRepository
    {
        public ExceptionLogRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
