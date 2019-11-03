namespace Zek.Model.ViewModels
{
    public class ActionToobarViewModel
    {
        public string ControllerName { get; set; }
        public string Target { get; set; }
        public int? Id { get; set; }

        public string DeleteDataTarget { get; set; } = "#" + Constant.Html.DeleteModalId;

    }
}
