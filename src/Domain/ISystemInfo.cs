using System;

namespace Guidelines.Domain
{
    public interface ISystemInfo
    {
        TimeZoneInfo GetCurrentTimeZone();

        string GetCurrentLanguageCode();
    }
}
