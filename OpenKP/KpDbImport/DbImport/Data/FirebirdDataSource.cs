﻿/*
 * Copyright 2019 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : KpDBImport
 * Summary  : Implements a data source for PostgreSQL
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2018
 * Modified : 2019
 */

using FirebirdSql.Data.FirebirdClient;
using Scada.Comm.Devices.DbImport.Configuration;
using System;
using System.Data.Common;

namespace Scada.Comm.Devices.DbImport.Data
{
    /// <summary>
    /// Implements a data source for Firebird.
    /// <para>Реализует источник данных для Firebird.</para>
    /// </summary>
    internal class FirebirdDataSource : DataSource
    {
        /// <summary>
        /// The default port of the database server.
        /// </summary>
        private const int DefaultPort = 3050;


        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected override DbConnection CreateConnection()
        {
            return new FbConnection();
        }

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected override DbCommand CreateCommand()
        {
            return new FbCommand();
        }

        /// <summary>
        /// Adds the command parameter containing the value.
        /// </summary>
        protected override void AddCmdParamWithValue(DbCommand cmd, string paramName, object value)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");
            if (!(cmd is FbCommand))
                throw new ArgumentException("FbCommand is required.", "cmd");

            FbCommand pgSqlCmd = (FbCommand)cmd;
            pgSqlCmd.Parameters.AddWithValue(paramName, value);
        }

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected override void ClearPool()
        {
            FbConnection.ClearAllPools();
        }


        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public override string BuildConnectionString(DbConnSettings connSettings)
        {
            return BuildFbConnectionString(connSettings);
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public static string BuildFbConnectionString(DbConnSettings connSettings)
        {
            if (connSettings == null)
                throw new ArgumentNullException("connSettings");

            if (connSettings.Port == string.Empty || connSettings.Port == null)
                connSettings.Port = DefaultPort.ToString();

            ExtractHostAndPort(connSettings.Server, Convert.ToInt32(connSettings.Port), out string host, out int port);
            return string.Format("Data Source={0};port number={1};Initial Catalog={2};user id={3};password={4};{5}",
                host, port, connSettings.Database, connSettings.User, connSettings.Password, connSettings.OptionalOptions);
        }
    }
}
