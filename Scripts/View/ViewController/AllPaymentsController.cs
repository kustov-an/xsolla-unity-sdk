using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Xsolla
{
	public class AllPaymentsController : MonoBehaviour {

		public PaymentListView paymenListView;
		public GameObject dropDownContainer;
		public DropDownController dropDownController;
		public GameObject back;
		private XsollaCountries _countries;
		private XsollaUtils utilsLink;

		public void SetPaymentMethods(XsollaPaymentMethods paymentMethods){
			paymenListView.SetPaymentMethods(paymentMethods);
			SetUpNavButtons();
		}

		public void InitScreen(XsollaUtils pUtils)
		{
			utilsLink = pUtils;
			back.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.BACK_TO_LIST);
		}

		public void SetCountries(string currentCountryIso, XsollaCountries countries)
		{
			_countries = countries;
			dropDownController.gameObject.SetActive (true);//Show countries dropdown
			dropDownController.SetParentForScroll (gameObject.transform.parent.parent.parent);
			dropDownController.OnItemSelected += (position, title) => {
				OnChangeCountry (position);
			};
			dropDownController.SetData (countries.GetTitleList(), _countries.GetItemByKey(currentCountryIso).name);

		}

		public void UpdatePaymentMethods(XsollaPaymentMethods paymentMethods){
			paymenListView.SetPaymentMethods(paymentMethods);
		}
		public void OnChangeCountry(string key)
		{
			XsollaCountry country = _countries.GetItemByKey(key);
			dropDownContainer.SetActive (false);
			GetComponentInParent<PaymentListScreenController>().ChangeCountry(country.iso);
		}

		public void OnChangeCountry(int position)
		{
			XsollaCountry country = _countries.GetItemsList()[position];
			dropDownContainer.SetActive (false);
			GetComponentInParent<PaymentListScreenController>().ChangeCountry(country.iso);
		}

		public void SetUpNavButtons()
		{
			back.GetComponent<Button>().onClick.AddListener (() => { GetComponentInParent<PaymentListScreenController>().OpenQuickPayments(); });
		}

	}
}
