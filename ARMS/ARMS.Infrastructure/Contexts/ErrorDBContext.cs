using ARMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Infrastructure.Contexts
{
    public class ErrorDBContext : DbContext
    {
       

        // Corrected the options type
        public ErrorDBContext(DbContextOptions<ErrorDBContext> options, ILoggerFactory loggerFactory) : base(options)
        {
           
        }

        public virtual DbSet<ErrorModel> Errors { get; set; }
        public virtual DbSet<ExceptionLogModel> ExceptionLog { get; set; }
        public virtual DbSet<ActivityLogModel> ActivityLog { get; set; }
        public virtual DbSet<ActivityModel> Activity { get; set; }
        
    }
}
