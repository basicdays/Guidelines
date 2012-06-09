using System;
using AutoMapper;
using Guidelines.Core;

namespace Guidelines.Mapping.AutoMapper.Formatters
{
	public class ToLocalDateTimeFormatter : ValueFormatter<DateTime?>
	{
		private readonly ILocalizedDateConverter _converter;

		public ToLocalDateTimeFormatter(ILocalizedDateConverter converter)
		{
			_converter = converter;
		}

		protected override string FormatValueCore(DateTime? value)
		{
			var result = string.Empty;

			if (value.HasValue) {
				result = _converter.Convert(value.Value).ToString();
			}

			return result;
		}
	}
}