using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace Test.WebSession
{
    static class SessDataAccess
    {
        public enum StoreProvider
        {
            Oracle,
            MicrosoftSQLServer
        }

        public static StoreProvider DataProviderType;
        public static string ConnectionString;
        public static SqlConnection connSQLClient;
        public static OracleConnection connOracle;

        //Data method to initialize connection

        private static void init()
        {
            cleanup(DataProviderType);

            switch (DataProviderType)
            {
                case StoreProvider.MicrosoftSQLServer:
                    connSQLClient = new SqlConnection(ConnectionString); 
                    connSQLClient.Open();
                    break;

                case StoreProvider.Oracle:
                    connOracle = new OracleConnection(ConnectionString); 
                    connOracle.Open();
                    break;
            }
        }


        //Data method to dispose and clear memory used for connection

        private static void cleanup(StoreProvider providerType)
        {
            try
            {

                if (providerType == StoreProvider.MicrosoftSQLServer && connSQLClient != null)
                    connSQLClient.Dispose();
                if (providerType == StoreProvider.Oracle && connOracle != null)
                    connOracle.Dispose();

            }
            catch (Exception ex)
            {}
        }

        public static int ExecuteNQ(string Query)
        {
            init();

            int iResult = 0;

            switch (DataProviderType)
            {
                case StoreProvider.Oracle:

                    OracleCommand cmdOrcl = new OracleCommand(Query, connOracle);
                    iResult = cmdOrcl.ExecuteNonQuery();

                    break;

                case StoreProvider.MicrosoftSQLServer:

                    SqlCommand cmdSQL = new SqlCommand(Query, connSQLClient);
                    iResult = cmdSQL.ExecuteNonQuery();

                    break;
            }

            return iResult;
        }

        public static List<DataRow> GetData(string Query)
        {
            List<DataRow> rows = new List<DataRow>();

            init();
            DataSet tmpDS = new DataSet();

            switch (DataProviderType)
            {
                case StoreProvider.MicrosoftSQLServer:

                    SqlDataAdapter daSQL = new SqlDataAdapter(Query, connSQLClient);
                    daSQL.Fill(tmpDS);

                    break;

                case StoreProvider.Oracle:

                    OracleDataAdapter daORCL = new OracleDataAdapter(Query, connOracle);
                    daORCL.Fill(tmpDS);

                    break;
            }

            if (tmpDS.Tables.Count > 0)
            {
                foreach (DataRow dr in tmpDS.Tables[0].Rows)
                    rows.Add(dr);
            }

            return rows;
        }

        public static Int32 GetInt32(object val)
        {
            try
            {
                return Int32.Parse(val.ToString());
            }
            catch { }

            return 0;
        }

        public static String GetString(object val)
        {
            try
            {
                return val.ToString();
            }
            catch { }

            return "";
        }

        public static DateTime GetDateTime(object val)
        {
            try
            {
                return DateTime.Parse(val.ToString());
            }
            catch { }

            return DateTime.MinValue;
        }
    }
}
