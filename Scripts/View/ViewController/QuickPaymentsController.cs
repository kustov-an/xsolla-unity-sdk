using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Xsolla{
	public class QuickPaymentsController : MonoBehaviour {

		public ImageLoader imageLoader;
		public GameObject[] quiсk;// max 2
		public GameObject[] popular;//max 6
		public GameObject quickPanel;
		public GameObject showMore;
		public GameObject back;
		public GameObject title;

		private XsollaUtils utilsLink;
		private int countQuickBtn = 3;


//		private XsollaQuickPayments quickPayments;
//		private XsollaPaymentMethods paymentMethods;

		public void DrawLayout(XsollaPaymentMethods paymentMethods){
		}

		public void InitScreen(XsollaUtils utils)
		{
			utilsLink = utils;
			showMore.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.PAYMENT_LIST_SHOW_MORE);
			back.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.BACK_TO_SPECIALS);
				
		}

		public void SetQuickMethods(XsollaQuickPayments quickPayments)
		{
			if (quickPayments != null)
			{
				List<XsollaPaymentMethod> paymentList = quickPayments.GetItemsList();

				for (int i=0; i < countQuickBtn; i++)
				{
					if (i < paymentList.Count)
					{
						CreateQuickBtn(paymentList[i]);	
					}
					else
					{
						CreateQuickBtn(null);	
					}
				}
			}

			return;

			if (quickPayments != null) {
				List<XsollaPaymentMethod> paymentList = quickPayments.GetItemsList ();
				quiсk [0].SetActive (true);
				quiсk [1].SetActive (true);
				if (paymentList.Count < 2) {
					quiсk [1].SetActive (false);
					if (paymentList [0].id == 2385)
						quiсk [0].SetActive (false);
				}

				if (paymentList.Count == 1) {
					quiсk [0].GetComponentsInChildren<Text> (true) [0].text = paymentList [0].name;
					quiсk [0].GetComponentsInChildren<Button> (true) [0].onClick.AddListener (() => OnChoosePaymentMethod ( paymentList [0].id));
				} else if(paymentList.Count >= 2) {
					for (int i = 1; i < paymentList.Count; i++) {
						XsollaPaymentMethod paymentMethod = paymentList [i];
						quiсk [i - 1].GetComponentsInChildren<Text> (true) [0].text = paymentMethod.name;
						quiсk [i - 1].GetComponentsInChildren<Button> (true) [0].onClick.AddListener (() => OnChoosePaymentMethod (paymentMethod.id));
					}
				}
				quiсk [1].SetActive (false);
			} else {
				quiсk [0].SetActive (false);
				quiсk [1].SetActive (false);
			}
		}

		private void CreateQuickBtn(XsollaPaymentMethod pMethod)
		{
			GameObject quickBtn = Instantiate(Resources.Load("Prefabs/SimpleView/_PaymentFormElements/QuickPaymentBtn")) as GameObject;
			quickBtn.transform.SetParent(quickPanel.transform);
			QuickPaymentBtnController controller = quickBtn.GetComponent<QuickPaymentBtnController>();

			if (pMethod == null)
			{
				controller.Hide();
				return;
			}

			// Set method
			controller.setMethod(pMethod);
			// Set name 
			controller.setLable(pMethod.GetName());
			// Set icon
			//if (!pMethod.imgUrl.Equals(""))
			//	imageLoader.LoadImage(controller._iconMethod, pMethod.GetImageUrl());		
			// Set BtnList
			controller._btnMethod.onClick.AddListener(() => OnChoosePaymentMethod(controller.getMethod().id));
		}

		public void SetAllMethods(XsollaPaymentMethods paymentMethods)
		{
			popular [0].SetActive (false);
			popular [1].SetActive (false);
			popular [2].SetActive (false);
			popular [3].SetActive (false);
			popular [4].SetActive (false);
			popular [5].SetActive (false);
			List<XsollaPaymentMethod> paymentList = paymentMethods.GetRecomendedItems ();
			int count = paymentMethods.GetRecomendedItems ().Count < 6 ? paymentMethods.GetRecomendedItems ().Count : 6;
			for(int i = 0; i < count; i++)
			{
				popular [i].SetActive(true);
				XsollaPaymentMethod paymentMethod =  paymentList[i];
				imageLoader.LoadImage(popular [i].GetComponentsInChildren<Image> (true)[2], paymentMethod.GetImageUrl());
				popular [i].GetComponentsInChildren<Button>(true)[0].onClick.AddListener(() => OnChoosePaymentMethod(paymentMethod.id));
			}
			SetUpNavButtons ();
		}

		public void SetUpNavButtons()
		{
			showMore.GetComponent<Button>().onClick.AddListener (() => { 
				GetComponentInParent<PaymentListScreenController>().OpenAllPayments();
			});
			back.GetComponent<Button>().onClick.AddListener (() => { 
				GetComponentInParent<XsollaPaystationController>().LoadShopPricepoints();
			});
		}

		public void OnChoosePaymentMethod(long paymentMethodId)
		{
			GetComponentInParent<PaymentListScreenController> ().ChoosePaymentMethod (paymentMethodId);
		}

		public void ShowMorePaymentMethod()
		{

		}


		public void Back()
		{

		}

	}
}
