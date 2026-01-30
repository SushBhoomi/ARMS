using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.DBType.Validations
{
    public class DBTypeValidation : BaseValidation<DBTypeViewModel>
    {
        public DBTypeValidation()
        {
            RuleFor(a => a.DBTypeName).NotEmpty();
            RuleFor(a => a.IsActive).NotEmpty();
        }
    }
}
