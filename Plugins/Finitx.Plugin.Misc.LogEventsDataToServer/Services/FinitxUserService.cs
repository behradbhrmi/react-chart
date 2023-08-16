using Finitx.Common.Dto;
using Finitx.Common.Helper;
using Finitx.Plugin.Misc.LogEventsDataToServer.Dto;
using Finitx.Plugin.Misc.LogEventsDataToServer.ViewModel;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Nop.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Services
{
    public class FinitxUserService : IFinitxUserService
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public FinitxUserService(ILogger logger,IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }
        public async Task<BaseResponseDto<object>> SendChangePassword(UserChangePasswordRequestDto user)
        {            
            await _logger.InsertLogAsync(Nop.Core.Domain.Logging.LogLevel.Debug, nameof(SendChangePassword), user.Data.Email);
            try
            {
                var result = await _apiHelper.PutData<BaseResponseDto<object>, FinitxChangePasswordVm>("Authenticate/api/changePassword", user.Data);
                result?.SetIsSuccess();
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<object> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponseDto<object>> SendUserConfirmation(UserConfirmRequestDto user)
        {
            await _logger.InsertLogAsync(Nop.Core.Domain.Logging.LogLevel.Debug, nameof(SendUserConfirmation), user.Data.Email);
            try
            {
                var result = await _apiHelper.PutData<BaseResponseDto<object>, FinitxConfirmVm>("Authenticate/api/confirm", user.Data);
                result?.SetIsSuccess();
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<object> { IsSuccess = false, Message = ex.Message };
            }

        }

        public async Task<BaseResponseDto<object>> SendUserRegistration(UserCreateRequestDto user)
        {
            await _logger.InsertLogAsync(Nop.Core.Domain.Logging.LogLevel.Debug, nameof(SendUserRegistration), user.Data.Email);           
            try
            { 
                var result = await _apiHelper.PostData<BaseResponseDto<object>, FinitxUserVm>("Authenticate/api/register", user.Data);
                result?.SetIsSuccess();
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<object> { IsSuccess=false,Message=ex.Message };
            }

        }

       

        public async  Task<BaseResponseDto<object>> SendUserUpdate(UserEditProfileRequestDto user)
        {
            await _logger.InsertLogAsync(Nop.Core.Domain.Logging.LogLevel.Debug, nameof(SendUserUpdate), user.Data.Email);           
            try
            {
                var result = await _apiHelper.PutData<BaseResponseDto<object>, FinitxEditProfileVm>("Authenticate/api/editProfile", user.Data);
                result?.SetIsSuccess();
                return result;
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<object> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
