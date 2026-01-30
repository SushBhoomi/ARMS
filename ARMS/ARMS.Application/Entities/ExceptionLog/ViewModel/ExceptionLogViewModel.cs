using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.Validations;
using ARMS.Application.Entities.ExceptionLog.Validations;
using ARMS.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.ExceptionLog.ViewModel
{
    public class ExceptionLogViewModel : ViewModelBase<ExceptionLogModel>
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public override void ValidateAndThrow(bool isUpdating = false)
        {
            new ExceptionLogValidation();
        }
    }
}
