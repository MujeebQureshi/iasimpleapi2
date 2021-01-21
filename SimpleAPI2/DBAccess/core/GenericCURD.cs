using SimpleAPI2.Models.Constants;
using SimpleAPI2.Utility;
using SimpleAPI2.Utility.Enums;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleAPI2.DBAccess.core
{
    public class GenericCURD
    {
        public object Get(string tablename, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var response = db.Query(tablename).Get();
                hasData = (response != null && response.Count() > 0) ? true : false;
                return response;
            }
            catch (Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.GenericGet.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericGet);

                throw ex;
            }
        }
        public object Get(string tablename, Dictionary<string, object> whereclause, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var query = db.Query(tablename); //new Query(tablename);
                foreach (var key in whereclause.Keys)
                {
                    query = query.Where(key, whereclause[key]);
                }
                
                var response = query.Get();
                hasData = (response != null && response.Count() > 0) ? true : false;
                return response;
            }
            catch (Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.GenericGet.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericGet);

                throw ex;
            }
        }
        public object Insert(Dictionary<string, object> model, string tablename, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var response = db.Query(tablename).Insert(model);
                if (response > 0)
                {
                    hasData = true;
                    return true;
                }

                throw new Exception(ExceptionMessage.EXP_NO_ROWS_INSERTED);
            }
            catch (Exception ex)
            {
                EventLogger.WriteEventViewerLog(
                    enAppEvents.GenericInsert.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericInsert);

                throw ex;
            }
        }
        public object Update(Dictionary<string, object> model, string tablename, string key, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var response = db.Query(tablename).Where(key, model[key]).Update(model);
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
                    enAppEvents.GenericUpdate.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericUpdate);

                throw ex;
            }
        }
        public object Update(Dictionary<string, object> model, string tablename, string [] keys, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var query = db.Query(tablename);
                foreach(var key in keys)
                {
                    query = query.Where(key, model[key]);
                }

                var response = query.Update(model);
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
                    enAppEvents.GenericUpdate.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericUpdate);

                throw ex;
            }
        }
        public object Delete(string Id, string tablename, string key, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var response = db.Query(tablename).Where(key, Id).Delete();
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
                    enAppEvents.GenericDelete.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericDelete);

                throw ex;
            }
        }
        public object Delete(string tablename, Dictionary<string, object> whereclause, out bool hasData)
        {
            try
            {
                var db = DBConnection.GetDBConnection();
                var query = db.Query(tablename);
                foreach (var key in whereclause.Keys)
                {
                    query = query.Where(key, whereclause[key]);
                }

                var response = query.Delete();
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
                    enAppEvents.GenericDelete.ToString(),
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    (int)enAppEvents.GenericDelete);

                throw ex;
            }
        }
    }
}