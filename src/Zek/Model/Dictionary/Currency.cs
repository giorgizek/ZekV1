using Microsoft.EntityFrameworkCore;
using Zek.Model.Accounting;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class Currency : BaseModel<ISO4217>
    {
        public string Code { get; set; }
        public string Symbol { get; set; }

        //public List<CurrencyTranslate> Translates { get; set; }
    }

    public class CurrencyMap : BaseModelMap<Currency, ISO4217>
    {
        public CurrencyMap(ModelBuilder builder) : base(builder, false)
        {
            ToTable("DD_Currency", "Dictionary");
            Property(t => t.Code).HasMaxLength(10).IsRequired();
            HasIndex(t => t.Code).IsUnique();
            Property(t => t.Symbol).HasMaxLength(10).IsRequired();
        }
    }




    public class CurrencyTranslate : TranslateModel<Currency, ISO4217>
    {
        public string MinorUnit { get; set; }
    }

    public class CurrencyTranslateMap : TranslateModelMap<CurrencyTranslate, Currency, ISO4217>
    {
        public CurrencyTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DT_Currency", "Translate");
        }
    }
}
