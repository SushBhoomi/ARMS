using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMS.Core.Entities
{
    public class ActivityLogModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public long ActivityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }

        public ActivityModel Activity { get; set; }

    }
}
