using Zek.Model.Base;

namespace Zek.Model.Membership
{
    public class Comment : BaseModel<int>
    {
        public int ApplicationId { get; set; }
        public int AreaId { get; set; }

        public int? ParentId { get; set; }
        public string Text { get; set; }
    }
}
