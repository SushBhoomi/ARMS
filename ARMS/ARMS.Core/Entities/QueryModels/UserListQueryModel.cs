using ARMS.Core.Models;

namespace ARMS.Core.Entities.QueryModels
{
    public class UserListQueryModel
    {
        public long Id { get; set; }

        public string AccountNumber { get; set; } //ID

        public string Email { get; set; }

        public string Name { get; set; }

        public string RoleName { get; set; }

        public UserStatus Status { get; set; }

        public string StatusName { get; set; }

        public DateTime LastSignInDate { get; set; }
    }
}
