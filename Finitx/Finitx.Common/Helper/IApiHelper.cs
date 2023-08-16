using Finitx.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Helper
{
    public interface IApiHelper
    {
        public Task<TResponse> PostData<TResponse, TRequest>(string url, TRequest requestData);
        public Task<TResponse> PutData<TResponse, TRequest>(string url, TRequest requestData);
    }
}
