using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using ErrorInfo = DevExpress.XtraEditors.DXErrorProvider.ErrorInfo;

namespace Zek.DataModel
{
    public class DXDataErrorInfo : IDXDataErrorInfo
    {
        public DXDataErrorInfo()
        {
            _properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
              .Where(prop => prop.CanRead && prop.CanWrite)
              .Select(prop => prop.Name).ToArray();

            _errors = new Dictionary<string, string>();
        }
        public virtual bool IsValid
        {
            get
            {
                var info = new ErrorInfo();
                GetError(info);
                return info.ErrorText.Length == 0;
            }
        }

        public bool ReadOnly { get; set; }

        public string Error { get; set; }

        protected static readonly object Lock = new object();

        private readonly string[] _properties;
        private readonly Dictionary<string, string> _errors;
        public virtual void ClearErrors()
        {
            _errors.Clear();
        }
        public virtual void ClearError(string propertyName)
        {
            SetError(propertyName, string.Empty);
        }
        public virtual void SetError(string propertyName, string errorText)
        {
            if (!string.IsNullOrEmpty(errorText))
            {
                if (!_errors.ContainsKey(propertyName))
                    _errors.Add(propertyName, errorText);
                else
                    _errors[propertyName] = errorText;
            }
            else
            {
                _errors.Remove(propertyName);
            }
        }
        

        public virtual void GetError(ErrorInfo info)
        {
            var propertyInfo = new ErrorInfo();
            foreach (var prop in _properties)
            {
                GetPropertyError(prop, propertyInfo);
                if (!string.IsNullOrEmpty(propertyInfo.ErrorText))
                {
                    info.ErrorText = "This object has errors: " + propertyInfo.ErrorText;
                    break;
                }
            }
        }
        public virtual void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (_errors.ContainsKey(propertyName) && !string.IsNullOrEmpty(_errors[propertyName]))
                info.ErrorText = _errors[propertyName];
            //else
            //    switch (propertyName)
            //    {
            //        case "PolicyNumber":
            //            if (string.IsNullOrWhiteSpace(PolicyNumber))
            //                info.ErrorText = string.Format("The '{0}' field required", propertyName);
            //            break;



            //        case "Premium":
            //            if (Premium <= 0m)
            //                info.ErrorText = string.Format("The '{0}' field must be greater then 0.00", propertyName);
            //            break;

            //        case "Risk":
            //            if (Risk <= 0m)
            //                info.ErrorText = string.Format("The '{0}' field must be greater then 0.00", propertyName);
            //            break;


            //        case "ContractID":
            //            if (!string.IsNullOrWhiteSpace(ContractNumber) && ContractID.IsNullOrDefault())
            //                info.ErrorText = string.Format("The '{0}' field must be inited", propertyName);
            //            break;

            //        case "PolicyHolderFirstName":
            //            if (string.IsNullOrWhiteSpace(PolicyHolderFirstName) && PolicyHolderID.IsNullOrDefault())
            //                info.ErrorText = string.Format("The '{0}' field cannot be empty", propertyName);
            //            break;

            //        case "PolicyHolderLastName":
            //            if (string.IsNullOrWhiteSpace(PolicyHolderLastName) && PolicyHolderID.IsNullOrDefault())
            //                info.ErrorText = string.Format("The '{0}' field cannot be empty", propertyName);
            //            break;

            //        case "ProductID":
            //            if (ProductID.IsNullOrDefault())
            //                info.ErrorText = string.Format("The '{0}' field cannot be empty", propertyName);
            //            break;

            //        case "FromDate":
            //        case "ToDate":
            //            if (FromDate > ToDate)
            //                info.ErrorText = string.Format("The 'FromDate' must be greater then 'ToDate'", propertyName);
            //            break;
            //    }

            if (!string.IsNullOrEmpty(info.ErrorText) && info.ErrorType == ErrorType.None)
                info.ErrorType = ErrorType.Critical;
        }
    }
}
