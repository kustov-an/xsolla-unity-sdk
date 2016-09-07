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
        public InputField _inputField;


        public void InitScreen(XsollaUtils pUtils)
        {
            _promoBtn.GetComponent<Text>().text = pUtils.GetTranslations().Get("coupon_control_header");
            _promoDesc.text = pUtils.GetTranslations().Get("coupon_control_hint");
            _promoCodeApply.gameObject.GetComponentInChildren<Text>().text = pUtils.GetTranslations().Get("coupon_control_apply");
            //pUtils.GetTranslations();
        }

        public void btnClick()
    {
        _promoContainerInputApplyCode.SetActive(!_promoContainerInputApplyCode.activeSelf);
		}

		public PromoCodeController ()
		{
		}
	}
}

