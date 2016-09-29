using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Xsolla
{
	public class MainHeaderController: MonoBehaviour
	{
		public Text _titleProj;
		public UserProfileController _btnUserProfile;
		public Text _userName;

		public void InitScreen(XsollaUtils pUtils)
		{
			// set title 
			_titleProj.text = pUtils.GetProject().name;

			// user name 
			_userName.text = pUtils.GetUser().GetName();
		}

		public void ShowHistory()
		{
			Logger.Log("Show user history");
			Dictionary<string, object> lParams = new Dictionary<string, object>();
			// Load History
			lParams.Add("offset", 0);
			lParams.Add("limit", 100);
			lParams.Add("sortDesc", true);
			lParams.Add("sortKey", "dateTimestamp");
			GetComponentInParent<XsollaPaystation> ().LoadHistory(lParams);
		}

		public MainHeaderController ()
		{
		}
	}
}

