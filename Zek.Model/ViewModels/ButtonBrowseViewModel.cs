namespace Zek.Model.ViewModels
{
    public class ButtonBrowseBaseViewModel
    {

        public ButtonBrowseBaseViewModel()
        {
        }

        public string Text { get; set; }

        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                if (value != _key)
                {
                    _key = value;
                    OnKeyChanged();
                }
            }
        }

        protected virtual void OnKeyChanged()
        {
            if (string.IsNullOrWhiteSpace(_key))
            {
                Text = null;
            }
        }


        public string PopupName { get; set; }
        public string PopupTitle { get; protected set; }
        public string PopupContentController { get; protected set; }
        public string PopupContentAction { get; set; } = "IndexPopup";
    }
}
