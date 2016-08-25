using System;
using UnityEngine;
using UnityEngine.UI;

namespace Xsolla
{
	public class ShopPaymentBtnController: MonoBehaviour
	{
		public Button _btn;
		public Image  _icon;
		public GameObject _self;

		private XsollaPaymentMethod _method;

		public void setMethod(XsollaPaymentMethod pMethod)
		{
			_method = pMethod;
		}

		public XsollaPaymentMethod getMethod()
		{
			return _method;
		}

		public ShopPaymentBtnController ()
		{
		}


	}
}

