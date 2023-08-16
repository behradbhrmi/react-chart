using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Common.Service
{
    public interface IFinitxLanguageService
    {
        public Task<int?> GetLanguageByUniqueSeoCode(string UniqueSeoCode);
        public Task<int?> AddPresianLanguage();
        public Task<int?> AddAmericanEnglishLanguage();
    }
}
