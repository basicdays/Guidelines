using System;

namespace Guidelines.Domain
{
    public interface ILocalizationProvider
    {
        TimeZoneInfo GetCurrentTimeZone();

        string GetCurrentLanguageCode();
    }
}
