using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Xml;

namespace Amazon.PAAPI.WCF
{
	public class AmazonHeader : MessageHeader {
		private string	name;
		private string	value;

		public AmazonHeader(string name, string value) {
			this.name	= name;
			this.value	= value;
		}

		public override string Name			{ get { return name; } } 
		public override string Namespace	{ get { return ConfigurationManager.AppSettings["amazonSecurityNamespace"]; } }

		protected override void OnWriteHeaderContents(XmlDictionaryWriter xmlDictionaryWriter, MessageVersion messageVersion) {
			xmlDictionaryWriter.WriteString(value);
		}
	}
}
