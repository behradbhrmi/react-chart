using Finitx.Common.Dto;
using Finitx.Plugin.Misc.LogEventsDataToServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Services
{
    public interface IFinitxUserService
    {
        Task<BaseResponseDto<object>> SendUserRegistration(UserCreateRequestDto user);
        Task<BaseResponseDto<object>> SendUserUpdate(UserEditProfileRequestDto user);
        Task<BaseResponseDto<object>> SendUserConfirmation(UserConfirmRequestDto user);
        Task<BaseResponseDto<object>> SendChangePassword(UserChangePasswordRequestDto user);
    }
}
