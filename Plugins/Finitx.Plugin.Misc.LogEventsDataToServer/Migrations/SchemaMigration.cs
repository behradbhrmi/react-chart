using Finitx.Plugin.Misc.LogEventsDataToServer.Domains;
using FluentMigrator;
using Nop.Data.Migrations;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Migrations
{
    [NopMigration("2023/06/01 08:18:05", "Finitx.Plugin.Misc.LogEventsDataToServer schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        /// <summary>
        /// Collect the UP migration expressions
        /// create table for save log of send order data if not send we will send agane
        /// </summary>
        public override void Up()
        {
           var tableName= nameof(OrderSync);
            //check table already not exist
            if (!Schema.Table(tableName).Exists())
            {
                _migrationManager.BuildTable<OrderSync>(Create);
            }           
        }
   
    }
}
