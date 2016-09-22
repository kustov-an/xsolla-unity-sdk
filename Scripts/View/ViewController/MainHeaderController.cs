using System;
using UnityEngine;
using UnityEngine.UI;


namespace Xsolla
{
	public class MainHeaderController: MonoBehaviour
	{
		public Text _titleProj;
		public Button _btnUserProfile;
		public Text _userName;

		private const string PREFAB_DROPDOWN_MENU = "Prefabs/SimpleView/DropdownMenu";

		public void InitScreen(XsollaUtils pUtils)
		{
			// set title 
			_titleProj.text = pUtils.GetProject().name;

			// user name 
			_userName.text = pUtils.GetUser().GetName();

			// set btn click
			_btnUserProfile.onClick.AddListener(delegate { OpenUserProfileMenu();});
		}

		private void OpenUserProfileMenu()
		{
			// get prefab menu
			GameObject dropDownMenu = Instantiate(Resources.Load(PREFAB_DROPDOWN_MENU)) as GameObject;

			// get controller 
			DropDownMenuController controller = dropDownMenu.GetComponent<DropDownMenuController>();

			// Set position
			controller.transform.SetParent(this.transform);
			controller.transform.position = new Vector3(_btnUserProfile.transform.position.x,_btnUserProfile.transform.position.y - 10,_btnUserProfile.transform.position.z);

		}

		public MainHeaderController ()
		{
		}
	}
}

