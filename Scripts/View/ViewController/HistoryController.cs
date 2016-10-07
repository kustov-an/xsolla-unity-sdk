using System;
using UnityEngine;
using UnityEngine.UI;

namespace Xsolla
{
	public class HistoryController: MonoBehaviour
	{
		public Text mTitle;
		public GameObject mHistroryContainer;
		private const string PREFAB_HISTORY_ROW  = "Prefabs/SimpleView/HistoryItem";

		public Text mDateTitle;
		public Text mTypeTitle;
		public Text mItemTitle;
		public Text mBalanceTitle;
		public Text mPriceTitle;

		public void InitScreen(XsollaTranslations pTranslation, XsollaHistoryList pList)
		{
			mDateTitle.text = pTranslation.Get("balance_history_date");
			mTypeTitle.text = pTranslation.Get("balance_history_purpose");
			mItemTitle.text = pTranslation.Get("balance_history_item");
			mBalanceTitle.text = pTranslation.Get("balance_history_vc_amount");
			mPriceTitle.text = pTranslation.Get("balance_history_payment_amount");
			mTitle.text = pTranslation.Get("balance_history_page_title");

			foreach (XsollaHistoryItem item in pList.GetItemsList())
				AddHistoryRow(pTranslation, item);


		}

		public void AddHistoryRow(XsollaTranslations pTranslation, XsollaHistoryItem pItem)
		{
			GameObject itemRow = Instantiate(Resources.Load(PREFAB_HISTORY_ROW)) as GameObject;
			HistoryElemController controller = itemRow.GetComponent<HistoryElemController>();
			if (controller != null)
				controller.Init(pTranslation, pItem);
			itemRow.transform.SetParent(mHistroryContainer.transform);
		}
			
	}
}

