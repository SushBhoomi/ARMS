using ARMS.Core.Models;

namespace ARMS.Core.Entities
{
    public class RoleModel : Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Activated { get; set; }
        public RoleStatus Status { get; set; }
        //public bool IsOfficial { get; set; }

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
        public virtual ICollection<RolePermissionModel> RolePermissions { get; set; }
    }
}
