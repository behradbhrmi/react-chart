using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Service
{
    public class FinitxLanguageService : IFinitxLanguageService
    {
        private readonly ILanguageService _languageService;
        private readonly IRepository<Language> _languageRepository;
        public FinitxLanguageService(ILanguageService languageService, IRepository<Language> languageRepository)
        {
            _languageService = languageService;
            _languageRepository = languageRepository;   
        }
        public async Task<int?> AddPresianLanguage()
        {
            var persianLang = new Language {DefaultCurrencyId=1,FlagImageFileName= "ir.png",DisplayOrder=2,LanguageCulture="fa-IR",LimitedToStores=false,Name="FA",Published=true,Rtl=true,UniqueSeoCode="fa" };
            await _languageService.InsertLanguageAsync(persianLang);
            return persianLang.Id;
        }public async Task<int?> AddAmericanEnglishLanguage()
        {
            var persianLang = new Language {DefaultCurrencyId=0,FlagImageFileName= "us.png", DisplayOrder=1,LanguageCulture="en-US",LimitedToStores=false,Name="EN",Published=true,Rtl=false,UniqueSeoCode="en" };
            await _languageService.InsertLanguageAsync(persianLang);
            return persianLang.Id;
        }

        public async Task<int?> GetLanguageByUniqueSeoCode(string uniqueSeoCode)
        {
            var id = (await _languageRepository.GetAllAsync(x => x.Where(l => l.UniqueSeoCode == uniqueSeoCode)))?.FirstOrDefault()?.Id;
            return id;
        }
    }
}
