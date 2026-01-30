using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Core.Entities
{
    public class DBTypeMasterModel : Entity
    {
        public int Id { get; set; }
        public string DBTypeName { get; set; }
        public string IsActive { get; set; }


    }
}
