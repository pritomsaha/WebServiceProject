//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestimonialWebApp.PaymentService {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MonoDevelop", "2.6.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PaymentServiceSoap", Namespace="dse.webservices")]
    public partial class PaymentService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getPaymentOperationCompleted;
        
        /// CodeRemarks
        public PaymentService() {
            this.Url = "http://127.0.0.1:8000/PaymentService.asmx";
        }
        
        public PaymentService(string url) {
            this.Url = url;
        }
        
        /// CodeRemarks
        public event getPaymentCompletedEventHandler getPaymentCompleted;
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("dse.webservices/getPayment", RequestNamespace="dse.webservices", ResponseNamespace="dse.webservices", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public double getPayment(string transactionNum) {
            object[] results = this.Invoke("getPayment", new object[] {
                        transactionNum});
            return ((double)(results[0]));
        }
        
        /// CodeRemarks
        public void getPaymentAsync(string transactionNum) {
            this.getPaymentAsync(transactionNum, null);
        }
        
        /// CodeRemarks
        public void getPaymentAsync(string transactionNum, object userState) {
            if ((this.getPaymentOperationCompleted == null)) {
                this.getPaymentOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetPaymentOperationCompleted);
            }
            this.InvokeAsync("getPayment", new object[] {
                        transactionNum}, this.getPaymentOperationCompleted, userState);
        }
        
        private void OngetPaymentOperationCompleted(object arg) {
            if ((this.getPaymentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getPaymentCompleted(this, new getPaymentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MonoDevelop", "2.6.0.0")]
    public delegate void getPaymentCompletedEventHandler(object sender, getPaymentCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MonoDevelop", "2.6.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getPaymentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getPaymentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public double Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((double)(this.results[0]));
            }
        }
    }
}
