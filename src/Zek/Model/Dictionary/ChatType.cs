using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.Membership.Identity;

namespace Zek.Model.Dictionary
{
    public class ChatType<TUser> : BaseModel<int, TUser>
             where TUser : User
    {
        //public bool IsAnonymous { get; set; }
    }

    public class ChatTypeMap<TUser> : BaseModelMap<ChatType<TUser>, int, TUser>
          where TUser : User
    {
        public ChatTypeMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_ChatType", "Dictionary");

            //Property(t => t.IsAnonymous).HasDefaultValue(false);
            //HasIndex(t => t.IsAnonymous);
        }
    }



    public class ChatTypeTranslate<TUser> : TranslateModel<ChatType<TUser>, int>
        where TUser : User
    {

    }

    public class ChatTypeTranslateMap<TUser> : TranslateModelMap<ChatTypeTranslate<TUser>, ChatType<TUser>, int>
         where TUser : User
    {
        public ChatTypeTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DT_ChatType", "Translate");
        }
    }
}
