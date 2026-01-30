using ARMS.Application.Common;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Entities.LoginMaster.ViewModels
{
    public class LoginViewModel : ViewModelBase<LoginMasterModel>
    {
        public long Id { get; set; }

        public string? AccountNumber { get; set; } //ID

        public string Username { get; set; }
        public string Password { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Title { get; set; }

        public string? Department { get; set; }

        public string? ProfilePicture { get; set; }

        public string? Phone { get; set; }
        public override void ValidateAndThrow(bool isUpdating = false)
        {
            throw new NotImplementedException();
        }
    }
}
