
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI2.Utility.Enums
{
    public enum enAppEvents
    {
        ValidateUserCredentials,
        GetUserMenus,
        CreateNewUser,
        UpdateUser,
        DeleteUser,
        GenericGet,
        GenericInsert,
        GenericUpdate,
        GenericDelete
    }

    public enum enActive
    {
        [Display(Description = "Y")]
        Yes,
        [Display(Description = "N")]
        No,
    }
}