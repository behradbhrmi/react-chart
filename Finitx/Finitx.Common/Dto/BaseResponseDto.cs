using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Dto
{
    public class BaseResponseDto<T>
    {
        public BaseResponseDto()
        {

        }

        [JsonProperty("data")]
        public virtual T Data { get; set; }

        [JsonIgnore]
        public virtual bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public virtual string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
        public virtual void SetIsSuccess()
        {
            IsSuccess = Code == "0";
        }
    }
    
    /*
     {
  "code": "0",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImIzZDc3OThiLTNmNzQtNDUzZi04NmZkLTFhZjg2NTA5MGY2NyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdG9yZSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IlN0b3JlIEFQSSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdXNlcmRhdGEiOiIiLCJqdGkiOiJkNjU4NDczMS0xZmExLTRjOTktYjBhNS0zNGFjN2Q4MDU3Y2EiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdG9yZSIsImV4cCI6MTY4NTE5MjkzMn0.dxLT4Yp1j2Qv5zeagCM4bBTy7XzvaTC-WYmav02zpM4",
    "expiration": "2023-05-27T13:08:52Z"
  },
  "message": ""
}
     */
}
