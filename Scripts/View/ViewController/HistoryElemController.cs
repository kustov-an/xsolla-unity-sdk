using System;
using UnityEngine;
using UnityEngine.UI;

namespace Xsolla
{
	public class HistoryElemController: MonoBehaviour
	{
		public Text mDate;
		public Text mType;
		public Text mItem;
		public Text mBalance;
		public Text mPrice;

		public void Init(XsollaHistoryItem pItem)
		{
			mDate.text = pItem.date.ToShortDateString();
			mType.text = "";//
			if (pItem.virtualItems.items.GetCount() != 0)
				mItem.text = pItem.virtualItems.items.GetItemByPosition(0).GetName();

			mBalance.text = pItem.vcAmount + "\n" + "=" + pItem.userBalance;
			mPrice.text = pItem.paymentAmount.ToString();
		}
	}
}

