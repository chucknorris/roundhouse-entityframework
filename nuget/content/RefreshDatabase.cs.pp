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
    using System;
    using System.Reflection;
    using roundhouse;

    public partial class RefreshDatabase
     {
        private static void Main(string[] args)
        {
            try
            {
                RunRoundhouse();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        private static void RunRoundhouse()
        {
            var migrationsAssembly = Assembly.LoadFrom(RefreshDatabaseParameters.MigrationsAssemblyPath);

            var migrator = new Migrate().Set(c =>
            {
                c.Logger = new roundhouse.infrastructure.logging.custom.ConsoleLogger();
                c.ConnectionString = RefreshDatabaseParameters.Database.GetConnectionString();
                c.RepositoryPath = RefreshDatabaseParameters.RepositoryPath;
                c.SqlFilesDirectory = RefreshDatabaseParameters.PathToSqlScripts;
                c.Restore = false;
                c.RestoreFromPath = RefreshDatabaseParameters.PathToRestore;
                if (!string.IsNullOrWhiteSpace(RefreshDatabaseParameters.RestoreCustomOptions))
                {
                    c.RestoreCustomOptions = RefreshDatabaseParameters.RestoreCustomOptions;
                }
                c.Silent = true;
                c.RecoveryModeSimple = true;
                c.VersionFile = RefreshDatabaseParameters.MigrationsAssemblyPath;
            });

            

            new DifferenceWithEntityFramework().Run(migrator, migrationsAssembly);
        }
    }
}