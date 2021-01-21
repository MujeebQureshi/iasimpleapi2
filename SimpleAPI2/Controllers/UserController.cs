using SimpleAPI2.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace SimpleAPI2.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpPost]
        public IHttpActionResult GetUserAssignedMenus(Dictionary<string,object> user)
        {
            try
            {
                ValidateExistingUser(user);
                //moves to next step if model is valid
                DBAccess.Authentication.User dbUser = new DBAccess.Authentication.User();
                bool hasData = false;
                var response = dbUser.GetUserMenus(user[DBField.USER_ID].ToString(), out hasData);
                return ApiResponse(response, hasData);
            }
            catch(Exception ex)
            {
                return ApiResponse(ex);
            }
            
        }

        [HttpPost]
        public IHttpActionResult AddNewUser(Dictionary<string, object> user)
        {
            try
            {
                ValidateNewUser(user);
                //moves to next step if model is valid 
                var _userID = RequestContext.Principal.Identity.Name;
                user.Add(DBField.LAST_USERID, _userID);
                DBAccess.Authentication.User dbUser = new DBAccess.Authentication.User();
                bool hasData = false;
                var response = dbUser.InsertUser(user, out hasData);
                return ApiResponse(response, hasData);
            }
            catch(Exception ex)
            {
                return ApiResponse(ex);
            }
            
        }

        [HttpPost]
        public IHttpActionResult UpdateExistingUser(Dictionary<string, object> user)
        {
            try
            {
                ValidateExistingUser(user);
                //moves to next step if model is valid
                var _userID = RequestContext.Principal.Identity.Name;
                user.Add(DBField.LAST_USERID, _userID);
                DBAccess.Authentication.User dbUser = new DBAccess.Authentication.User();
                bool hasData = false;
                var response = dbUser.UpdateUser(user, out hasData);
                return ApiResponse(response, hasData);
            }
            catch(Exception ex)
            {
                return ApiResponse(ex);
            }
        }

        #region Model Validations
        private object ValidateNewUser(Dictionary<string, object> user)
        {
            if(!(user.ContainsKey(DBField.USER_ID) && !string.IsNullOrEmpty(user[DBField.USER_ID].ToString())))
            {
                throw new Exception(ExceptionMessage.EXP_USERID_MISSING);
            }

            if (!(user.ContainsKey(DBField.PASSWORD) && !string.IsNullOrEmpty(user[DBField.PASSWORD].ToString())))
            {
                throw new Exception(ExceptionMessage.EXP_PASS_MISSING);
            }

            if (!(user.ContainsKey(DBField.USERNAME) && !string.IsNullOrEmpty(user[DBField.USERNAME].ToString())))
            {
                throw new Exception(ExceptionMessage.EXP_USERNAME_MISSING);
            }
            
            if (!(user.ContainsKey(DBField.ROLE_ID) && !string.IsNullOrEmpty(user[DBField.ROLE_ID].ToString())))
            {
                throw new Exception(ExceptionMessage.EXP_ROLEID_MISSING);
            }

            if (!(user.ContainsKey(DBField.ASSIGN_LOCATION) && !string.IsNullOrEmpty(user[DBField.ASSIGN_LOCATION].ToString())))
            {
                throw new Exception(ExceptionMessage.EXP_ASSIGNLOC_MISSING);
            }

            return true;
        }
        private object ValidateExistingUser(Dictionary<string, object> user)
        {
            if (!(user.ContainsKey(DBField.USER_ID) && !string.IsNullOrEmpty(user[DBField.USER_ID].ToString())))
            {
                throw new Exception(ExceptionMessage.EXP_USERID_MISSING);
            }

            if (user.ContainsKey(DBField.PASSWORD) && string.IsNullOrEmpty(user[DBField.PASSWORD].ToString()))
            {
                throw new Exception(ExceptionMessage.EXP_PASS_NOTEMPTY);
            }

            return true;
        }
        #endregion
    }
}
