namespace ARMS.Application.Authorization
{
    public static class AppPermissions
    {
        //User
        public const string User = "User";
        public const string User_RequestList = "User.RequestList";
        public const string User_RequestDetail = "User.RequestDetail";
        public const string User_RequestDetail_Edit = "User.RequestDetail.Edit";
        public const string User_UserList = "User.UserList";
        public const string User_UserDetail = "User.UserDetail";
        public const string User_UserDetail_Add = "User.UserDetail.Add";
        public const string User_UserDetail_Edit = "User.UserDetail.Edit";
        public const string User_RoleList = "User.RoleList";
        public const string User_RoleDetail = "User.RoleDetail";
        public const string User_RoleDetail_Edit = "User.RoleDetail.Edit";

        public const string Master_Application_DLMaster_Add = "Master_Application_DLMaster_Add";
        public const string Master_Application_DLMaster_Edit = "Master_Application_DLMaster_Edit";
        public const string Master_Application_DLMaster_Delete = "Master_Application_DLMaster_Delete";
        public const string Master_Application_DLMaster_List = "Master_Application_DLMaster_List";

        public const string Master_Application_OtherEntitlment_Add = "Master_Application_OtherEntitlment_Add";
        public const string Master_Application_OtherEntitlment_Edit = "Master_Application_OtherEntitlment_Edit";
        public const string Master_Application_OtherEntitlment_Delete = "Master_Application_OtherEntitlment_Delete";
        public const string Master_Application_OtherEntitlment_List = "Master_Application_OtherEntitlment_List";

        public const string Master_Application_ExceptionApprover_Add = "Master_Application_ExceptionApprover_Add";
        public const string Master_Application_ExceptionApprover_Edit = "Master_Application_ExceptionApprover_Edit";
        public const string Master_Application_ExceptionApprover_Delete = "Master_Application_ExceptionApprover_Delete";
        public const string Master_Application_ExceptionApprover_List = "Master_Application_ExceptionApprover_List";

        public const string Master_Infra_InfraDBType_Add = "Master_Infra_InfraDBType_Add";
        public const string Master_Infra_InfraDBType_Edit = "Master_Infra_InfraDBType_Edit";
        public const string Master_Infra_InfraDBType_Delete = "Master_Infra_InfraDBType_Delete";
        public const string Master_Infra_InfraDBType_List = "Master_Infra_InfraDBType_List";
        
    }

    public class AppPermissionOrders
    {
        // The inner dictionary.
        // Key: Permission name
        // Value: Order value
        public Dictionary<string, int> PermissionOrderDictionary { get; private set; }

        public AppPermissionOrders()
        {
            // Seed data into dictionary when it has been called by constructor
            PermissionOrderDictionary = new Dictionary<string, int>() { };


            //Users
            PermissionOrderDictionary.Add(AppPermissions.User, 1801);
            PermissionOrderDictionary.Add(AppPermissions.User_RequestList, 1802);
            PermissionOrderDictionary.Add(AppPermissions.User_RequestDetail, 1803);
            PermissionOrderDictionary.Add(AppPermissions.User_RequestDetail_Edit, 1804);
            PermissionOrderDictionary.Add(AppPermissions.User_UserList, 1805);
            PermissionOrderDictionary.Add(AppPermissions.User_UserDetail, 1806);
            PermissionOrderDictionary.Add(AppPermissions.User_UserDetail_Add, 1807);
            PermissionOrderDictionary.Add(AppPermissions.User_UserDetail_Edit, 1808);
            PermissionOrderDictionary.Add(AppPermissions.User_RoleList, 1809);
            PermissionOrderDictionary.Add(AppPermissions.User_RoleDetail, 1810);
            PermissionOrderDictionary.Add(AppPermissions.User_RoleDetail_Edit, 1811);

            PermissionOrderDictionary.Add(AppPermissions.Master_Application_DLMaster_Add, 1001);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_DLMaster_Edit, 1002);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_DLMaster_Delete, 1003);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_DLMaster_List, 1004);

            PermissionOrderDictionary.Add(AppPermissions.Master_Application_OtherEntitlment_Add, 1005);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_OtherEntitlment_Edit, 1006);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_OtherEntitlment_Delete, 1007);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_OtherEntitlment_List, 1008);

            PermissionOrderDictionary.Add(AppPermissions.Master_Application_ExceptionApprover_Add, 1009);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_ExceptionApprover_Edit, 1010);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_ExceptionApprover_Delete, 1011);
            PermissionOrderDictionary.Add(AppPermissions.Master_Application_ExceptionApprover_List, 1012);

            PermissionOrderDictionary.Add(AppPermissions.Master_Infra_InfraDBType_Add, 1013);
            PermissionOrderDictionary.Add(AppPermissions.Master_Infra_InfraDBType_Edit, 1014);
            PermissionOrderDictionary.Add(AppPermissions.Master_Infra_InfraDBType_Delete, 1015);
            PermissionOrderDictionary.Add(AppPermissions.Master_Infra_InfraDBType_List, 1016);

        }
    }

}
