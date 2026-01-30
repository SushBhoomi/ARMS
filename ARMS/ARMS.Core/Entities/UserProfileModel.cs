using ARMS.Core.Models;

namespace ARMS.Core.Entities
{
    public class UserProfileModel : Entity
    {
        public long Id { get; set; }

        public string? AccountNumber { get; set; } //ID

        public string Username { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Title { get; set; }

        public string? Department { get; set; }

        public string? ProfilePicture { get; set; }

        public string? Phone { get; set; }

        public UserStatus Status { get; set; }

        public DateTime LastSignInDate { get; set; }

        public string? DateFormat { get; set; }

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }

        protected override void AuditChildren(string user)
        {
            if (UserRoles != null)
            {
                foreach (var userRole in UserRoles)
                {
                    userRole.Audit(user);
                }
            }
        }
    }

}
