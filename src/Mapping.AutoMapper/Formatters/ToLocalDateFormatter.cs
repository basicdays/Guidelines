using System;
using AutoMapper;
using Guidelines.Core;

namespace Guidelines.Mapping.AutoMapper.Formatters
{
	public class ToLocalDateFormatter : ValueFormatter<DateTime?>
    {
		private readonly ILocalizedDateConverter _converter;

		public ToLocalDateFormatter(ILocalizedDateConverter converter)
        {
			_converter = converter;
        }

        protected override string FormatValueCore(DateTime? value)
        {
        	var result = string.Empty;

			if(value.HasValue) {
				result = _converter.Convert(value.Value).ToString("d");
			}

			return result;
        }
    }
}
