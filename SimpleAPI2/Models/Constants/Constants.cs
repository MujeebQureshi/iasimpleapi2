using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleAPI2.Models.Constants
{
    public struct DBTable
    {
        public static string USERS = "Users";
        public static string MENU_ROLE_RELATIONSHIPS = "MenuRoleRelationships";
        public static string MENU = "Menu";
        public static string ROLES = "Roles";
    }

    public struct DBField
    {
        public static string USER_ID = "UserID";
        public static string PASSWORD = "Password";
        public static string ACTIVE = "Active";
        public static string ROLE_ID = "RoleID";
        public static string ROLE_DESCRIPTION = "RoleDescription";
        public static string MENU_ID = "MenuID";
        public static string USERNAME = "UserName";
        public static string MENU_DESCRIPTION = "MenuDescription";
        public static string PARENT_MENU = "ParentMenu";
        public static string LAST_DTTM = "LastDTTM";
        public static string LAST_USERID = "LastUserID";
        public static string ASSIGN_LOCATION = "AssignLocation";
    }

    public struct VMField
    {
        public static string USER = "User";
    }
    public struct ExceptionMessage
    {
        public static string EXP_USERID_MISSING = "User Id should be present and cannot be null";
        public static string EXP_PASS_MISSING = "Password should be present and cannot be null";
        public static string EXP_PASS_NOTEMPTY = "Password cannot be null or empty";
        public static string EXP_USERNAME_MISSING = "Username should be present and cannot be null";
        public static string EXP_ROLEID_MISSING = "Role should be present and cannot be null";
        public static string EXP_ASSIGNLOC_MISSING = "Assigned Location should be present and cannot be null";
        public static string EXP_NO_ROWS_INSERTED = "No row(s) inserted";
        public static string EXP_INSERT = "Exception while inserting row(s)";
        public static string EXP_NO_ROWS_UPDATED = "No row(s) updated";
        public static string EXP_UPDATE = "Exception while updating row(s)";
        public static string EXP_NO_ROWS_DELETED = "No row(s) deleted";
        public static string EXP_DELETE = "Exception while deleting row(s)";
    }
}