using Finitx.Plugin.Misc.LogEventsDataToServer.Domains;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Mapping.Builders
{
    public class PluginBuilder : NopEntityBuilder<OrderSync>
    {
        #region Methods

        public override void MapEntity(CreateTableExpressionBuilder table)
        {
        }

        #endregion
    }
}