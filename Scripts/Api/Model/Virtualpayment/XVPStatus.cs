using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

namespace Xsolla {
	public class XVPStatus : IParseble {
	//	{
	//		"operation_id":"36786976",
	//		"operation_type":"purchase",
	//		"operation_created":"2016-04-08T13:37:24+03:00",
	//		"finance":{
	//			"total":{
	//				"vc_amount":100
	//			}
	//		},
	//		"purchase":{
	//			"virtual_items":[
	//			   {
	//				"name":"Test product 007",
	//				"quantity":1,
	//				"description":"Test product 007",
	//				"image_url":"\/\/cdn3.xsolla.com\/img\/misc\/merchant\/default-item.png"
	//			   }
	//			   ],
	//			"virtual_currency":[
	//			   ],
	//			"subscriptions":[
	//			   ]
	//		},
	//		"status":{
	//			"code":"done",
	//			"description":"Purchase successful!",
	//			"header":"Purchase completed. Thank you for your order!",
	//			"header_description":""
	//		},
	//		"back_url":null,
	//		"return_region":null,
	//		"back_url_caption":null,
	//		"api":{
	//			"ver":"1.0.1"
	//		}
	//	}

		public string OperationId { get; private set; }
		public string OperationType { get; private set; }
		public string OperationCreated { get; private set; }
		public string VcAmount { get; private set; }
		public string BackUrl { get; private set; }
		public string ReturnTegion { get; private set; }
		public string BackUrlCaption { get; private set; }

		public XStatus 			 Status { get; private set; }
		public List<SimpleVItem> Items { get; private set; }
		public List<SimpleVCur>  vCurr { get; private set; }

		public XVPStatus() {
			Items = new List<SimpleVItem>();
			vCurr = new List<SimpleVCur>();
		}

		public XsollaStatus.Group GetGroup()
		{
			switch(Status.Code)
			{
				case "invoice":
					return XsollaStatus.Group.INVOICE;
				case "done":
					return XsollaStatus.Group.DONE;
				case "delivering":
					return XsollaStatus.Group.DELIVERING;
				case "trobled":
					return XsollaStatus.Group.TROUBLED;
				default:
					return XsollaStatus.Group.UNKNOWN;
			}
		}

		public string GetPurchase(int i) {
			if (Items.Count > 0)
			{
				SimpleVItem item = Items[i];
				return item.Quantity + " x " + item.Name;
			}
			else
			{
				return "";
			}
		}

		public string GetVCPurchase(int i)
		{
			if (vCurr.Count > 0)
			{
				SimpleVCur item = vCurr[i];
				return item.vcAmount;
			}
			else
			{
				return "";
			}
		}

		public IParseble Parse (JSONNode rootNode)
		{
			OperationId 		= rootNode["operation_id"].Value;
			OperationType 		= rootNode["operation_type"].Value;
			OperationCreated 	= rootNode["operation_created"].Value;
			VcAmount 			= rootNode["finance"]["total"]["vc_amount"].Value;
			BackUrl 			= rootNode["back_url"].Value;
			ReturnTegion 		= rootNode["return_region"].Value;
			BackUrlCaption 		= rootNode["back_url_caption"].Value;
			JSONNode statusNode = rootNode["status"];
			if(statusNode != null) {
				Status = new XStatus(statusNode["code"].Value, 
				                     statusNode["description"].Value, 
				                     statusNode["header"].Value, 
				                     statusNode["header_description"].Value);
			}

			IEnumerator<JSONNode> iEnumerator;
			JSONArray jarrItems 	= rootNode["purchase"]["virtual_items"].AsArray;
			iEnumerator = jarrItems.Childs.GetEnumerator();
			while (iEnumerator.MoveNext()) {
				JSONNode itemNode = iEnumerator.Current;
				SimpleVItem newItem = new SimpleVItem(itemNode["name"].Value, 
				                                      itemNode["description"].Value,
				                                      itemNode["image_url"].Value, 
				                                      itemNode["quantity"].AsInt);
				Items.Add(newItem);
			}

			JSONArray jarrVC 	= rootNode["purchase"]["virtual_currency"].AsArray;
			iEnumerator = jarrVC.Childs.GetEnumerator();
			while (iEnumerator.MoveNext()) {
				JSONNode itemNode = iEnumerator.Current;
				SimpleVCur newItem = new SimpleVCur(itemNode["vc_amount"].Value);
				vCurr.Add(newItem);
			}

			return this;
		}

		public struct SimpleVItem {
			public string 	Name{ get; private set; }
			public string 	Description{ get; private set; }
			public string 	ImageUrl{ get; private set; }		
			public int 		Quantity{ get; private set; }	

			public string GetImage() {
				return this.ImageUrl.StartsWith("http") ? ImageUrl : "https:" + ImageUrl;
			}

			public SimpleVItem(string name, string description, string imageUrl, int quantity) {
				this.Name 		 = name;
				this.Description = description;
				this.ImageUrl 	 = imageUrl;
				this.Quantity 	 = quantity;
			}

			public override string ToString ()
			{
				return string.Format ("[SimpleVItem: name={0}, imageUrl={1}, quantity={2}]", Name, ImageUrl, Quantity);
			}
			
		} 

		public struct SimpleVCur
		{
			public string vcAmount;

			public SimpleVCur(string pVcAmount)
			{
				this.vcAmount = pVcAmount;
			}
		}


		public struct XStatus {
			public string Code { get; private set; }
			public string Description { get; private set; }
			public string Header { get; private set; }
			public string HeaderDescription { get; private set; }

			public XStatus (string code, string description, string header, string headerDescription)
			{
				this.Code = code;
				this.Description = description;
				this.Header = header;
				this.HeaderDescription = headerDescription;
			}
			
		}

		public override string ToString ()
	{
		return string.Format ("[XVPStatus: OperationId={0}, OperationType={1}, OperationCreated={2}, VcAmount={3}, BackUrl={4}, ReturnTegion={5}, BackUrlCaption={6}, Status={7}, Items={8}]", OperationId, OperationType, OperationCreated, VcAmount, BackUrl, ReturnTegion, BackUrlCaption, Status, Items);
	}
	


	}
}