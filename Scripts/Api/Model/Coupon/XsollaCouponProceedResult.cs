using System;
using System.Collections;
using System.Collections.Generic;

namespace Xsolla
{
	public class XsollaCouponProceedResult: IParseble
	{
		public string _couponCode;
		public long _operationId;
		public string _error;
		public XsollaApi _api;


		public IParseble Parse (SimpleJSON.JSONNode rootNode)
		{
			_couponCode = rootNode["coupon_code"];
			_operationId = rootNode["operation_id"].AsInt;
			_error = rootNode["errors"].AsArray[0]["message"];
			_api = new XsollaApi().Parse(rootNode[XsollaApiConst.R_API]) as XsollaApi;
			return this;
		}
			
		public XsollaCouponProceedResult ()
		{
		}
	}
}

