using UnityEngine;
using System.Collections;
using Xsolla;

public class Selfdestruction : MonoBehaviour {

	public void DestroyRoot(){
		XsollaPaystationController controller = gameObject.GetComponentInParent<XsollaPaystationController> ();
		Destroy (controller.gameObject);

		// delete HttpRequst 
		HttpTlsRequest[] listObj = (HttpTlsRequest[])FindObjectsOfType(typeof(HttpTlsRequest));
		foreach(HttpTlsRequest item in listObj)
			Destroy(item.gameObject);

		// delete Xsolla.StyleManager
		StyleManager[] listObjStyles = (StyleManager[])FindObjectsOfType(typeof(StyleManager));
		foreach(StyleManager item in listObjStyles)
			Destroy(item.gameObject);

		TransactionHelper.Clear ();
	}

	public void Selfdestroy(){
		Destroy (gameObject);
	}

	public void DestroyObject(GameObject go){
		Destroy (go);
	}
}
