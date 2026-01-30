using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ARMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Activity = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DBTypeMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DBTypeName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBTypeMaster", x => x.Id);
                    table.UniqueConstraint("AK_DBTypeName", x => x.DBTypeName);
                });

            migrationBuilder.CreateTable(
                name: "ErrorModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Timestamp = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LogLevel = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Exception = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLogModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Timestamp = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LogLevel = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Message = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Exception = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AccountNumber = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    Department = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Phone = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginMaster", x => x.Id);
                    table.UniqueConstraint("AK_LogNumber", x => x.AccountNumber);
                    table.UniqueConstraint("AK_Logname", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Order = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Activated = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AccountNumber = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    Department = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    ProfilePicture = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Phone = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LastSignInDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DateFormat = table.Column<string>(type: "NVARCHAR2(50)", nullable: true, defaultValue: "MM/DD/YYYY"),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.UniqueConstraint("AK_AccountNumber", x => x.AccountNumber);
                    table.UniqueConstraint("AK_Username", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ActivityId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IpAddress = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ActivityModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PermissionId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    RoleId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityModel",
                columns: new[] { "Id", "Activity", "CreatedBy", "CreatedDate", "Message", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, "DBType/Get", "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Getting List of DBType", "System", null },
                    { 2L, "DBType/Insert", "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insert Data in DBType", "System", null },
                    { 3L, "DBType/Edit", "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Edit Data in DBType", "System", null },
                    { 4L, "DBType/Delete", "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delete Data in DBType", "System", null },
                    { 5L, "DBType/Search", "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Search Data in DBType", "System", null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Name", "Order", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", 1801, "System", null },
                    { 2L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.RequestList", 1802, "System", null },
                    { 3L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.RequestDetail", 1803, "System", null },
                    { 4L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.RequestDetail.Edit", 1804, "System", null },
                    { 5L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.UserList", 1805, "System", null },
                    { 6L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.UserDetail", 1806, "System", null },
                    { 7L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.UserDetail.Add", 1807, "System", null },
                    { 8L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.UserDetail.Edit", 1808, "System", null },
                    { 9L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.RoleList", 1809, "System", null },
                    { 10L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.RoleDetail", 1810, "System", null },
                    { 11L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User.RoleDetail.Edit", 1811, "System", null },
                    { 12L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_DLMaster_Add", 1001, "System", null },
                    { 13L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_DLMaster_Edit", 1002, "System", null },
                    { 14L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_DLMaster_Delete", 1003, "System", null },
                    { 15L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_DLMaster_List", 1004, "System", null },
                    { 16L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_OtherEntitlment_Add", 1005, "System", null },
                    { 17L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_OtherEntitlment_Edit", 1006, "System", null },
                    { 18L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_OtherEntitlment_Delete", 1007, "System", null },
                    { 19L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_OtherEntitlment_List", 1008, "System", null },
                    { 20L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_ExceptionApprover_Add", 1009, "System", null },
                    { 21L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_ExceptionApprover_Edit", 1010, "System", null },
                    { 22L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_ExceptionApprover_Delete", 1011, "System", null },
                    { 23L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Application_ExceptionApprover_List", 1012, "System", null },
                    { 24L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Infra_InfraDBType_Add", 1013, "System", null },
                    { 25L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Infra_InfraDBType_Edit", 1014, "System", null },
                    { 26L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Infra_InfraDBType_Delete", 1015, "System", null },
                    { 27L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master_Infra_InfraDBType_List", 1016, "System", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Activated", "CreatedBy", "CreatedDate", "Description", "Name", "Status", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, false, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "System Admin", 1, "System", null },
                    { 2L, false, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CSR", 1, "System", null },
                    { 3L, false, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Registered User", 1, "System", null },
                    { 4L, false, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Guest", 1, "System", null },
                    { 5L, false, "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pending", 1, "System", null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 3L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 4L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 5L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 6L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 7L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 8L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 9L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 10L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 11L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 12L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 13L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 14L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 15L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 16L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 17L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 18L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 19L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 20L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 21L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 22L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 23L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 24L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 25L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 26L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 27L, 1L, "System", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityId",
                table: "ActivityLogModel",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogModel");

            migrationBuilder.DropTable(
                name: "DBTypeMaster");

            migrationBuilder.DropTable(
                name: "ErrorModel");

            migrationBuilder.DropTable(
                name: "ExceptionLogModel");

            migrationBuilder.DropTable(
                name: "LoginMaster");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ActivityModel");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
