using System;

namespace Xsolla
{
	public class XsollaStatusPing: IParseble
	{
		//rootNode	{{"status":"canceled", "final":"true", "elapsedTime":"394", "group":"troubled", "api":{"ver":"1.0.1"}}}	SimpleJSON.JSONClass
		private String _status;
		private bool _final;
		private int _elapsedTime;
		private XsollaStatus.Group _group;

		public IParseble Parse (SimpleJSON.JSONNode rootNode)
		{
			if (rootNode != null && !"null".Equals(rootNode.ToString())) 
			{
				this._status = rootNode["status"];
				this._final = rootNode["final"].AsBool;
				this._elapsedTime = rootNode["elapsedTime"].AsInt;
				switch(rootNode["group"])
				{
				case "invoice":
					{
						this._group = XsollaStatus.Group.INVOICE;
						break;
					}
				case "done":
					{
						this._group = XsollaStatus.Group.DONE;
						break;
					}
				case "delivering":
					{
						this._group = XsollaStatus.Group.DELIVERING;
						break;
					}
				case "troubled":
					{
						this._group = XsollaStatus.Group.TROUBLED;
						break;
					}
				default:
					{
						this._group = XsollaStatus.Group.UNKNOWN;
						break;
					}
				} 
			}
			return this;
		}

		public XsollaStatus.Group GetGroup()
		{
			return _group;
		}

		public String GetStatus()
		{
			return _status;
		}

		public bool isFinal()
		{
			return _final;
		}

		public int GetElapsedTiem()
		{
			return _elapsedTime;
		}
				
		public XsollaStatusPing ()
		{
		}
	}
}

