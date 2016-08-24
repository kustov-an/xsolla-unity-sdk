using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Xsolla
{
	public class PaymentListScreenController : ScreenBaseConroller<object> 
	{
		public Text titleText;
		public QuickPaymentsController quickController;
		public AllPaymentsController allController;
		public SavedPayController savedPayController;
		public ImageLoader imageLoader;
		public GameObject screenHider;

		private string startCountryIso;
		private XsollaQuickPayments _quickPayments;
		private XsollaPaymentMethods _paymentMethods;
		private XsollaSavedPaymentMethods _savedPaymetnsMethods;
		private XsollaCountries _countries;
		private int loadingProgress = 0; // 4 - full loaded with savedmethods
		private XsollaUtils utilsLink;

		public override void InitScreen (XsollaTranslations translations, object model)
		{
			throw new System.NotImplementedException ();
		}
	
		public void InitScreen(XsollaUtils utils)
		{
			utilsLink = utils;
			titleText.text = utils.GetTranslations().Get(XsollaTranslations.PAYMENT_METHODS_PAGE_TITLE);
			startCountryIso = utils.GetUser ().GetCountryIso ();
			savedPayController.InitScreen(utilsLink);
			quickController.InitScreen(utilsLink);
			//allController.InitScreen(utilsLink);
		}

		public void SetQuickPayments(XsollaQuickPayments quickPayments)
		{
			_quickPayments = quickPayments;
			LoadElement ();
		}

		public void SetPaymentsMethods(XsollaPaymentMethods paymentMethods)
		{
			_paymentMethods = paymentMethods;
			LoadElement ();
		}

		public void SetCountries(XsollaCountries countries)
		{
			_countries = countries;
			LoadElement ();
//			OpenAllPayments ();
		}

		public void SetSavedPaymentsMethods(XsollaSavedPaymentMethods paymentMethods)
		{
			_savedPaymetnsMethods = paymentMethods;
			LoadElement();
		}

		public void UpdateQuick(XsollaQuickPayments quickPayments)
		{
			_quickPayments = quickPayments;
			quickController.SetQuickMethods (quickPayments);
		}

		public void UpdateRecomended(XsollaPaymentMethods paymentMethods)
		{
			_paymentMethods = paymentMethods;
			quickController.SetAllMethods (paymentMethods);
			allController.UpdatePaymentMethods (paymentMethods);
		}

		public void UpdateSavedMethods(XsollaSavedPaymentMethods paymentMethods)
		{
			_savedPaymetnsMethods = paymentMethods;
			//quickController.SetSavedMethods(paymentMethods);
			//allController.UpdatePaymentMethods(paymentMethods)
		}

		private void InitChildView(){
//			Resizer.ResizeToParrent (gameObject);
 			quickController.SetQuickMethods (_quickPayments);
			quickController.SetAllMethods (_paymentMethods);
			allController.SetPaymentMethods (_paymentMethods);
			if (utilsLink.GetUser ().IsAllowChangeCountry ()) { 
				allController.SetCountries (startCountryIso, _countries);
			}
			savedPayController.SetSavedMethods(_savedPaymetnsMethods);
		}

		public void OpenQuickPayments()
		{
			savedPayController.gameObject.SetActive(false);
			screenHider.SetActive (false);
			allController.gameObject.SetActive(false);
			quickController.gameObject.SetActive(true);
		}

		public void OpenAllPayments()
		{
			savedPayController.gameObject.SetActive(false);
			screenHider.SetActive (false);
			quickController.gameObject.SetActive(false);
			allController.gameObject.SetActive(true);
		}

		public void OpenSavedMethod()
		{
			screenHider.SetActive(true);
			quickController.gameObject.SetActive(false);
			allController.gameObject.SetActive(false);
		}

		private void LoadElement(){
			loadingProgress++;
			if(IsAllLoaded()){
				InitChildView ();
				// if count savedMethods equal 0, we open Auick methods, else we open saved methods 
				if (_savedPaymetnsMethods.GetCount() == 0)
					OpenQuickPayments ();
				else 
					OpenSavedMethod ();

			}
		}

		public bool IsAllLoaded()
		{
			return loadingProgress == 4;
		}

		public void ChoosePaymentMethod(long paymentMethodId)
		{
//			if (_purchase.ContainsKey ("pid"))
//				_purchase ["pid"] = paymentMethodId;
//			else
//				_purchase.Add ("pid", paymentMethodId);
//
//			if (!_purchase.ContainsKey ("hidden"))
//				_purchase.Add ("hidden", "out");
			
			Dictionary<string, object> purchase = new Dictionary<string, object>();
			purchase.Add ("pid", paymentMethodId);
			purchase.Add ("hidden", "out");
			GetComponentInParent<XsollaPaystationController> ().ChoosePaymentMethod (purchase);
		}

		public void ChangeCountry(string countryIso)
		{
			GetComponentInParent<XsollaPaystationController> ().UpdateCountries (countryIso);
		}

		public bool IsQuiqckPayments()
		{
			return _quickPayments != null;
		}

		public bool IsAllPayments()
		{
			return _paymentMethods != null;
		}

		public bool IsSavedPayments()
		{
			return _savedPaymetnsMethods != null;
		}
	}
}
