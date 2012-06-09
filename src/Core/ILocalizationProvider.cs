using System;

namespace Guidelines.Core
{
    public interface ILocalizationProvider
    {
        TimeZoneInfo GetCurrentTimeZone();

        string GetCurrentLanguageCode();
    }
}
