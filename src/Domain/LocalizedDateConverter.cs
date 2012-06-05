using System;

namespace Guidelines.Domain
{
	/// <summary>
	/// Accepts a UTC date and converts it to the timezone provided by the localization provider.
	/// </summary>
	public class LocalizedDateConverter : ILocalizedDateConverter
	{
		private readonly ILocalizationProvider _localizationProvider;

		public LocalizedDateConverter(ILocalizationProvider localizationProvider)
		{
			_localizationProvider = localizationProvider;
		}

		public DateTime Convert(DateTime value)
		{
			var timeZone = _localizationProvider.GetCurrentTimeZone();

			var timeToConvert = DateTime.SpecifyKind(value, DateTimeKind.Utc);

			DateTime convertedDate = timeToConvert > DateTime.MinValue
			                         	? TimeZoneInfo.ConvertTimeFromUtc(timeToConvert, timeZone)
			                         	: DateTime.MinValue;

			DateTime localizedDate = timeZone != null ? convertedDate : timeToConvert;

			return localizedDate;
		}
	}
}