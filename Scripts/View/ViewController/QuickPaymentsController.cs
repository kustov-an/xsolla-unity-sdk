using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Xsolla{
	public class QuickPaymentsController : MonoBehaviour {

		public ImageLoader imageLoader;
		//public GameObject[] quiсk;// max 2
		public GameObject[] popular;//max 6
		public GameObject quickPanel;
		public GameObject recPanel;
		public GameObject showMore;
		public GameObject back;
		public GameObject title;

		private XsollaUtils utilsLink;
		private int countQuickBtn = 3;
		private int countPopBtn = 6;

		public void DrawLayout(XsollaPaymentMethods paymentMethods){
		}

		public void InitScreen(XsollaUtils utils)
		{
			utilsLink = utils;
			showMore.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.PAYMENT_LIST_SHOW_MORE);
			back.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.BACK_TO_SPECIALS);
		}

		public void SetQuickMethods(List<XsollaPaymentMethod> pQuickPayments)
		{
			if (pQuickPayments != null)
			{
				List<XsollaPaymentMethod> paymentList = pQuickPayments;

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
			controller.setIcon(pMethod.id);
			controller._btnMethod.onClick.AddListener(() => OnChoosePaymentMethod(controller.getMethod().id));
		}

		private void CreatePopularBtn(XsollaPaymentMethod pMethod)
		{
			GameObject popularBtn = Instantiate(Resources.Load("Prefabs/SimpleView/_PaymentFormElements/ShopPaymentBtn")) as GameObject;
			popularBtn.transform.SetParent(recPanel.transform);
			ShopPaymentBtnController controller = popularBtn.GetComponent<ShopPaymentBtnController>();
			// Set method
			controller.setMethod(pMethod);
			// Set icon
			if (pMethod.GetImageUrl() != "")
				imageLoader.LoadImage(controller._icon, pMethod.GetImageUrl());
			controller._btn.onClick.AddListener(() => OnChoosePaymentMethod(controller.getMethod().id));
		}

		public void SetAllMethods(List<XsollaPaymentMethod> paymentMethods)
		{
			for (int i = 0; i < countPopBtn; i++)
			{
				CreatePopularBtn(paymentMethods[i]);
			}
			SetUpNavButtons ();
			return;

//			popular [0].SetActive (false);
//			popular [1].SetActive (false);
//			popular [2].SetActive (false);
//			popular [3].SetActive (false);
//			popular [4].SetActive (false);
//			popular [5].SetActive (false);
//			List<XsollaPaymentMethod> paymentList = paymentMethods;
//			int count = paymentMethods.GetRecomendedItems ().Count < 6 ? paymentMethods.GetRecomendedItems ().Count : 6;
//			for(int i = 0; i < count; i++)
//			{
//				popular [i].SetActive(true);
//				XsollaPaymentMethod paymentMethod =  paymentList[i];
//				imageLoader.LoadImage(popular [i].GetComponentsInChildren<Image> (true)[2], paymentMethod.GetImageUrl());
//				popular [i].GetComponentsInChildren<Button>(true)[0].onClick.AddListener(() => OnChoosePaymentMethod(paymentMethod.id));
//			}
//			SetUpNavButtons ();
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
