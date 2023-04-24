using OnionBaseArchitecture.Application.Abstractions.Caching;
using OnionBaseArchitecture.Application.Abstractions.Services;

namespace OnionBaseArchitecture.Caching.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILanguageTextService _languageTextService;
        private readonly ILanguageService _languageService;

        public CacheService(ICacheManager cacheManager, ILanguageTextService languageTextService, ILanguageService languageService)
        {
            _cacheManager = cacheManager;
            _languageTextService = languageTextService;
            _languageService = languageService;
        }

        public async Task<bool> ClearCache()
        {
            return await SetCache();
        }

        public async Task<bool> SetCache()
        {
            try
            {
                await _cacheManager.ClearAsync();
                await _cacheManager.SetAsync<string>("IsSetCache", "ready");

                var languageTexts = await _languageTextService.GetAllAsync();
                var languages = await _languageService.GetAllAsync();
                foreach (var language in languageTexts)
                    await _cacheManager.SetAsync<string>(string.Format("{0}_{1}", languages.Where(x => x.Id == language.LanguageId).FirstOrDefault().ShortCode, language.Name), language.Value);
                //tr_menu.name.approved = "Onaylanmış Ödemeler"

                return true;
            }
            catch (Exception)
            { }

            return false;
        }

        public async Task<string> StringControl(string Name)
        {
            if (true)
                await ClearCache();

            string lang = "tr";
            //var cookies = _httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Culture"];
            //// cookies null ise languageshortcode = tr
            //if (cookies == null)
            //{
            //    lang = "tr";
            //}
            //else
            //{
            //    string[] languageShortCodes = cookies.Split("|");
            //    lang = languageShortCodes[0].Replace("c=", "");
            //}
            Name = Name.Trim();
            string text = await _cacheManager.GetAsync<string>(string.Format("{0}_{1}", lang, Name));

            if (!string.IsNullOrEmpty(text))
                return text;
            else
                return Name;
        }

    }
}
