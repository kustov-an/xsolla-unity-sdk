using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

namespace Xsolla {

	public class XVirtualPaymentSummary : IParseble {
//		{
//			"finance":{
//				"total_without_discount":{
//					"vc_amount":100
//				},
//				"total":{
//					"vc_amount":100
//				}
//			},
//			"purchase":{
//				"virtual_items":[
//				   {
//					"name":"Test product 007",
//					"image_url":"\/\/cdn3.xsolla.com\/img\/misc\/merchant\/default-item.png",
//					"quantity":1
//				   }
//				   ]
//			},
//			"skip_confirmation":false,
//			"api":{
//				"ver":"1.0.1"
//			}
//		}
		public int 				TotalWithoutDiscount{ get; private set; }
		public int 				Total{ get; private set; }

		public bool 				IsSkipConfirmation{ get; private set; }
		
		public List<SimpleVItem> 	Items { get; private set; }

		public XVirtualPaymentSummary() {
			Items = new List<SimpleVItem>();
		}

		public IParseble Parse(JSONNode rootNode) {
			TotalWithoutDiscount 	= rootNode["finance"]["total_without_discount"]["vc_amount"].AsInt;
			Total 					= rootNode["finance"]["total"]["vc_amount"].AsInt;
			IsSkipConfirmation 		= rootNode["skip_confirmation"].AsBool;
			JSONArray jarrItems 	= rootNode["purchase"]["virtual_items"].AsArray;
			IEnumerator<JSONNode> iEnumerator = jarrItems.Childs.GetEnumerator();
			while (iEnumerator.MoveNext()) {
				JSONNode itemNode = iEnumerator.Current;
				SimpleVItem newItem = new SimpleVItem(itemNode["name"].Value, 
				                                   itemNode["image_url"].Value, 
				                                   itemNode["quantity"].AsInt);
				Items.Add(newItem);
			}
			return this;
		}
			
		public struct SimpleVItem {
			public String 	Name{ get; private set; }
			public String 	ImageUrl{ get; private set; }	
			public int 		Quantity{ get; private set; }	

			public String GetImage() {
				return this.ImageUrl.StartsWith("http") ? ImageUrl : "https:" + ImageUrl;
			}

			public SimpleVItem(string name, string imageUrl, int quantity) {
				this.Name 		= name;
				this.ImageUrl 	= imageUrl;
				this.Quantity 	= quantity;
			}

			public override string ToString ()
			{
				return string.Format ("[SimpleVItem: name={0}, imageUrl={1}, quantity={2}]", Name, ImageUrl, Quantity);
			}
			
		} 

		public override string ToString ()
		{
			return string.Format ("[XVirtualPaymentSummary: totalWithoutDiscount={0}, total={1}, isSkipConfirmation={2}, items={3}]", TotalWithoutDiscount, Total, IsSkipConfirmation, Items);
		}
		

	}


}
