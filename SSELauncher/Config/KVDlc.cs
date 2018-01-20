using System;
using System.Xml.Serialization;

namespace SSELauncher
{
	[XmlType(TypeName = "Dlc")]
	[Serializable]
	public struct KVDlc<K, V>
	{
		public K DlcId
		{
			get;
			set;
		}

		public V DlcName
		{
			get;
			set;
		}

		public bool Disabled
		{
			get;
			set;
		}

		public KVDlc(K k, V v, bool disabled = false)
		{
			this = default(KVDlc<K, V>);
            DlcId = k;
            DlcName = v;
            Disabled = disabled;
		}
	}
}
