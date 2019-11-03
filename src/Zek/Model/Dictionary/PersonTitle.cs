using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class PersonTitle : BaseModel<byte>
    {

    }

    public class PersonTitleMap : BaseModelMap<PersonTitle, byte>
    {
        public PersonTitleMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_PersonTitle", "Dictionary");
        }
    }


    public class PersonTitleTranslate : TranslateModel<PersonTitle, byte>
    {
        
    }

    public class PersonTitleTranslateMap : TranslateModelMap<PersonTitleTranslate, PersonTitle, byte>
    {
        public PersonTitleTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DT_PersonTitle", "Translate");
        }
    }
}
