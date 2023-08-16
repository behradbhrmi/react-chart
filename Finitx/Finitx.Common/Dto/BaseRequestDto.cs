using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Dto
{
    public class BaseRequestDto<T>
    {
        public BaseRequestDto()
        {

        }
        public virtual T Data { get; set; }
      
    }
}
