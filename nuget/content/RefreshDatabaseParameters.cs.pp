// ==============================================================================
// 
// Fervent Coder Copyright © 2012 - Present - Released under the Apache 2.0 License
// 
// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
//
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
// ==============================================================================

namespace $rootnamespace$
{

//Please contribute back to https://github.com/chucknorris/roundhouse-entityframework
//There is a sample available in the samples folder.

//1. These files should be in a console project with x86 (and not Client Profile).
//2. Add a reference to the project where you have your migrations.
//3. Look through these options and set them appropriately.

    /// <summary>
    /// https://github.com/chucknorris/roundhouse/wiki/RoundhouserefreshdatabaseEF
    /// This should be added to a console application that is set to x86.
    /// </summary>
    /// <remarks>
    /// Make sure it is not using client profile or the build will fail
    /// </remarks>
    public static class RefreshDatabaseParameters
    {
        /// <summary>
        /// Should we drop the database prior to running? You want this to continue making changes to the same script
        /// </summary>
        public static bool DropDatabaseFirst = true;

        /// <summary>
        /// Should we restore the database from a backup prior to running?
        /// You want to restore if you have a production backup that is small enough. 
        /// Otherwise you get into a bit more advanced scenario that this package doesn't cover well
        /// </summary>
        public static bool RestoreDatabase = false;
        
        /// <summary>
        /// This is the path to your scripts folder where Up/Views/Functions/Sprocs are the next folder below. This is a relative path from bin\Debug. The three sets of parent folders already here should get it out of your project folder so you can traverse into the database project folder. 
        /// </summary>
        public static string PathToSqlScripts = @"..\..\..\__NAME__.Database\__NAME__";
        
        /// <summary>
        /// The path to your source control repository. Used only for information sake.
        /// </summary>
        public static string RepositoryPath = "https://github.com/__NAME__/";
        

        /// <summary>
        /// This is the path to the restore file, likely on the network so everyone can get to it
        /// </summary>
        public static string PathToRestore = @"\\nowhere\to\befound.bak";

        /// <summary>
        /// The is the custom options for the restore, like moving logical files to the correct location
        /// </summary>
        public static string RestoreCustomOptions = @"MOVE '__NAME__' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\{{DatabaseName}}.mdf', MOVE '__NAME___log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\{{DatabaseName}}_log.ldf'";

        /// <summary>
        /// Add a reference to the migrations assembly. After adding a reference, the file will be in the build directory, so you can just add the name of the dll here.
        /// </summary>
        public static string MigrationsAssemblyPath = @".\__NAME__.dll";
        
        /// <summary>
        /// The database information
        /// </summary>
        public static class Database
        {
            /// <summary>
            /// Name of your database - hopefully on your local default instance
            /// </summary>
            public static string Name = "__NAME__";
            /// <summary>
            /// This is the server, it is highly recommended that this is either . or .\SQLExpress
            /// </summary>
            public static string Server = @".";

            /// <summary>
            /// The user name for the connection string - leave blank for SSPI=true
            /// </summary>
            public static string UserName = "";

            /// <summary>
            /// The user password for the connection string. If the UserName is blank this will not be used.
            /// </summary>
            public static string UserPassword = "";

            /// <summary>
            /// Gets the connection string.
            /// </summary>
            /// <returns>The connection string</returns>
            public static string GetConnectionString()
            {
                return string.Format("Data Source={0};Initial Catalog={1};{2}", 
                    Server, 
                    Name, 
                    UserName == string.Empty ? "Integrated Security=SSPI;" : string.Format("User Id={0};Password={1}", UserName, UserPassword));
            }
        }

    }

    
}