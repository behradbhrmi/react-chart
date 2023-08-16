using Finitx.Plugin.Widgets.ProductBadges.Domains;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;

namespace Finitx.Plugin.Widgets.ProductBadges.Mapping.Builders
{
    public class PluginBuilder : NopEntityBuilder<CustomTable>
    {
        #region Methods

        public override void MapEntity(CreateTableExpressionBuilder table)
        {
        }

        #endregion
    }
}