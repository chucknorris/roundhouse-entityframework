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
    using System.IO;
    using System.Reflection;
    using System.Linq;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Text;
    using roundhouse;
    using roundhouse.infrastructure.app;


    public class DifferenceWithEntityFramework
    {
        private static string path_to_sql_scripts_up_folder;

        /// <summary>
        /// Set up your migrator and call this to generate a diff file. Known limitations - will not detect size changes or renames. In other words, destructive changes will need to be done by hand.
        /// </summary>
        /// <param name="databaseMigrator">The Migrator to use when running.</param>
        /// <param name="migrationsAssembly">This is the assembly that contains your mapping files.</param>
        public void Run(Migrate databaseMigrator, Assembly migrationsAssembly)
        {

            var configuration = databaseMigrator.GetConfiguration();
            configuration.Silent = true;
            configuration.Restore = false;
            ApplicationConfiguraton.set_defaults_if_properties_are_not_set(configuration);
            path_to_sql_scripts_up_folder = Path.Combine(configuration.SqlFilesDirectory, configuration.UpFolderName);

            databaseMigrator.Set(c =>
            {
                c.Silent = true;
                c.VersionFile = migrationsAssembly.Location;
            }
        );

            run_changes(databaseMigrator, configuration, migrationsAssembly);
        }

        private void run_changes(Migrate migrator, ConfigurationPropertyHolder configuration, Assembly migrations_assembly)
        {
            var files_directory = configuration.SqlFilesDirectory;
            configuration.SqlFilesDirectory = ".";
            bool restoring_the_database = RefreshDatabaseParameters.RestoreDatabase;
            var initial_development = RefreshDatabaseParameters.DropDatabaseFirst;

            if (initial_development)
            {
                migrator.RunDropCreate();
            }
            else if (restoring_the_database)
            {
                copy_db_local_and_set_restore_path(configuration);
                configuration.Restore = true;
                migrator.RunRestore();
            }
            else
            {
                migrator.Run();
            }

            generate_database_changes(migrations_assembly);
            Console.WriteLine("NOTE: To regenerate files you need to first delete them from the file system.");

            configuration.SqlFilesDirectory = files_directory;
            configuration.Restore = false;
            migrator.Run();
        }

        private void generate_database_changes(Assembly migrations_assembly)
        {
            Console.WriteLine("");
            Console.WriteLine("".PadLeft(60, '+'));
            Console.WriteLine("-Entity Framework-".PadLeft(30, '+').PadRight(60, '+'));

            //IObjectContextAdapter
            Type dbConfiguration = migrations_assembly.GetTypes().Where(x => x.IsClass && x.IsSubclassOf(typeof(DbMigrationsConfiguration))).FirstOrDefault();
            if (dbConfiguration == null) throw new Exception("You must have one System.Data.Entity.Migrations.DbMigrationsConfiguration<TContext> inheritor in the application");
            DbMigrationsConfiguration config = get_instance_of<DbMigrationsConfiguration>(dbConfiguration);
            config.TargetDatabase = new DbConnectionInfo(RefreshDatabaseParameters.Database.GetConnectionString(), "System.Data.SqlClient");

            //Type dbContextType = migrations_assembly.GetTypes().Where(x => x.IsClass && x.IsSubclassOf(typeof (DbContext))).First();
            //if (dbContextType == null) throw new Exception("You must have one System.Data.Entity.DbContext inheritor in the application");
            //config.ContextType = dbContextType;

            //feedback from the EF team to construct a new DBMigrator to start the loop and for each MigratorScripting Decorator - Update doesn't appear to be idempotent.
            //DbMigrator efMigrator = new DbMigrator(config);
            string lastMigration = DbMigrator.InitialDatabase;
            foreach (string migration in (new DbMigrator(config)).GetLocalMigrations())
            {
                var filePath = Path.Combine(path_to_sql_scripts_up_folder, migration) + ".sql";
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Generating or updating script for Migration '{0}' to file path '{1}'.", migration, filePath);
                    var sql = new MigratorScriptingDecorator(new DbMigrator(config)).ScriptUpdate(sourceMigration: lastMigration, targetMigration: migration);

                    File.WriteAllText(filePath, sql, Encoding.UTF8);
                    //efMigrator.Update(migration);
                }
                lastMigration = migration;
            }

            Console.WriteLine("-Entity Framework-".PadLeft(30, '+').PadRight(60, '+'));
            Console.WriteLine("".PadLeft(60, '+'));
            Console.WriteLine("");
        }

        private T get_instance_of<T>(Type object_type)
        {
            if (object_type == null) throw new NullReferenceException(string.Format("A type cannot be created - it's likely you passed in null."));

            ConstructorInfo ci = object_type.GetConstructor(new Type[] { });

            return (T)ci.Invoke(new object[] { });
        }

        private void copy_db_local_and_set_restore_path(ConfigurationPropertyHolder configuration)
        {
            var originalPath = Path.GetFullPath(configuration.RestoreFromPath);
            var originalFI = new FileInfo(originalPath);
            var tempPath = Path.Combine(Path.GetTempPath(), "databases");
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            var localPath = Path.Combine(tempPath, Path.GetFileName(originalPath));
            if (File.Exists(localPath))
            {
                var tempFI = new FileInfo(localPath);
                if (originalFI.Length != tempFI.Length)
                {
                    File.Copy(originalPath, localPath, true);
                }
            }
            else
            {
                File.Copy(originalPath, localPath, true);
            }
            configuration.RestoreFromPath = localPath;
        }
    }
}