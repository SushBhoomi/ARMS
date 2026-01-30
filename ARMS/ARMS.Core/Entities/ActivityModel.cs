using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Core.Entities
{
    public class ActivityModel : Entity
    {
        public long Id { get; set; }

        public string Activity { get; set; }
        public string Message { get; set; }

        public virtual ICollection<ActivityLogModel> Activities { get; set; }

    }
}
