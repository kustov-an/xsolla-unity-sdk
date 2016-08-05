using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Xsolla
{
	public class SavedPayController : MonoBehaviour
	{
		public ImageLoader imageLoader;
		public GameObject title;
		public GameObject methodsGrid;
		public GameObject showQuickPaymentMethods;
		public GameObject back;
		public GameObject self;

		private XsollaUtils utilsLink;

		public void InitScreen(XsollaUtils utils)
		{
			utilsLink = utils;
			title.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.SAVEDMETHOD_PAGE_TITLE);
			showQuickPaymentMethods.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.PAYMENT_LIST_SHOW_QUICK);
			back.GetComponent<Text>().text = utilsLink.GetTranslations().Get(XsollaTranslations.BACK_TO_SPECIALS);
		}

		public void SetSavedMethods(XsollaSavedPaymentMethods pMethods)
		{
			if (pMethods != null)
			{
				List<XsollaSavedPaymentMethod> paymentList = pMethods.GetItemList();
				// For each method we create btn
				foreach(XsollaSavedPaymentMethod method in paymentList)
				{
					CreateMethodBtn(method);
				}
				self.gameObject.SetActive(true);
				SetUpNavButtons();
			}
			else
			{
				// if methods list is empty, we hide all window
				self.gameObject.SetActive(false);
			}
		}

		private void CreateMethodBtn(XsollaSavedPaymentMethod pMethod)
		{
			// Create object
			GameObject methodBtn = Instantiate(Resources.Load("Prefabs/SimpleView/_PaymentFormElements/SavedMethodBtn")) as GameObject;
			methodBtn.transform.SetParent(methodsGrid.transform);
			SavedMethodBtnController controller = methodBtn.GetComponent<SavedMethodBtnController>();

			// Set method
			controller.setMethod(pMethod);
			// Set name 
			controller._nameMethod.text = pMethod.GetName();
			// Set Type
			controller._nameType.text = pMethod.GetPsName();
			// Set icon
			imageLoader.LoadImage(controller._iconMethod, pMethod.GetImageUrl());		
			// Set BtnList
			controller._btnMethod.onClick.AddListener(() => onMethodClick(controller.getMethod()));
		}

		private void onMethodClick(XsollaSavedPaymentMethod pMethod)
		{
			Dictionary<string, object> purchase = new Dictionary<string, object>();
//			access_token:DGgexDZvknZb32PEnlmCe3yQrL1T5mL1
			//purchase.Add("access_token", utilsLink.GetAcceessToken());
//			saved_method_id:1802757
			purchase.Add("saved_method_id", pMethod.GetKey());
//			pid:1380
			purchase.Add("pid", pMethod.GetPid());
//			paymentWithSavedMethod:1
			purchase.Add("paymentWithSavedMethod", 1);
//			paymentSid:HSUTivuAzJ4Sx85w
			purchase.Add("paymentSid", pMethod.GetFormSid());

//			userInitialCurrency:USD
			purchase.Add("userInitialCurrency", pMethod.GetCurrency());
//			returnUrl:https://secure.xsolla.com/paystation3/#/desktop/return/?access_token=DGgexDZvknZb32PEnlmCe3yQrL1T5mL1&preferences=eyJ1c2VySW5pdGlhbEN1cnJlbmN5IjoiVVNEIiwic2t1WzFdIjoxfQ--&sessional=eyJoaXN0b3J5IjpbWyJ2aXJ0dWFsaXRlbSIsdHJ1ZV0sWyJzYXZlZG1ldGhvZCIsdHJ1ZV1dfQ--&additional=eyJzYXZlZF9tZXRob2RfaWQiOjE4MDI3NTcsInBpZCI6MTM4MCwicGF5bWVudFdpdGhTYXZlZE1ldGhvZCI6MSwicGF5bWVudFNpZCI6IlYzSWNibk9PSkdaWTA5NmUifQ--
//			sku[1]:1
//			ga_client_id:14912095.1466420900
//			ps_custom_data:{"cd19":null}
			GetComponentInParent<XsollaPaystationController> ().ChoosePaymentMethod (purchase);
		}
			
		public void SetUpNavButtons()
		{
			showQuickPaymentMethods.GetComponent<Button>().onClick.AddListener (() => { 
				GetComponentInParent<PaymentListScreenController>().OpenQuickPayments();
			});
			back.GetComponent<Button>().onClick.AddListener (() => { 
				GetComponentInParent<XsollaPaystationController>().LoadShopPricepoints();
			});
		}

	}
}

