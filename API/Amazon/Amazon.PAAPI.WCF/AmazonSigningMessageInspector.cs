using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Text.RegularExpressions;

namespace Amazon.PAAPI.WCF
{
	public class AmazonSigningMessageInspector : IClientMessageInspector {
		private string	_accessKeyId	= "";
		private string	_secretKey	= "";

		public AmazonSigningMessageInspector(string accessKeyId, string secretKey) {
			this._accessKeyId	= accessKeyId;
			this._secretKey		= secretKey;
		}

		public object BeforeSendRequest(ref Message request, IClientChannel channel) {
			// prepare the data to sign
			string		operation		= Regex.Match(request.Headers.Action, "[^/]+$").ToString();
			DateTime	now				= DateTime.UtcNow;
			string		timestamp		= now.ToString("yyyy-MM-ddTHH:mm:ssZ");
			string		signMe			= operation + timestamp;
			byte[]		bytesToSign		= Encoding.UTF8.GetBytes(signMe);

			// sign the data
			byte[]		secretKeyBytes	= Encoding.UTF8.GetBytes(_secretKey);
			HMAC		hmacSha256		= new HMACSHA256(secretKeyBytes);
			byte[]		hashBytes		= hmacSha256.ComputeHash(bytesToSign);
			string		signature		= Convert.ToBase64String(hashBytes);

			// add the signature information to the request headers
			request.Headers.Add(new AmazonHeader("AWSAccessKeyId", _accessKeyId));
			request.Headers.Add(new AmazonHeader("Timestamp", timestamp));
			request.Headers.Add(new AmazonHeader("Signature", signature));

			return null;
		}

		public void AfterReceiveReply(ref Message reply, object correlationState) { }
	}
}
