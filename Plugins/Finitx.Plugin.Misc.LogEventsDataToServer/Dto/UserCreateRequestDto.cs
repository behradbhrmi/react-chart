using Finitx.Common.Dto;
using Finitx.Plugin.Misc.LogEventsDataToServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Dto
{
    public class UserCreateRequestDto : BaseRequestDto<FinitxUserVm>
    {
    }
    public class UserConfirmRequestDto : BaseRequestDto<FinitxConfirmVm>
    {
    }
    public class UserChangePasswordRequestDto : BaseRequestDto<FinitxChangePasswordVm>
    {
    }
    public class UserEditProfileRequestDto : BaseRequestDto<FinitxEditProfileVm>
    {
    }
}
