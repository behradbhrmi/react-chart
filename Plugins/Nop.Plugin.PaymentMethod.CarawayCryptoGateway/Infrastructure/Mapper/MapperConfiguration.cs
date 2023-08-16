using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Models;
using Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.PaymentMethod.CarawayCryptoGateway.Infrastructure.Mapper
{
    public class MapperConfiguration : Profile, IOrderedMapperProfile
    {
        public MapperConfiguration()
        {
            CreateZohoSettingMapping();
        }
        private void CreateZohoSettingMapping()
        {
            CreateMap<CarawayConfigurationModel, CarawayPaymentSettings>().ReverseMap();
        }
        public int Order => 2;
    }
}
