using System;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

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
		private string 		mCustomCurrency = "";

		public CustomVirtCurrAmountController ()
		{
		}

		public void initScreen(XsollaUtils pUtils, string pCustomCurrency, Action<Dictionary<string, object>> pActionCalc, Action<float> pTryPay)
		{
			// Set btn Name
			btnPay.gameObject.GetComponentInChildren<Text>().text = pUtils.GetTranslations().Get("form_continue");
			mTotalTitle = pUtils.GetTranslations().Get("payment_summary_total");
			mCustomCurrency = pCustomCurrency;
			ImageLoader imageLoader = FindObjectOfType<ImageLoader>();
			Logger.Log("VirtIcon " + pUtils.GetProject().virtualCurrencyIconUrl);
			imageLoader.LoadImage(iconVirtCurr, "http:" + pUtils.GetProject().virtualCurrencyIconUrl);
			virtCurrAmount.onEndEdit.AddListener(delegate 
				{
					pActionCalc(GetParamsForCalc());
				});

			btnPay.onClick.AddListener(delegate  
				{
					pTryPay(GetOutAmount());
				});
		}

		private float GetOutAmount()
		{
			float res = 0;
			string value = virtCurrAmount.text;

			float.TryParse(value, out res);
			return res;
		}

		private Dictionary<string, object> GetParamsForCalc()
		{
			Dictionary<string, object> res = new Dictionary<string, object>();
			res.Add("userInitialCurrency", "");
			res.Add("custom_currency", mCustomCurrency);
			res.Add("custom_vc_amount", virtCurrAmount.text);
			return res;
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

			if (pValue.vcAmount > 0)
				btnPay.interactable = true;
			else
				btnPay.interactable = false;

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

