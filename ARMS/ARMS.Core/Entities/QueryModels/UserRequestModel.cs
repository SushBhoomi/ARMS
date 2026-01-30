using ARMS.Core.Models;

namespace ARMS.Core.Entities.QueryModels
{
    public class UserRequestModel : Entity
    {
        public long Id { get; set; }

        public string AccountNumber { get; set; } //ID

        public string Username { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public UserStatus Status { get; set; }

        public long? RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
