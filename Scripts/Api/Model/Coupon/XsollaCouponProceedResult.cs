using SimpleJSON;

namespace Xsolla
{
	public class XsollaCouponProceedResult: IParseble
	{
		public string _couponCode;
		public int _operationId;
		public string _error;
		public XsollaApi _api;


		public IParseble Parse(JSONNode rootNode)
		{
			_couponCode = rootNode["coupon_code"];
			_operationId = rootNode["operation_id"].AsInt;
			JSONNode node = rootNode["errors"].AsArray[0];
            _error = node["message"];
			_api = new XsollaApi().Parse(rootNode[XsollaApiConst.R_API]) as XsollaApi;
			return this;
		}

		public XsollaCouponProceedResult()
		{
		}
	}
}

