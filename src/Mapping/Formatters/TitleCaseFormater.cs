using System.Globalization;
using System.Threading;
using AutoMapper;

namespace Guidelines.Mapping.Formatters
{
    public class TitleCaseFormater : ValueFormatter<string>
    {
        protected override string FormatValueCore(string value)
        {
            //Get the culture property of the thread.
            return ToTitleCase(value);
        }

        public static string ToTitleCase(string value)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(value.ToLower());
        }
    }
}
