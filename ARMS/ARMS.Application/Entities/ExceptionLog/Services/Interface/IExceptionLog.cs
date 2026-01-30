using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Application.Entities.ExceptionLog.ViewModel;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.ExceptionLog.Services.Interface
{
    public interface IExceptionLog : IServiceBase<ExceptionLogModel, ExceptionLogViewModel>
    {
        new Task<ExceptionLogViewModel> CreateAsync(ExceptionLogViewModel viewModel);
    }
}
