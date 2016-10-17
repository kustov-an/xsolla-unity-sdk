using System;
using UnityEngine;
using UnityEngine.UI;

namespace Xsolla
{
	public class CustomVirtCurrAmountController: MonoBehaviour
	{
		public InputField 	virtCurrAmount;
		public InputField 	realCurrAmount;
		public Button 		btnPay;
		public Text 		totalAmountTitle;

		public Image		iconVirtCurr;
		public Text			iconRealCurr;		

		public CustomVirtCurrAmountController ()
		{
		}

		public void initScreen(XsollaUtils pUtils)
		{
			// TODO: release method
		}
	}
}

