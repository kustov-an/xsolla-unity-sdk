﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Xsolla
{
	public class MainHeaderController: MonoBehaviour
	{
		public Text _titleProj;
		public GameObject _btnDropDownObj;
		public Text _userName;
		private const string PREFAB_VIEW_MENU_ITEM_EMPTY = "Prefabs/SimpleView/ProfileBtn";

		public void InitScreen(XsollaUtils pUtils)
		{
			// set title 
			_titleProj.text = pUtils.GetProject().name;

			// user name 
			_userName.text = pUtils.GetUser().GetName();
		
			if (pUtils.GetUser().virtualCurrencyBalance != null)
			{
				GameObject obj = Instantiate(Resources.Load(PREFAB_VIEW_MENU_ITEM_EMPTY)) as GameObject;
				UserProfileBtnController controller = obj.GetComponentInChildren<UserProfileBtnController>();
				controller.InitScreen("History", ShowHistory);
				obj.transform.SetParent(_btnDropDownObj.transform);
			}
		}

		public void ShowHistory()
		{
			Logger.Log("Show user history");
			Dictionary<string, object> lParams = new Dictionary<string, object>();
			// Load History
			lParams.Add("offset", 0);
			lParams.Add("limit", 20);
			lParams.Add("sortDesc", true);
			lParams.Add("sortKey", "dateTimestamp");
			GetComponentInParent<XsollaPaystation> ().LoadHistory(lParams);
		}

		public MainHeaderController ()
		{
		}
	}
}

