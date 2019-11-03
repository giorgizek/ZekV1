using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Model.Base;
using Zek.Model.Membership.Identity;

namespace Zek.Model.Attachment
{
    public class Attachment : Attachment<User>
    {
    }

    public class Attachment<TUser> : AttachmentPoco
      where TUser : User
    {
        public TUser Creator { get; set; }

        public TUser Modifier { get; set; }
    }
    public class AttachmentMap<TAttachment, TUser> : AttachmentPocoMap<TAttachment>
        where TAttachment : Attachment<TUser>
        where TUser : User
    {
        public AttachmentMap(ModelBuilder builder) : base(builder)
        {
            HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).OnDelete(DeleteBehavior.Restrict);
        }
    }


    public class AttachmentPoco : PocoModel<int>
    {
        public int ApplicationId { get; set; }
        public int AreaId { get; set; }

        public int? TypeId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string CheckSum { get; set; }
    }

    public class AttachmentPocoMap<TAttachment> : PocoModelMap<TAttachment, int>
        where TAttachment : AttachmentPoco
    {
        public AttachmentPocoMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_Attachment", "Attachment");
            HasIndex(t => new { t.ApplicationId, t.AreaId });

            Property(f => f.ApplicationId).HasDefaultValue(0);
            Property(f => f.AreaId).HasDefaultValue(0);

            Property(t => t.FileName).HasMaxLength(260).IsRequired();
            Property(t => t.FileType).HasMaxLength(10).IsRequired();
            Property(t => t.CheckSum).HasMaxLength(100).IsRequired();
            HasIndex(t => t.CheckSum);
        }
    }
}
