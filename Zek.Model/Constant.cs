namespace Zek.Model
{
    public static class Constant
    {
        public static class Mvc
        {
            public const string HomeControllerName = "Home";
            public const string HotelsControllerName = "Hotels";
            public const string HotelBuildingsControllerName = "HotelBuildings";
            public const string HotelDashboardsControllerName = "HotelDashboards";
            public const string HotelInvoicesControllerName = "HotelInvoices";

            public const string IndexActionName = "Index";
            public const string CreateActionName = "Create";
            public const string EditActionName = "Edit";
            public const string DeleteActionName = "Delete";


        }



        public static class Bind
        {
            public const string ExcludeCreate = "Id,CreatorId,CreateDate";
            public const string ExcludeEdit = "CreatorId,CreateDate,ModifierId,ModifiedDate";
        }


        public static class Css
        {
            public const string RowDeleteAction = "js-delete";
            //public const string TableClass = "table table-striped table-bordered table-hover";
            public const string RowDoubleClickEditForm = "dblclickEditForm";

            public class Icon
            {
                public const string Ok = "fa fa-check";
                public const string Cancel = "fa fa-ban";

                public const string Find = "fa fa-search";

                public const string Check = "fa fa-check";
                public const string CheckIn = "fa fa-sign-in";
                public const string CheckOut = "fa fa-sign-out";
                public const string Billing = "fa fa-list-alt";



                public const string Create = "fa fa-plus";
                public const string Edit = "fa fa-pencil";
                public const string Delete = "fa fa-times";
                public const string Choose = "fa fa-hand-o-up";
                public const string Approve = "fa fa-thumbs-o-up";
                public const string Disapprove = "fa fa-thumbs-o-down";
                public const string Sum = "";
                public const string Print = "fa fa-print";
                public const string Export = "fa fa-file-excel-o";
                public const string Refresh = "fa fa-refresh";
                public const string FilterPanel = "fa fa-filter";

                public const string FirstPage = "fa fa-fast-backward";
                public const string PreviousPage = "fa fa-backward";

                public const string PreviousPageOptional = "fa fa-ellipsis-h";
                public const string NextPageOptional = "fa fa-ellipsis-h";
                public const string NextPage = "fa fa-forward";
                public const string LastPage = "fa fa-fast-forward";

                public const string Star = "fa fa-star";

                public const string Info = "fa fa-info";

                public const string Email = "fa fa-envelope";
                public const string Phone = "fa fa-phone";
                public const string Fax = "fa fa-fax";
                public const string Mobile = "fa fa-mobile";
                public const string Url = "fa fa-link";
                public const string Dashboard = "fa fa-tachometer";
                public const string Invoice = "fa fa-list";
                public const string Hotel = "fa fa-bed";
                public const string Building = "fa fa-building";

                public const string Password = "fa fa-key";

                public const string ButtonBrowse = "fa fa-ellipsis-h";
            }
        }


        public static class Html
        {
            public const string EditFormId = "editForm";
            public const string CreateButtonId = "btnCreate";
            public const string EditButtonId = "btnEdit";

            public const string DeleteActionName = "Delete";
            public const string DeleteButtonId = "btnDelete";
            public const string DeleteHiddenId = "btnCancel";
            public const string DeleteConfirmButtonId = "btnDeleteConfirm";

            public const string ChooseButtonId = "btnChoose";
            public const string ApprovButtonId = "btnApprove";
            public const string SumButtonId = "btnSum";
            public const string PrintButtonId = "btnPrint";
            public const string ExportButtonId = "btnExport";
            public const string RefreshButtonId = "btnRefresh";
            public const string FilterPaneButtonlId = "btnFilterPanel";

            public const string DeleteModalId = "mdlDelete";
            public const string DeleteModalAlertId = "alertDelete";

            public const string FilterPanelModalId = "mdlFilter";

            public const string FilterPanelId = "pnlFilter";

            public const string OkId = "btnOk";
            public const string CancelButtonId = "btnCancel";


            public const string ListFormGridAjaxUpdateTargetId = "ajaxGrid";
            public const string ListFormGridId = "gridMain";
            public const string FilterFormId = "frmFilter";







            public const string ToolBarRole = "toolbar";




            public const string QuickBookingFormId = "frmQuickBooking";
            public const string QuickBookingModalId = "mdlQuickBooking";
            public const string QuickBookingAlertId = "alertQuickBooking";
            public const string QuickBookingUpdateTargetId = "pnlQuickBooking";
            //public const string QuickBookingGridId = "gridQuickBooking";

        }

        public static class Session
        {
            public const string CurrentCultureId = "CurrentCultureId";
            public const string CurrentUserId = "CurrentUserId";
            public const string CurrentHotelId = "CurrentHotelId";
        }

        //public const byte DefaultCultureId = 2;

        public const string DatePickerFormat = "yyyy-mm-dd";
        public const string DateTimePickerFormat = "yyyy-mm-dd HH:mm:ss";
        public const string TimePickerFormat = "HH:mm:ss";
        //public const string TimeMinutesPickerFormat = "HH:mm";
    }
}
