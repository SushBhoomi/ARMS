using System.ComponentModel.DataAnnotations;

namespace ARMS.Core.Models
{
    public enum UserStatus
    {
        [Display(Name = "label.deleted", Description = "OrganizationDeletedDescription")]
        Deleted = -1,
        [Display(Name = "label.rejected", Description = "OrganizationRejectedDescription")]
        Rejected = 0,
        [Display(Name = "label.pending", Description = "OrganizationPendingDescription")]
        Pending = 1,
        [Display(Name = "label.active", Description = "OrganizationActiveDescription")]
        Active = 2,
        [Display(Name = "label.inactive", Description = "OrganizationActiveDescription")]
        Inactive = 3,
        [Display(Name = "label.waitForConfirm", Description = "OrganizationActiveDescription")]
        WaitForConfirm = 4
    }

    public enum RoleStatus
    {
        [Display(Name = "label.active", Description = "RoleActiveDescription")]
        Active = 1,
        [Display(Name = "label.inactive", Description = "RoleInActiveDescription")]
        Inactive = 0
    }

    public enum Role
    {
        [Display(Description = "System Admin")]
        SystemAdmin = 1,
        [Display(Description = "CSR")]
        CSR = 2,
        [Display(Description = "Registered User")]
        RegisteredUser = 3,
        [Display(Description = "Guest")]
        Guest = 4,
        [Display(Description = "Pending")]
        Pending = 5
    }
}
