using ARMS.Application.Common;
using ARMS.Application.Entities.DBType.Validations;
using ARMS.Application.Entities.Users.Validations;
using ARMS.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.DBType.ViewModel
{
    public class DBTypeViewModel : ViewModelBase<DBTypeMasterModel>
    {
        public int Id { get; set; }
        public string DBTypeName { get; set; }
        public string IsActive { get; set; }
        public override void ValidateAndThrow(bool isUpdating = false)
        {
            new DBTypeValidation().ValidateAndThrow(this);
        }
    }
}
