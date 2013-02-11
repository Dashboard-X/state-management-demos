using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Web;
using System.Data;
using System.Collections;
//using System.Data.Odbc;
using System.Web.SessionState;
using System.Diagnostics;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Specialized;

namespace Test.WebSession
{
    class CustomSessionStoreProvider : SessionStateStoreProviderBase
    {
       

        private SessionStateSection pConfig = null;
        private SessDataAccess.StoreProvider sessionStoreType = SessDataAccess.StoreProvider.MicrosoftSQLServer;    //defaults to MS SQL Server
        private string connectionString;
        private string pApplicationName;

        private string eventSource = "WebSession.CustomSessionStoreProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please contact your administrator.";

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //
        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        public SessDataAccess.StoreProvider SessionStoreProvider
        {
            get { return sessionStoreType; }
            set { sessionStoreType = value; }
        }

        
        // The ApplicationName property is used to differentiate sessions
        // in the data source by application.
        //

        public string ApplicationName
        {
            get { return pApplicationName; }
        }


        public override void Initialize(string name, NameValueCollection config)
        {
            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "CustomSessionStateStore";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Session State Store provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            //
            // Initialize the ApplicationName property.
            //

            pApplicationName =
              HttpContext.Current.Request.ApplicationPath;


            //
            // Get <sessionState> configuration element.
            //

            pConfig = (SessionStateSection)(ConfigurationManager.GetSection("system.web/sessionState"));


            connectionString = ConfigurationManager.AppSettings["DBConnectionstring"];
            SessDataAccess.ConnectionString = connectionString;

            string tmpDBClientType = ConfigurationManager.AppSettings["DBProvider"];
            switch (tmpDBClientType.ToUpper())
            {
                case "SQLCLIENT":
                    SessDataAccess.DataProviderType = SessDataAccess.StoreProvider.MicrosoftSQLServer;
                    break;

                case "ORACLE":
                    SessDataAccess.DataProviderType = SessDataAccess.StoreProvider.Oracle;
                    break;
            }

            

            //
            // Initialize WriteExceptionsToEventLog
            //

            pWriteExceptionsToEventLog = false;

            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                    pWriteExceptionsToEventLog = true;
            }
        }


        //
        // SessionStateStoreProviderBase members
        //

        public override void Dispose()
        {
        }


        //
        // SessionStateProviderBase.SetItemExpireCallback
        //

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }


        //
        // SessionStateProviderBase.SetAndReleaseItemExclusive
        //

        public override void SetAndReleaseItemExclusive(HttpContext context,
          string id,
          SessionStateStoreData item,
          object lockId,
          bool newItem)
        {
            // Serialize the SessionStateItemCollection as a string.
            string sessItems = Serialize((SessionStateItemCollection)item.Items);


            string tmpQuery = "";
            string tmpDeleteQuery = "";

            if (newItem)
            {
                // query to clear an existing expired session if it exists.
                tmpDeleteQuery = @"DELETE FROM Sessions 
WHERE SessionId = '@session_id@' AND ApplicationName = '@application_name@' AND Expires < @expires@";

                tmpDeleteQuery = tmpDeleteQuery.Replace("@session_id@", id);
                tmpDeleteQuery = tmpDeleteQuery.Replace("@application_name@", ApplicationName);
                tmpDeleteQuery = tmpDeleteQuery.Replace("@expires@", convDate_forQuery(DateTime.Now));

                // query to insert the new session item.
                tmpQuery = @"INSERT INTO Sessions 
(SessionId, ApplicationName, Created, Expires, 
  LockDate, LockId, Timeout, Locked, SessionItems, Flags) 
  Values('@session_id@', '@app_name@', @created@, @expires@, 
  @lock_date@, '@lock_id@', '@timeout@', '@locked@', '@session_items@', '@flags@')";

                tmpQuery = tmpQuery.Replace("@session_id@", id);
                tmpQuery = tmpQuery.Replace("@app_name@",ApplicationName);
                tmpQuery = tmpQuery.Replace("@created@", convDate_forQuery(DateTime.Now));
                tmpQuery = tmpQuery.Replace("@expires@", convDate_forQuery(DateTime.Now.AddMinutes((double)item.Timeout)));
                tmpQuery = tmpQuery.Replace("@lock_date@", convDate_forQuery(DateTime.Now));
                tmpQuery = tmpQuery.Replace("@lock_id@", "0");
                tmpQuery = tmpQuery.Replace("@timeout@", item.Timeout.ToString());
                tmpQuery = tmpQuery.Replace("@locked@", "0");
                tmpQuery = tmpQuery.Replace("@session_items@", sessItems);
                tmpQuery = tmpQuery.Replace("@flags@", "0");

            }
            else
            {
                // query to update the existing session item.
                tmpQuery = @"UPDATE Sessions SET Expires = @expires@, SessionItems = '@sess_items@', Locked = '@locked@' 
WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@' AND LockId = '@lock_id@'";

                tmpQuery = tmpQuery.Replace("@expires@", convDate_forQuery(DateTime.Now.AddMinutes((Double)item.Timeout)));
                tmpQuery = tmpQuery.Replace("@sess_items@", sessItems);
                tmpQuery = tmpQuery.Replace("@locked@", "0");
                tmpQuery = tmpQuery.Replace("@sess_id@", id);
                tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);
                tmpQuery = tmpQuery.Replace("@lock_id@", lockId.ToString());
            }

            try
            {
                if (tmpDeleteQuery != "")
                    SessDataAccess.ExecuteNQ(tmpDeleteQuery);
                if (tmpQuery != "")
                    SessDataAccess.ExecuteNQ(tmpQuery);
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "SetAndReleaseItemExclusive");
                    throw (e);
                }
                else
                    throw e;
            }
        }

        
        //
        // SessionStateProviderBase.GetItem
        //

        public override SessionStateStoreData GetItem(HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(false, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }


        //
        // SessionStateProviderBase.GetItemExclusive
        //

        public override SessionStateStoreData GetItemExclusive(HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(true, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }


        //
        // GetSessionStoreItem is called by both the GetItem and 
        // GetItemExclusive methods. GetSessionStoreItem retrieves the 
        // session data from the data source. If the lockRecord parameter
        // is true (in the case of GetItemExclusive), then GetSessionStoreItem
        // locks the record and sets a new LockId and LockDate.
        //

        private SessionStateStoreData GetSessionStoreItem(bool lockRecord,
          HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            // Initial values for return value and out parameters.
            SessionStateStoreData item = null;
            lockAge = TimeSpan.Zero;
            lockId = null;
            locked = false;
            actionFlags = 0;

            
            // DateTime to check if current session item is expired.
            DateTime expires;
            // String to hold serialized SessionStateItemCollection.
            string serializedItems = "";
            // True if a record is found in the database.
            bool foundRecord = false;
            // True if the returned session item is expired and needs to be deleted.
            bool deleteData = false;
            // Timeout value from the data store.
            int timeout = 0;

            try
            {
                string tmpQuery = "";

                // lockRecord is true when called from GetItemExclusive and
                // false when called from GetItem.
                // Obtain a lock if possible. Ignore the record if it is expired.
                if (lockRecord)
                {
                    tmpQuery = @"UPDATE Sessions SET Locked = '@locked@', LockDate = @lock_date@ 
WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@' AND Expires > @expires@";

                    tmpQuery = tmpQuery.Replace("@locked@", "1");
                    tmpQuery = tmpQuery.Replace("@lock_date@", convDate_forQuery(DateTime.Now));
                    tmpQuery = tmpQuery.Replace("@sess_id@", id);
                    tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);
                    tmpQuery = tmpQuery.Replace("@locked@", "0");
                    tmpQuery = tmpQuery.Replace("@expires@", convDate_forQuery(DateTime.Now));


                    if (SessDataAccess.ExecuteNQ(tmpQuery) == 0)
                        // No record was updated because the record was locked or not found.
                        locked = true;
                    else
                        // The record was updated.

                        locked = false;
                }

                // Retrieve the current session item information.
                tmpQuery = @"SELECT Expires, SessionItems, LockId, LockDate, Flags, Timeout 
FROM Sessions WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@'";

                tmpQuery = tmpQuery.Replace("@sess_id@", id);
                tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);

                // Retrieve session item data from the data source.
                List<DataRow> rows = SessDataAccess.GetData(tmpQuery);
                foreach (DataRow tmpDR in rows)
                {
                    expires = SessDataAccess.GetDateTime(tmpDR["Expires"]);

                    if (expires < DateTime.Now)
                    {
                        // The record was expired. Mark it as not locked.
                        locked = false;
                        // The session was expired. Mark the data for deletion.
                        deleteData = true;
                    }
                    else
                        foundRecord = true;

                    serializedItems = SessDataAccess.GetString(tmpDR["SessionItems"]);
                    lockId = SessDataAccess.GetInt32(tmpDR["LockId"]);
                    lockAge = DateTime.Now.Subtract(SessDataAccess.GetDateTime(tmpDR["LockDate"]));
                    actionFlags = (SessionStateActions)SessDataAccess.GetInt32(tmpDR["Flags"]);
                    timeout = SessDataAccess.GetInt32(tmpDR["Timeout"]);
                    
                }
                   

                // If the returned session item is expired, 
                // delete the record from the data source.
                if (deleteData)
                {
                    tmpQuery = @"DELETE FROM Sessions WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@'";
                    tmpQuery = tmpQuery.Replace("@sess_id@", id);
                    tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);

                    SessDataAccess.ExecuteNQ(tmpQuery);
                }

                // The record was not found. Ensure that locked is false.
                if (!foundRecord)
                    locked = false;

                // If the record was found and you obtained a lock, then set 
                // the lockId, clear the actionFlags,
                // and create the SessionStateStoreItem to return.
                if (foundRecord && !locked)
                {
                    lockId = (int)lockId + 1;

                    tmpQuery = @"UPDATE Sessions SET LockId = '@lock_id@', Flags = '@flags@' 
WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@'";
                    
                    tmpQuery = tmpQuery.Replace("@lock_id@", lockId.ToString());
                    tmpQuery = tmpQuery.Replace("@sess_id@", id);
                    tmpQuery = tmpQuery.Replace("@flags@", "0");
                    tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);

                    SessDataAccess.ExecuteNQ(tmpQuery);

                    // If the actionFlags parameter is not InitializeItem, 
                    // deserialize the stored SessionStateItemCollection.
                    if (actionFlags == SessionStateActions.InitializeItem)
                        item = CreateNewStoreData(context, pConfig.Timeout.Minutes);
                    else
                        item = Deserialize(context, serializedItems, timeout);
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetSessionStoreItem");
                    throw (e);
                }
                else
                    throw e;
            }

            return item;
        }




        //
        // Serialize is called by the SetAndReleaseItemExclusive method to 
        // convert the SessionStateItemCollection into a Base64 string to    
        // be stored in an Access Memo field.
        //

        private string Serialize(SessionStateItemCollection items)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);

            if (items != null)
                items.Serialize(writer);

            writer.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        //
        // DeSerialize is called by the GetSessionStoreItem method to 
        // convert the Base64 string stored in the Access Memo field to a 
        // SessionStateItemCollection.
        //

        private SessionStateStoreData Deserialize(HttpContext context,
          string serializedItems, int timeout)
        {
            MemoryStream ms =
              new MemoryStream(Convert.FromBase64String(serializedItems));

            SessionStateItemCollection sessionItems =
              new SessionStateItemCollection();

            if (ms.Length > 0)
            {
                BinaryReader reader = new BinaryReader(ms);
                sessionItems = SessionStateItemCollection.Deserialize(reader);
            }

            return new SessionStateStoreData(sessionItems,
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }

        //
        // SessionStateProviderBase.ReleaseItemExclusive
        //

        public override void ReleaseItemExclusive(HttpContext context,
          string id,
          object lockId)
        {
            string tmpQuery = @"UPDATE Sessions SET Locked = 0, Expires = @expires@ WHERE 
SessionId = '@sess_id@' AND ApplicationName = '@app_name@' AND LockId = '@lock_id@'";

            tmpQuery = tmpQuery.Replace("@expires@", convDate_forQuery(DateTime.Now.AddMinutes((double)(pConfig.Timeout.Minutes))));
            tmpQuery = tmpQuery.Replace("@sess_id@", id);
            tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);
            tmpQuery = tmpQuery.Replace("@lock_id@", lockId.ToString());

            try
            {
                SessDataAccess.ExecuteNQ(tmpQuery);
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw (e);
                }
                else
                    throw e;
            }
        }


        //
        // SessionStateProviderBase.RemoveItem
        //

        public override void RemoveItem(HttpContext context,
          string id,
          object lockId,
          SessionStateStoreData item)
        {
            string tmpQuery = @"DELETE * FROM Sessions 
WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@' AND LockId = '@lock_id@'";

            tmpQuery = tmpQuery.Replace("@sess_id@", id);
            tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);
            tmpQuery = tmpQuery.Replace("@lock_id@", lockId.ToString());

            try
            {
                SessDataAccess.ExecuteNQ(tmpQuery);
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "RemoveItem");
                    throw (e);
                }
                else
                    throw e;
            }
        }



        //
        // SessionStateProviderBase.CreateUninitializedItem
        //

        public override void CreateUninitializedItem(HttpContext context,
          string id,
          int timeout)
        {
            string tmpQuery = @"INSERT INTO Sessions 
(SessionId, ApplicationName, Created, Expires, 
LockDate, LockId, Timeout, Locked, SessionItems, Flags) 
Values('@sess_id@', '@app_name@', @created@, @expires@, 
@lock_date@, '@lock_id@', '@timeout@', '@locked@', '@sess_items@', '@flags@')";

            tmpQuery = tmpQuery.Replace("@sess_id@", id);
            tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);
            tmpQuery = tmpQuery.Replace("@created@", convDate_forQuery(DateTime.Now));
            tmpQuery = tmpQuery.Replace("@expires@", convDate_forQuery(DateTime.Now.AddMinutes((Double)timeout)));
            tmpQuery = tmpQuery.Replace("@lock_date@", convDate_forQuery(DateTime.Now));
            tmpQuery = tmpQuery.Replace("@lock_id@", "0");
            tmpQuery = tmpQuery.Replace("@timeout@", timeout.ToString());
            tmpQuery = tmpQuery.Replace("@locked@", "0");
            tmpQuery = tmpQuery.Replace("@sess_items@", "");
            tmpQuery = tmpQuery.Replace("@flags@", "1");

            try
            {
                SessDataAccess.ExecuteNQ(tmpQuery);
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateUninitializedItem");
                    throw (e);
                }
                else
                    throw e;
            }
        }


        //
        // SessionStateProviderBase.CreateNewStoreData
        //

        public override SessionStateStoreData CreateNewStoreData(
          HttpContext context,
          int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection(),
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }



        //
        // SessionStateProviderBase.ResetItemTimeout
        //

        public override void ResetItemTimeout(HttpContext context,
                                              string id)
        {
            string tmpQuery = @"UPDATE Sessions SET Expires = @expires@ 
WHERE SessionId = '@sess_id@' AND ApplicationName = '@app_name@'";

            tmpQuery = tmpQuery.Replace("@sess_id@", id);
            tmpQuery = tmpQuery.Replace("@app_name@", ApplicationName);
            tmpQuery = tmpQuery.Replace("@expires@", convDate_forQuery(DateTime.Now.AddMinutes((double)(pConfig.Timeout.Minutes))));

            try
            {
                SessDataAccess.ExecuteNQ(tmpQuery);
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetItemTimeout");
                    throw (e);
                }
                else
                    throw e;
            }
        }


        //
        // SessionStateProviderBase.InitializeRequest
        //

        public override void InitializeRequest(HttpContext context)
        {
        }


        //
        // SessionStateProviderBase.EndRequest
        //

        public override void EndRequest(HttpContext context)
        {
        }


        //
        // WriteToEventLog
        // This is a helper function that writes exception detail to the 
        // event log. Exceptions are written to the event log as a security
        // measure to ensure private database details are not returned to 
        // browser. If a method does not return a status or Boolean
        // indicating the action succeeded or failed, the caller also 
        // throws a generic exception.
        //

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message =
              "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }

        private string convDate_forQuery(DateTime dtValue)
        {
            string tmpValue = "";

            switch (SessDataAccess.DataProviderType)
            {
                case SessDataAccess.StoreProvider.MicrosoftSQLServer:
                    tmpValue = string.Format("'{0}'", dtValue.ToString("dd/MMM/yyyy H:mm:ss"));
                    break;

                case SessDataAccess.StoreProvider.Oracle:

                    //to_date('05-AUG-2010 09:59:01', 'DD-MM-YYYY HH:MI:SS AM')
                    tmpValue = "to_date('@dt@', 'DD-MM-YYYY HH24:MI:SS')";

                    //05-AUG-2010 09:59:01
                    tmpValue = tmpValue.Replace("@dt@", dtValue.ToString("dd-MMM-yyyy H:mm:ss"));

                    break;
            }


            return tmpValue;
        }
    }
}
