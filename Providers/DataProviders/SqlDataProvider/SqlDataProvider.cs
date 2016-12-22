/*
' Copyright (c) 2016 Courtney Popielarz
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Website.DesktopModules.TaskManagerModule.Components;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace Website.DesktopModules.TaskManagerModule.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// 
    /// This concreted data provider class provides the implementation of the abstract methods 
    /// from data dataprovider.cs
    /// 
    /// In most cases you will only modify the Public methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "TaskManagerModule_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        // used to prefect your database objects (stored procedures, tables, views, etc)
        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private static object GetNull(object Field)
        {
            return Null.GetNull(Field, DBNull.Value);
        }


        #endregion

        #region Public Methods

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}


        public override IDataReader GetTasks(int moduleId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "GetTasks",
                                            new SqlParameter("@ModuleId", moduleId));
        }

        public override IDataReader GetTask(int taskId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "GetTask",
                                            new SqlParameter("@TaskId", taskId));
        }

        public override void DeleteTask(int taskId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, NamePrefix + "DeleteTask", new SqlParameter("@TaskId",taskId));
        }

        public override void DeleteTasks(int moduleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, NamePrefix + "DeleteTasks", new SqlParameter("@ModuleId", moduleId));
        }

        public override int AddTask(Task t)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString,CommandType.StoredProcedure,NamePrefix+"AddTask"
                , new SqlParameter("@TaskName",t.TaskName)
                , new SqlParameter("@TaskDescription", t.TaskDescription)
                , new SqlParameter("@AssignedUserId", t.AssignedUserId)
                , new SqlParameter("@ModuleId", t.ModuleId)
                , new SqlParameter("@TargetCompletionDate", t.TargetCompletionDate)
                , new SqlParameter("@CompletedOnDate", t.CompletedOnDate)
                , new SqlParameter("@CreatedByUserId", t.CreatedByUserId)
                ));
        }

        public override void UpdateTask(Task t)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString,CommandType.StoredProcedure,NamePrefix+"UpdateTask"
                , new SqlParameter("@TaskId", t.TaskId)
                , new SqlParameter("@TaskName", t.TaskName)
                , new SqlParameter("@TaskDescription", t.TaskDescription)
                , new SqlParameter("@AssignedUserId", t.AssignedUserId)
                , new SqlParameter("@ModuleId", t.ModuleId)
                , new SqlParameter("@TargetCompletionDate", t.TargetCompletionDate)
                , new SqlParameter("@CompletedOnDate", t.CompletedOnDate)
                , new SqlParameter("@ContentItemId",t.ContentItemId)
                , new SqlParameter("@LastModifiedByUserId", t.LastModifiedByUserId)
                );
        }

        #endregion

    }

}