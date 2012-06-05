using AutoMapper;

namespace Guidelines.AutoMapper.Formatters
{
    public class ZeroToNullFormatter : ValueFormatter<int?>
    {
        protected override string FormatValueCore(int? value)
        {
            return (value != null && value == 0) ? string.Empty : (value.HasValue ? value.Value.ToString() : string.Empty);
        }
    }
}
