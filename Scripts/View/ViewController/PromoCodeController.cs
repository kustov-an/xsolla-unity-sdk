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

		public GameObject _acceptBlock;
		public Text 	  _acceptLable;


        public void InitScreen(XsollaTranslations pTranslation)
        {
			_promoBtn.GetComponent<Text>().text = pTranslation.Get("coupon_control_header");
			_promoDesc.text = pTranslation.Get("coupon_control_hint");
			_promoCodeApply.gameObject.GetComponentInChildren<Text>().text = pTranslation.Get("coupon_control_apply");
			_acceptLable.text = pTranslation.Get("coupon_control_accepted");
        }

        public void btnClick()
    	{
        	_promoContainerInputApplyCode.SetActive(!_promoContainerInputApplyCode.activeSelf);
		}

		public void ApplySuccessful()
		{
			_inputField.interactable = false;
			_promoCodeApply.gameObject.SetActive(false);
			_acceptBlock.SetActive(true);
		}

		public PromoCodeController ()
		{
		}
	}
}

