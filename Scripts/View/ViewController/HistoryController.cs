using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Xsolla
{
	public class HistoryController: MonoBehaviour
	{
		public Text mTitle;
		public GameObject mHistoryContainer;
		private const string PREFAB_HISTORY_ROW  = "Prefabs/SimpleView/HistoryItem";
		private int mLimit = 0;
		private int mCountMore = 20;
		public bool isRefresh = false;

//		public Text mDateTitle;
//		public Text mTypeTitle;
//		public Text mItemTitle;
//		public Text mBalanceTitle;
//		public Text mPriceTitle;

		public Button mBtnRefresh;

		public void InitScreen(XsollaTranslations pTranslation, XsollaHistoryList pList)
		{
			mTitle.text = pTranslation.Get("balance_history_page_title");

			AddHistoryRow(pTranslation, null, false, true);

			foreach (XsollaHistoryItem item in pList.GetItemsList())
			{
				AddHistoryRow(pTranslation, item, mLimit%2 != 0, false);
				mLimit ++;
			}

			mBtnRefresh.onClick.AddListener(delegate { OnRefreshHistory(); });
			isRefresh = false;
		}

		private void ClearList()
		{
			mLimit = 0;
			Resizer.DestroyChilds(mHistoryContainer.transform);
			isRefresh = true;
		}

		private void OnRefreshHistory()
		{
			ClearList();
			LoadMore();
		}

		public void AddListRows(XsollaTranslations pTranslation, XsollaHistoryList pList)
		{
			foreach (XsollaHistoryItem item in pList.GetItemsList())
			{
				AddHistoryRow(pTranslation, item, mLimit%2 != 0, false);
				mLimit ++;
			}
		}

		public void AddHistoryRow(XsollaTranslations pTranslation, XsollaHistoryItem pItem, Boolean pEven, Boolean pHeader = false)
		{
			GameObject itemRow = Instantiate(Resources.Load(PREFAB_HISTORY_ROW)) as GameObject;
			HistoryElemController controller = itemRow.GetComponent<HistoryElemController>();
			if (controller != null)
			{
				if (pHeader)
				{
					controller.Init(pTranslation, null, pEven, true);
				}
				else
				{
					controller.Init(pTranslation, pItem, pEven);
				}
			}
			itemRow.transform.SetParent(mHistoryContainer.transform);
		}

		public void OnScrollChange(Vector2 pVector)
		{
			if (pVector == new Vector2(0.0f, 0.0f))
			{
				Logger.Log("End scroll");
				if (!isRefresh)
					LoadMore();
			}
		}

		private void LoadMore()
		{
			Dictionary<string, object> lParams = new Dictionary<string, object>();
			// Load History
			lParams.Add("offset", mLimit);
			lParams.Add("limit", mCountMore);
			lParams.Add("sortDesc", true);
			lParams.Add("sortKey", "dateTimestamp");
			GetComponentInParent<XsollaPaystation> ().LoadHistory(lParams);
		}
	}
}

