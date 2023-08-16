using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Helper
{
    public interface IResourceHelper
    {
        Task AddOrUpdateResource(Dictionary<string,string> enResource,Dictionary<string,string> faResource);
    }
}
