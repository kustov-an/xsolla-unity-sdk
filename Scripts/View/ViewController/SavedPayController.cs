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
		public GameObject methodsGrid;
		public Button showMore;
		public Button back;
		public GameObject self;

		public void Start()
		{
			//SetSavedMethods(null);
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

			}
			else
			{
				// if methods list is empty, we hide all window
				self.SetActive(false);
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
			// Set icon
			imageLoader.LoadImage(controller._iconMethod, pMethod.GetImageUrl());		
			// Set BtnList
			controller._btnMethod.onClick.AddListener(() => onMethodClick(controller.getMethod()));
		}

		private void onMethodClick(XsollaSavedPaymentMethod pMethod)
		{
			Logger.Log(pMethod.GetName());
		}

	}
}

