using FluentMigrator;
using Nop.Data.Migrations;

namespace Finitx.Plugin.Widgets.ProductBadges.Migrations
{
    [NopMigration("2022/03/04 17:08:55:1687541", "Finitx.Plugin.Widgets.ProductBadges schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
        }
    }
}
