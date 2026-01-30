using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.ViewModel;
using ARMS.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.ExceptionLog.Validations
{
    public class ExceptionLogValidation : BaseValidation<ExceptionLogModel>
    {
        public ExceptionLogValidation()
        {
            RuleFor(a => a.Exception).NotEmpty();
            RuleFor(a => a.Message).NotEmpty();
            RuleFor(a => a.LogLevel).NotEmpty();
        }
    }
}
