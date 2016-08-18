using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Xsolla
{
	public class QuickPaymentBtnController: MonoBehaviour
	{
		public Image _iconMethod;
		public Text  _labelMethod;
		public Button _btnMethod;

		private XsollaPaymentMethod _method;

		public void setMethod(XsollaPaymentMethod pMethod)
		{
			_method = pMethod;
		}

		public XsollaPaymentMethod getMethod()
		{
			return _method;
		}

		public void setLable(String pName)
		{
			_labelMethod.text = pName;
		}

		public void Hide()
		{
			CanvasGroup canvas = GetComponent<CanvasGroup>();
			canvas.alpha = 0f;
			canvas.blocksRaycasts = false;
		}

		public void Show()
		{
			CanvasGroup canvas = GetComponent<CanvasGroup>();
			canvas.alpha = 1f;
			canvas.blocksRaycasts = true;
		}
	}
}

