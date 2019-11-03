﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zek.Test.CC {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WSBaseRequest", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Request")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Zek.Test.CC.BaseRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Zek.Test.CC.RequestOfAdminLoginRequest))]
    public partial class WSBaseRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ApplicationKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> TimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LanguageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, string> DataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ApplicationKey {
            get {
                return this.ApplicationKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.ApplicationKeyField, value) != true)) {
                    this.ApplicationKeyField = value;
                    this.RaisePropertyChanged("ApplicationKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Timestamp {
            get {
                return this.TimestampField;
            }
            set {
                if ((this.TimestampField.Equals(value) != true)) {
                    this.TimestampField = value;
                    this.RaisePropertyChanged("Timestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string Language {
            get {
                return this.LanguageField;
            }
            set {
                if ((object.ReferenceEquals(this.LanguageField, value) != true)) {
                    this.LanguageField = value;
                    this.RaisePropertyChanged("Language");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public System.Collections.Generic.Dictionary<string, string> Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseRequest", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Request")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Zek.Test.CC.RequestOfAdminLoginRequest))]
    public partial class BaseRequest : Zek.Test.CC.WSBaseRequest {
        
        private string RequestIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string RequestID {
            get {
                return this.RequestIDField;
            }
            set {
                if ((object.ReferenceEquals(this.RequestIDField, value) != true)) {
                    this.RequestIDField = value;
                    this.RaisePropertyChanged("RequestID");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestOfAdminLoginRequest", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Request")]
    [System.SerializableAttribute()]
    public partial class RequestOfAdminLoginRequest : Zek.Test.CC.BaseRequest {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Zek.Test.CC.AdminLoginRequest ValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Zek.Test.CC.AdminLoginRequest Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AdminLoginRequest", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Request")]
    [System.SerializableAttribute()]
    public partial class AdminLoginRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseResponse", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Response")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Zek.Test.CC.ResponseOfAdminLoginResponse))]
    public partial class BaseResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RequestHashField;
        
        private string RequestIDField;
        
        private System.DateTime TimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Zek.Test.CC.ErrorCodes ErrorCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExceptionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RequestHash {
            get {
                return this.RequestHashField;
            }
            set {
                if ((object.ReferenceEquals(this.RequestHashField, value) != true)) {
                    this.RequestHashField = value;
                    this.RaisePropertyChanged("RequestHash");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string RequestID {
            get {
                return this.RequestIDField;
            }
            set {
                if ((object.ReferenceEquals(this.RequestIDField, value) != true)) {
                    this.RequestIDField = value;
                    this.RaisePropertyChanged("RequestID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime Timestamp {
            get {
                return this.TimestampField;
            }
            set {
                if ((this.TimestampField.Equals(value) != true)) {
                    this.TimestampField = value;
                    this.RaisePropertyChanged("Timestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public Zek.Test.CC.ErrorCodes ErrorCode {
            get {
                return this.ErrorCodeField;
            }
            set {
                if ((this.ErrorCodeField.Equals(value) != true)) {
                    this.ErrorCodeField = value;
                    this.RaisePropertyChanged("ErrorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string Exception {
            get {
                return this.ExceptionField;
            }
            set {
                if ((object.ReferenceEquals(this.ExceptionField, value) != true)) {
                    this.ExceptionField = value;
                    this.RaisePropertyChanged("Exception");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseOfAdminLoginResponse", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Response")]
    [System.SerializableAttribute()]
    public partial class ResponseOfAdminLoginResponse : Zek.Test.CC.BaseResponse {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Zek.Test.CC.AdminLoginResponse ValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Zek.Test.CC.AdminLoginResponse Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ErrorCodes", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model")]
    public enum ErrorCodes : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InternalError = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RequestIDIsRequired = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ApplicationKeyIsInvalid = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RequestValueIsNull = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RequestValueParametersIsEmpty = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WebServiceError = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PasswordIsNull = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PasswordIsEmpty = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PasswordIncorrect = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserIDIsRequired = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserIDOrUserNameIsRequired = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserNameDecrypt = 12,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserNotFound = 13,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserRestrictedDate = 14,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PasswordExpired = 15,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PolicyNotFound = 16,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MerchantIDNotFound = 17,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AccountIDNotFound = 18,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TransactionIDIsRequired = 19,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UnknownTransactionID = 20,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientPersonalNumberIsInvalid = 21,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AmountRequired = 22,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DuplicatedTransactionID = 23,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PolicyNumberIsRequired = 24,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PeriodIsClosed = 25,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NewPolicyNumberIsInvalid = 26,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NewPolicyNumberIsExists = 27,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OperationTypeIsRequired = 28,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PersonNotFound = 29,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MobileCliamManagerAuthenticationNotInRole = 30,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SMSMobileNumberIsEmptyOrNotValidate = 31,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SMSMessageIsEmpty = 32,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VehiclePasswordIncorrect = 33,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VehicleNotFound = 34,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NotFound = 35,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MedicalDeclarationNumberIsEmpty = 36,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AdminLoginResponse", Namespace="http://schemas.datacontract.org/2004/07/Insurance.Model.WS.Response")]
    [System.SerializableAttribute()]
    public partial class AdminLoginResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserFullNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] UserRolesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserFullName {
            get {
                return this.UserFullNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserFullNameField, value) != true)) {
                    this.UserFullNameField = value;
                    this.RaisePropertyChanged("UserFullName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((this.UserIDField.Equals(value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] UserRoles {
            get {
                return this.UserRolesField;
            }
            set {
                if ((object.ReferenceEquals(this.UserRolesField, value) != true)) {
                    this.UserRolesField = value;
                    this.RaisePropertyChanged("UserRoles");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CC.ICustomerCare")]
    public interface ICustomerCare {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerCare/AdminLogin", ReplyAction="http://tempuri.org/ICustomerCare/AdminLoginResponse")]
        Zek.Test.CC.ResponseOfAdminLoginResponse AdminLogin(Zek.Test.CC.RequestOfAdminLoginRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerCare/AdminLogin", ReplyAction="http://tempuri.org/ICustomerCare/AdminLoginResponse")]
        System.Threading.Tasks.Task<Zek.Test.CC.ResponseOfAdminLoginResponse> AdminLoginAsync(Zek.Test.CC.RequestOfAdminLoginRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerCareChannel : Zek.Test.CC.ICustomerCare, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerCareClient : System.ServiceModel.ClientBase<Zek.Test.CC.ICustomerCare>, Zek.Test.CC.ICustomerCare {
        
        public CustomerCareClient() {
        }
        
        public CustomerCareClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomerCareClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerCareClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerCareClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Zek.Test.CC.ResponseOfAdminLoginResponse AdminLogin(Zek.Test.CC.RequestOfAdminLoginRequest request) {
            return base.Channel.AdminLogin(request);
        }
        
        public System.Threading.Tasks.Task<Zek.Test.CC.ResponseOfAdminLoginResponse> AdminLoginAsync(Zek.Test.CC.RequestOfAdminLoginRequest request) {
            return base.Channel.AdminLoginAsync(request);
        }
    }
}
