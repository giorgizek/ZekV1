﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zek.WSCartuVerifyCards {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.cartubank.ge/", ConfigurationName="WSCartuVerifyCards.VerifyCardsProxySoap")]
    public interface VerifyCardsProxySoap {
        
        // CODEGEN: Parameter 'PostData' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cartubank.ge/SendRequest", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Zek.WSCartuVerifyCards.SendRequestResponse SendRequest(Zek.WSCartuVerifyCards.SendRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.cartubank.ge/VerifyCard", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Zek.WSCartuVerifyCards.VerifyResponse VerifyCard(string VerifyRequest, string Signature);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.cartubank.ge/")]
    public enum HttpStatusCode {
        
        /// <remarks/>
        Continue,
        
        /// <remarks/>
        SwitchingProtocols,
        
        /// <remarks/>
        OK,
        
        /// <remarks/>
        Created,
        
        /// <remarks/>
        Accepted,
        
        /// <remarks/>
        NonAuthoritativeInformation,
        
        /// <remarks/>
        NoContent,
        
        /// <remarks/>
        ResetContent,
        
        /// <remarks/>
        PartialContent,
        
        /// <remarks/>
        MultipleChoices,
        
        /// <remarks/>
        Ambiguous,
        
        /// <remarks/>
        MovedPermanently,
        
        /// <remarks/>
        Moved,
        
        /// <remarks/>
        Found,
        
        /// <remarks/>
        Redirect,
        
        /// <remarks/>
        SeeOther,
        
        /// <remarks/>
        RedirectMethod,
        
        /// <remarks/>
        NotModified,
        
        /// <remarks/>
        UseProxy,
        
        /// <remarks/>
        Unused,
        
        /// <remarks/>
        TemporaryRedirect,
        
        /// <remarks/>
        RedirectKeepVerb,
        
        /// <remarks/>
        BadRequest,
        
        /// <remarks/>
        Unauthorized,
        
        /// <remarks/>
        PaymentRequired,
        
        /// <remarks/>
        Forbidden,
        
        /// <remarks/>
        NotFound,
        
        /// <remarks/>
        MethodNotAllowed,
        
        /// <remarks/>
        NotAcceptable,
        
        /// <remarks/>
        ProxyAuthenticationRequired,
        
        /// <remarks/>
        RequestTimeout,
        
        /// <remarks/>
        Conflict,
        
        /// <remarks/>
        Gone,
        
        /// <remarks/>
        LengthRequired,
        
        /// <remarks/>
        PreconditionFailed,
        
        /// <remarks/>
        RequestEntityTooLarge,
        
        /// <remarks/>
        RequestUriTooLong,
        
        /// <remarks/>
        UnsupportedMediaType,
        
        /// <remarks/>
        RequestedRangeNotSatisfiable,
        
        /// <remarks/>
        ExpectationFailed,
        
        /// <remarks/>
        InternalServerError,
        
        /// <remarks/>
        NotImplemented,
        
        /// <remarks/>
        BadGateway,
        
        /// <remarks/>
        ServiceUnavailable,
        
        /// <remarks/>
        GatewayTimeout,
        
        /// <remarks/>
        HttpVersionNotSupported,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SendRequest", WrapperNamespace="http://www.cartubank.ge/", IsWrapped=true)]
    public partial class SendRequestRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.cartubank.ge/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] PostData;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.cartubank.ge/", Order=1)]
        public string Uri;
        
        public SendRequestRequest() {
        }
        
        public SendRequestRequest(byte[] PostData, string Uri) {
            this.PostData = PostData;
            this.Uri = Uri;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SendRequestResponse", WrapperNamespace="http://www.cartubank.ge/", IsWrapped=true)]
    public partial class SendRequestResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.cartubank.ge/", Order=0)]
        public bool SendRequestResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.cartubank.ge/", Order=1)]
        public Zek.WSCartuVerifyCards.HttpStatusCode HttpStatus;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.cartubank.ge/", Order=2)]
        public string sMessage;
        
        public SendRequestResponse() {
        }
        
        public SendRequestResponse(bool SendRequestResult, Zek.WSCartuVerifyCards.HttpStatusCode HttpStatus, string sMessage) {
            this.SendRequestResult = SendRequestResult;
            this.HttpStatus = HttpStatus;
            this.sMessage = sMessage;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.cartubank.ge/")]
    public partial class VerifyResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string transactionIdField;
        
        private string paymentIdField;
        
        private string statusField;
        
        private int tryCountField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string TransactionId {
            get {
                return this.transactionIdField;
            }
            set {
                this.transactionIdField = value;
                this.RaisePropertyChanged("TransactionId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string PaymentId {
            get {
                return this.paymentIdField;
            }
            set {
                this.paymentIdField = value;
                this.RaisePropertyChanged("PaymentId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("Status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int TryCount {
            get {
                return this.tryCountField;
            }
            set {
                this.tryCountField = value;
                this.RaisePropertyChanged("TryCount");
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
    public interface VerifyCardsProxySoapChannel : Zek.WSCartuVerifyCards.VerifyCardsProxySoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VerifyCardsProxySoapClient : System.ServiceModel.ClientBase<Zek.WSCartuVerifyCards.VerifyCardsProxySoap>, Zek.WSCartuVerifyCards.VerifyCardsProxySoap {
        
        public VerifyCardsProxySoapClient() {
        }
        
        public VerifyCardsProxySoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VerifyCardsProxySoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VerifyCardsProxySoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VerifyCardsProxySoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Zek.WSCartuVerifyCards.SendRequestResponse Zek.WSCartuVerifyCards.VerifyCardsProxySoap.SendRequest(Zek.WSCartuVerifyCards.SendRequestRequest request) {
            return base.Channel.SendRequest(request);
        }
        
        public bool SendRequest(byte[] PostData, string Uri, out Zek.WSCartuVerifyCards.HttpStatusCode HttpStatus, out string sMessage) {
            Zek.WSCartuVerifyCards.SendRequestRequest inValue = new Zek.WSCartuVerifyCards.SendRequestRequest();
            inValue.PostData = PostData;
            inValue.Uri = Uri;
            Zek.WSCartuVerifyCards.SendRequestResponse retVal = ((Zek.WSCartuVerifyCards.VerifyCardsProxySoap)(this)).SendRequest(inValue);
            HttpStatus = retVal.HttpStatus;
            sMessage = retVal.sMessage;
            return retVal.SendRequestResult;
        }
        
        public Zek.WSCartuVerifyCards.VerifyResponse VerifyCard(string VerifyRequest, string Signature) {
            return base.Channel.VerifyCard(VerifyRequest, Signature);
        }
    }
}
