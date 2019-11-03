using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class TransactionType : BaseModel<byte>
    {
        //public string Code { get; set; }
    }

    public class TransactionTypeMap : BaseModelMap<TransactionType, byte>
    {
        public TransactionTypeMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_TransactionType", "Dictionary");
            //Property(t => t.Code).HasMaxLength(10).IsRequired();
        }
    }



    public class TransactionTypeTranslate : TranslateModel<TransactionType, byte>
    {
    }

    public class TransactionTypeTranslateMap : TranslateModelMap<TransactionTypeTranslate, TransactionType, byte>
    {
        public TransactionTypeTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DT_TransactionType", "Translate");
        }
    }
}
