namespace Zek.Model.ViewModel.Ecomm
{
    public class TransactionResponseViewModel : BaseResponseViewModel
    {
        private string _transactionId;

        /// <summary>
        /// transaction identifier (28 characters in base64 encoding)
        /// </summary>
        public string TransactionId
        {
            get { return _transactionId; }
            set
            {
                if (value != _transactionId)
                {
                    _transactionId = value;
                    Success = !string.IsNullOrEmpty(_transactionId);
                }

            }
        }

        public bool Success { get; private set; }
    }
}