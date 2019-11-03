namespace Zek.Model.ViewModel.Ecomm
{
    public class ExecuteRegularPaymentViewModel : ExecuteTransactionViewModel
    {
        /// <summary>
        /// transaction identifier (28 characters in base64 encoding)
        /// </summary>
        public string TransactionId { get; set; }

    }
}
