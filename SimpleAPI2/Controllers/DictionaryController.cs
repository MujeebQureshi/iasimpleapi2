using SimpleAPI2.DBAccess.core;
using SimpleAPI2.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleAPI2.Controllers
{
    [Authorize]
    public class DictionaryController : BaseController
    {
        #region Roles
        [HttpPost]
        public IHttpActionResult GetAllRoles(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = null;
                if(model != null && model.Keys.Count > 0)
                {
                    response = db.Get(DBTable.ROLES, model, out hasData);
                }
                else
                {
                    response = db.Get(DBTable.ROLES, out hasData);
                }
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        [HttpPost]
        public IHttpActionResult CreateNewRole(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = db.Insert(model, DBTable.ROLES, out hasData);
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        [HttpPost]
        public IHttpActionResult UpdateRole(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = db.Update(model, DBTable.ROLES, DBField.ROLE_ID,out hasData);
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        #endregion

        #region Menus
        [HttpPost]
        public IHttpActionResult GetAllMenus(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = null;
                if (model != null && model.Keys.Count > 0)
                {
                    response = db.Get(DBTable.MENU, model, out hasData);
                }
                else
                {
                    response = db.Get(DBTable.MENU, out hasData);
                }
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        [HttpPost]
        public IHttpActionResult CreateNewMenu(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = db.Insert(model, DBTable.MENU, out hasData);
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        [HttpPost]
        public IHttpActionResult UpdateMenu(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = db.Update(model, DBTable.MENU, DBField.MENU_ID, out hasData);
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        #endregion

        #region MenusRoleRelationship
        [HttpPost]
        public IHttpActionResult GetAllMenusAgainstRoles(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = null;
                if (model != null && model.Keys.Count > 0)
                {
                    response = db.Get(DBTable.MENU_ROLE_RELATIONSHIPS, model, out hasData);
                }
                else
                {
                    response = db.Get(DBTable.MENU, out hasData);
                }
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        [HttpPost]
        public IHttpActionResult AddNewMenuToRole(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = db.Insert(model, DBTable.MENU_ROLE_RELATIONSHIPS, out hasData);
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        [HttpPost]
        public IHttpActionResult RemoveMenuAgainstRole(Dictionary<string, object> model)
        {
            try
            {
                GenericCURD db = new GenericCURD();
                bool hasData = false;
                object response = db.Delete(DBTable.MENU_ROLE_RELATIONSHIPS, model, out hasData);
                return ApiResponse(response, hasData);
            }
            catch (Exception ex)
            {
                return ApiResponse(ex);
            }

        }
        #endregion
    }
}
