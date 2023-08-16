using Nop.Core;
using System;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Domains
{
    public partial class OrderSync : BaseEntity
    {
        public Guid OrderGuid { get; set; }
        public Guid OrderItemGuid { get; set; }
        public bool HasSync { get; set; }
    }
}
