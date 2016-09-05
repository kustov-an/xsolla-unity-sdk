using System;
using UnityEngine.UI;
using UnityEngine;

namespace Xsolla
{
	public class PromoCodeController: MonoBehaviour
	{

		public GameObject _promoBtn;
		public GameObject _promoContainerInputApplyCode;
		public Text 	  _promoDesc;
		public Button     _promoCodeApply;

		public void btnClick()
		{
			_promoContainerInputApplyCode.SetActive(!_promoContainerInputApplyCode.activeSelf);
		}

		public void show()
		{
			this.gameObject.SetActive(true);
		}

		public void hide()
		{
			this.gameObject.SetActive(false);
		}

		public PromoCodeController ()
		{
		}
	}
}

