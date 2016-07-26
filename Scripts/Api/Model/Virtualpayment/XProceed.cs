using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace Xsolla {
	public class XProceed : IParseble {
//		{
//			"invoice_created":true,
//			"operation_id":36504230,
//			"api":{
//				"ver":"1.0.1"
//			}

		public bool IsInvoiceCreated { get; private set; }
		public long OperationId { get; private set; }
		public string Error { get; private set; }

		public IParseble Parse(JSONNode rootNode) {
			IsInvoiceCreated 	= rootNode ["invoice_created"].AsBool;
			OperationId 		= rootNode ["operation_id"].AsInt;
			Error 				= rootNode ["errors"].AsArray [0] ["message"];
			return this;
		}

		public override string ToString ()
		{
			return string.Format ("[XProceed: IsInvoiceCreated={0}, OperationId={1}, Error={2}]", IsInvoiceCreated, OperationId, Error);
		}
		
//		{
//			"errors":[
//			   {
//				"message":"Insufficient balance to complete operation"
//			   }
//			   ],
//			"api":{
//				"ver":"1.0.1"
//			}
//		}
	}
}
