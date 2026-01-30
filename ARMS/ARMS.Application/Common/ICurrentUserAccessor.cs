using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Application.Common
{
    public interface ICurrentUserAccessor
    {
        IdentityInfo CurrentUser { get; }
    }
}
