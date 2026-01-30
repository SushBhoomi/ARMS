using ARMS.Core;
using ARMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Interfaces.Repository
{
    public interface IDBTypeRepository : IRepository<DBTypeMasterModel>
    {
        IQueryable<DBTypeMasterModel> AsQueryable(string name, string status);
    }
}
