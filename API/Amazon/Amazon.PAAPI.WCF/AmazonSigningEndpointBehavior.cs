using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Amazon.PAAPI.WCF
{
	public class AmazonSigningEndpointBehavior : IEndpointBehavior {
		private string	_accessKeyId	= "";
		private string	_secretKey	= "";

        public AmazonSigningEndpointBehavior()
        {
            this._accessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            this._secretKey = ConfigurationManager.AppSettings["secretKey"];
        }

		public AmazonSigningEndpointBehavior(string accessKeyId, string secretKey) {
			this._accessKeyId	= accessKeyId;
			this._secretKey		= secretKey;
		}

		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime) {
			clientRuntime.MessageInspectors.Add(new AmazonSigningMessageInspector(_accessKeyId, _secretKey));
		}

		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher) { return; }
		public void Validate(ServiceEndpoint serviceEndpoint) { return; }
		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters) { return; }
	}
}
