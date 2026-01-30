using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.Services.Interfece;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.ExceptionLog.Services.Interface;
using ARMS.Application.Entities.ExceptionLog.ViewModel;
using ARMS.Application.Interfaces.UnitOfWork;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.ExceptionLog.Services
{
    public class ExceptionLogService : ServiceBase<ExceptionLogModel, ExceptionLogViewModel>, IExceptionLog
    {
        public ExceptionLogService(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {

        }

       
    }
}
