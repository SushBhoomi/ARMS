using ARMS.Application.Entities.LoginMaster.ViewModels;
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
    public class LoginRepository : Repository<ApplicationDbContext, LoginMasterModel>, ILoginRepository
    {
        public LoginRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
