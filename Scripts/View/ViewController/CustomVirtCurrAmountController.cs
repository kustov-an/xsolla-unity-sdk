using System;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

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

		private string 		mTotalTitle = "";

		public CustomVirtCurrAmountController ()
		{
		}

		public void initScreen(XsollaUtils pUtils)
		{
			// Set btn Name
			btnPay.gameObject.GetComponentInChildren<Text>().text = pUtils.GetTranslations().Get("form_continue");
			mTotalTitle = pUtils.GetTranslations().Get("payment_summary_total");
			ImageLoader imageLoader = FindObjectOfType<ImageLoader>();
			Logger.Log("VirtIcon " + pUtils.GetProject().virtualCurrencyIconUrl);
			imageLoader.LoadImage(iconVirtCurr, "http:" + pUtils.GetProject().virtualCurrencyIconUrl);
			virtCurrAmount.onEndEdit.AddListener(delegate 
				{
					Calculate();
				});

		}

		public void Calculate()
		{
			Logger.Log("Calculate");

		}
			
		public void setValues(CustomAmountCalcRes pValue)
		{
			totalAmountTitle.text = mTotalTitle + " " + CurrencyFormatter.FormatPrice(pValue.currency, pValue.amount.ToString("0.00"));
			virtCurrAmount.text = pValue.vcAmount.ToString();
			realCurrAmount.text = pValue.amount.ToString("0.00");

			if (pValue.currency == "USD")
				iconRealCurr.text = "$";
			else if (pValue.currency == "EUR")
				iconRealCurr.text = "€";
			else if (pValue.currency == "RUB")
				iconRealCurr.text = "";
		}

		public class CustomAmountCalcRes: IParseble
		{
			public float amount;
			public string currency;
			public float vcAmount;
			public Bonus bonus;

			public IParseble Parse(JSONNode pNode)
			{
				amount = pNode["amount"].AsFloat;
				currency = pNode["currency"];
				vcAmount = pNode["vc_amount"].AsFloat;
				bonus = new Bonus().Parse(pNode["bonus"]) as Bonus;
				return this;
			}

			public class Bonus: IParseble
			{
				public IParseble Parse(JSONNode pNode)
				{
					return this;
				}

			}

		}
			

	}
}

