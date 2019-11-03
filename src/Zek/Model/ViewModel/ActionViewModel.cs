namespace Zek.Model.ViewModel
{
    public class BaseActionViewModel<TId>
    {
        /// <summary>
        /// ID
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// Controller name
        /// </summary>
        public string Controller { get; set; }
    }

    public class ActionViewModel : ActionViewModel<int?>
    {
        public ActionViewModel(int? id = null, string controller = null, string action = "Delete") : base(id, controller, action)
        {
        }

    }

    public class ActionViewModel<TId> : BaseActionViewModel<TId>
    {
        public ActionViewModel(TId id = default(TId), string controller = null, string action = "Delete")
        {
            Id = id;
            Controller = controller;
            Action = action;
        }
        
        /// <summary>
        /// Action name
        /// </summary>
        public string Action { get; set; }


        public string OnAjaxSuccess { get; set; }
    }



  
    public class RowActionViewModel : RowActionViewModel<int?>
    {
        public RowActionViewModel(
            int? id = null,
            string controller = null, 
            string detailsAction = "Details",
            string editAction = "Edit",
            string approveAction = "Approve",
            string deleteAction = "Delete") : base(id, controller, detailsAction, editAction, approveAction, deleteAction)
        {
        }
    }

    public class RowActionViewModel<TId> : BaseActionViewModel<TId>
    {
        public RowActionViewModel(
            TId id = default(TId), 
            string controller = null, 
            string detailsAction = "Details",
            string editAction = "Edit",
            string approveAction = "Approve",
            string deleteAction = "Delete"
            )
        {
            Id = id;
            Controller = controller;
            DetailsAction = detailsAction;
            EditAction = editAction;
            ApproveAction = approveAction;
            DeleteAction = deleteAction;

            ShowDetail = true;
            ShowEdit = true;
            ShowApprove = true;
            ShowDelete = true;
        }

        /// <summary>
        /// Details action name
        /// </summary>
        public string DetailsAction { get; set; }

        public bool ShowDetail
        {
            get;
            set;
        }

        /// <summary>
        /// Edit action name
        /// </summary>
        public string EditAction { get; set; }
        public bool ShowEdit { get; set; }

        /// <summary>
        /// Edit action name
        /// </summary>
        public string ApproveAction { get; set; }

        public bool ShowApprove { get; set; }

        /// <summary>
        /// Delete action name
        /// </summary>
        public string DeleteAction { get; set; }

        public bool ShowDelete { get; set; }
    }
}
