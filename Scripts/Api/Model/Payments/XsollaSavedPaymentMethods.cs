using System;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

namespace Xsolla
{
	public class XsollaSavedPaymentMethods: XsollaObjectsManager<XsollaPaymentMethod>, IParseble 
	{

		private XsollaApi api;

		public object getApi()
		{
			return api;
		}

		public List<XsollaPaymentMethod> GetSortedItems(string s)
		{
			return itemsList.FindAll (delegate(XsollaPaymentMethod xpm) {
				return xpm.name.ToLower().StartsWith (s.ToLower());
			});
		}

		public List<XsollaPaymentMethod> GetRecomendedItems()
		{
			return GetItemsList().FindAll (delegate(XsollaPaymentMethod xpm) {
				return xpm.isHidden == 0 && xpm.isRecommended == 1;
			});//((xpm) => {return xpm.isHidden == 0 && xpm.isRecommended == 1;});

		}

		public IParseble Parse (JSONNode paymentListNode)
		{
			IEnumerator<JSONNode> paymentListEnumerator =  paymentListNode["instances"].Childs.GetEnumerator();
			while(paymentListEnumerator.MoveNext())
			{
				XsollaPaymentMethod method = new XsollaPaymentMethod().Parse(paymentListEnumerator.Current) as XsollaPaymentMethod;
				if(method.id != 64 && method.id != 1738){
					AddItem(method);
				}
			}
			api = new XsollaApi().Parse(paymentListNode["api"]) as XsollaApi;
			return this;
		}
	}

	public class XsollaSavedPaymentMethod : IXsollaObject, IParseble
	{
		private long id {get; private set;}
		private string type {get; private set;}
		private string currency {get; private set;}
		private string name {get; private set;}
		private long pid {get; private set;}
		private string reccurentType {get; private set;}
		private object form {get; private set;} // "form":{"paymentSid":"pDEPBqz5qAoFWnBz"}
		private bool replaced {get; private set;}
		private string psName {get; private set;}
		private string iconSrc {get; private set;}
		private bool isSelected {get; private set;}

		public string GetImageUrl()
		{
			if(iconSrc.StartsWith("https:"))
				return iconSrc;
			else
				return "https:" + iconSrc;
		}

		public string GetKey()
		{
			return id.ToString();
		}

		public string GetName()
		{
			return name;
		}

		public string GetPsName()
		{
			return psName;
		}

		public object GetForm()
		{
			return form;
		}

		public IParseble Parse (JSONNode pJsonNode)
		{
			id = pJsonNode["id"].AsInt;
			type = pJsonNode["type"];
			currency = pJsonNode["currency"];
			name = pJsonNode["name"];
			pid = pJsonNode["pid"].AsInt;
			reccurentType = pJsonNode["reccurentType"];
			// parse form
			//
			replaced = pJsonNode["replaced"].AsBool;
			psName = pJsonNode["psName"];
			iconSrc = pJsonNode["iconSrc"];
			isSelected = pJsonNode["isSelected"].AsBool;
			return this;
		}

		public override string ToString ()
		{
			return string.Format ("[XsollaSavedPaymentMethod: id={0}, type={1}, currency={2}, name={3}, pid={4}, reccurentType={5}, form={6}, replaced={7}, psName={8}, iconSrc={9}, isSelected={10}]", id, type, currency, name, pid, reccurentType, form, replaced, psName, iconSrc, isSelected);
		}
	}


}

