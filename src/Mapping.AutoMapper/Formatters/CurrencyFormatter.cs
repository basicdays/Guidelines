using AutoMapper;

namespace Guidelines.Mapping.AutoMapper.Formatters
{
    public class CurrencyFormatter : ValueFormatter<decimal>
    {
        protected override string FormatValueCore(decimal value)
        {
        	return value.ToString("###,###,###,###,##0.00##");
        }
    }
}
