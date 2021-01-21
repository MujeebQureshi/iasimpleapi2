using SqlKata.Execution;
using System;
using System.Linq;
using SimpleAPI2.Utility;
using SimpleAPI2.Utility.Enums;
using SimpleAPI2.DBAccess.core;
using SimpleAPI2.Models.Constants;
using System.Collections.Generic;

namespace SimpleAPI2.DBAccess.Authentication
{
    public class User
    {
        public object ValidateUserCredentials(string username, string password)
        {
            try
            {
                password = Helper.Encrypt(password);

                var db = DBConnection.GetDBConnection();
                object response =
                    db.Query(DBTable.USERS)
                        .Where(DBField.USER_ID, username)
                        .Where(DBField.PASSWORD, password)
                        .Where(DBField.ACTIVE,  Helper.GetDescriptionFromEnumValue(enActive.Yes)).Get().First();

                return response; //JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(response));
            }
            catch(Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.ValidateUserCredentials.ToString(), 
                    ex.Message + Environment.NewLine + ex.StackTrace, 
                    (int)enAppEvents.ValidateUserCredentials);

                return null;
            }
        }
        public object GetUserMenus(string userID, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var response =
                    db.Query(DBTable.USERS)
                    .Join(DBTable.ROLES, Helper.InstanceOf(DBTable.USERS, DBField.ROLE_ID), Helper.InstanceOf(DBTable.ROLES, DBField.ROLE_ID))
                    .Join(DBTable.MENU_ROLE_RELATIONSHIPS, Helper.InstanceOf(DBTable.ROLES, DBField.ROLE_ID), Helper.InstanceOf(DBTable.MENU_ROLE_RELATIONSHIPS, DBField.ROLE_ID))
                    .Join(DBTable.MENU, Helper.InstanceOf(DBTable.MENU_ROLE_RELATIONSHIPS, DBField.MENU_ID), Helper.InstanceOf(DBTable.MENU, DBField.MENU_ID))
                    .Where(Helper.InstanceOf(DBTable.MENU, DBField.ACTIVE), Helper.GetDescriptionFromEnumValue(enActive.Yes))
                    .Where(Helper.InstanceOf(DBTable.USERS, DBField.USER_ID), userID)
                    .Select(
                        Helper.InstanceOf(DBTable.USERS, DBField.USER_ID),
                        Helper.InstanceOf(DBTable.USERS, DBField.USERNAME),
                        Helper.InstanceOf(DBTable.MENU, DBField.MENU_ID),
                        Helper.InstanceOf(DBTable.MENU, DBField.MENU_DESCRIPTION),
                        Helper.InstanceOf(DBTable.MENU, DBField.PARENT_MENU),
                        Helper.InstanceOf(DBTable.MENU, DBField.ACTIVE)
                        ).Get();

                hasData = (response != null && response.Count() > 0)? true : false;
                return response; //JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.GetUserMenus.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GetUserMenus);

                throw ex;
            }
        }
        public object InsertUser(Dictionary<string, object> user, out bool hasData)
        {
            try
            {
                //set props
                user.Add(DBField.ACTIVE, Helper.GetDescriptionFromEnumValue(enActive.Yes));
                user.Add(DBField.LAST_DTTM, DateTime.Now);
                user[DBField.PASSWORD] = Helper.Encrypt(user[DBField.PASSWORD].ToString());

                var db = DBConnection.GetDBConnection();
                var response = db.Query(DBTable.USERS).Insert(user);
                if (response > 0)
                {
                    hasData = true;
                    return true;
                }
                
                throw new Exception(ExceptionMessage.EXP_NO_ROWS_INSERTED);
            }
            catch(Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.CreateNewUser.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.CreateNewUser);

                throw ex;
            }
        }
        public object UpdateUser(Dictionary<string, object> user, out bool hasData)
        {
            try
            {
                //set props
                if (user.ContainsKey(DBField.PASSWORD))
                {
                    user[DBField.PASSWORD] = Helper.Encrypt(user[DBField.PASSWORD].ToString());
                }
                user.Add(DBField.LAST_DTTM, DateTime.Now);
                
                var db = DBConnection.GetDBConnection();
                var response = db.Query(DBTable.USERS).Where(DBField.USER_ID, user[DBField.USER_ID]).Update(user);
                if (response > 0)
                {
                    hasData = true;
                    return true;
                }

                throw new Exception(ExceptionMessage.EXP_NO_ROWS_UPDATED);
            }
            catch (Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.UpdateUser.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.UpdateUser);

                throw ex;
            }
        }
        public object DeleteUser(string userID, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var response = db.Query(DBTable.USERS).Where(DBField.USER_ID, userID).Delete();
                if (response > 0)
                {
                    hasData = true;
                    return true;
                }

                throw new Exception(ExceptionMessage.EXP_NO_ROWS_DELETED);
            }
            catch (Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.DeleteUser.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.DeleteUser);

                throw ex;
            }
        }
    }
}