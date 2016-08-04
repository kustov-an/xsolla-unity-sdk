using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Xsolla
{
	public class SavedMethodBtnController: MonoBehaviour
	{
		public Image _iconMethod;
		public Text _nameMethod;
		public Text _nameType;
		public Button _btnMethod;

		private XsollaSavedPaymentMethod _method;

		public void setMethod (XsollaSavedPaymentMethod pMethod)
		{
			_method = pMethod;
		}

		public XsollaSavedPaymentMethod getMethod()
		{
			return _method;
		}
	}
}

