using System;
using System.Collections.Generic;

namespace AppInfo
{
	public class AppInfoItemKey
	{
		private string m_Name = "";

		private byte m_Type;

		private string m_TypeDescription = "";

		private string m_Value = "";

		public List<AppInfoItemKey> keys = new List<AppInfoItemKey>();

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
                m_Name = value;
			}
		}

		public byte Type
		{
			get
			{
				return m_Type;
			}
			set
			{
                m_Type = value;
			}
		}

		public string TypeDescription
		{
			get
			{
				return m_TypeDescription;
			}
			set
			{
                m_TypeDescription = value;
			}
		}

		public string Value
		{
			get
			{
				return m_Value;
			}
			set
			{
                m_Value = value;
			}
		}

		public AppInfoItemKey GetKey(string name, AppInfoItemKey AIIKParent)
		{
			AppInfoItemKey appInfoItemKey = null;
			name = name.ToLower();
			foreach (AppInfoItemKey current in AIIKParent.keys)
			{
				if (current.Name.ToLower() == name)
				{
					appInfoItemKey = current;
					break;
				}
				if (current.keys.Count > 0)
				{
					appInfoItemKey = GetKey(name, current);
				}
				if (appInfoItemKey != null)
				{
					break;
				}
			}
			return appInfoItemKey;
		}

		private string GetValue(string name, AppInfoItemKey AIIKParent)
		{
			string result = "";
			name = name.ToLower();
			foreach (AppInfoItemKey current in AIIKParent.keys)
			{
				if (current.Name.ToLower() == name.ToLower())
				{
					result = current.Value;
					break;
				}
				if (current.keys.Count > 0)
				{
					result = GetValue(name, current);
				}
			}
			return result;
		}

		public string GetKeyValue(string key)
		{
			string result = "";
			AppInfoItemKey key2 = GetKey(key, this);
			if (key2 != null)
			{
				result = key2.Value;
			}
			return result;
		}
	}
}
