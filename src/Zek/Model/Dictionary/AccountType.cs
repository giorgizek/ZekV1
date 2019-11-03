using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{

    public class AccountType : BaseModel<short>
    {
        //public string Code { get; set; }


        public List<AccountType> Children { get; set; }

        public short? ParentId { get; set; }
        public AccountType Parent { get; set; }
    }

    public class AccountTypeMap : BaseModelMap<AccountType, short>
    {
        public AccountTypeMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_AccountType", "Dictionary");

            HasOne(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.Restrict);
            
            //Property(t => t.Code).HasMaxLength(10).IsRequired();
        }
    }



    public class AccountTypeTranslate : TranslateModel<AccountType, short>
    {
    }

    public class AccountTypeTranslateMap : TranslateModelMap<AccountTypeTranslate, AccountType, short>
    {
        public AccountTypeTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DT_AccountType", "Translate");
        }
    }
}
