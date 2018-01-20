using System;
using System.IO;
using System.Text;

namespace AppInfo
{
	public class AppInfoItem
	{
		private uint m_AppID;

		private int m_DataSize;

		private uint m_State;

		private uint m_LastUpdated;

		private ulong m_AccessToken;

		private string m_SHA1 = "";

		private uint m_ChangeNumber;

		public AppInfoItemKey AppInfoKey;

		public uint AppID
		{
			get
			{
				return m_AppID;
			}
			set
			{
                m_AppID = value;
			}
		}

		public int DataSize
		{
			get
			{
				return m_DataSize;
			}
			set
			{
                m_DataSize = value;
			}
		}

		public uint State
		{
			get
			{
				return m_State;
			}
			set
			{
                m_State = value;
			}
		}

		public uint LastUpdated
		{
			get
			{
				return m_LastUpdated;
			}
			set
			{
                m_LastUpdated = value;
			}
		}

		public ulong AccessToken
		{
			get
			{
				return m_AccessToken;
			}
			set
			{
                m_AccessToken = value;
			}
		}

		public string SHA1
		{
			get
			{
				return m_SHA1;
			}
			set
			{
                m_SHA1 = value;
			}
		}

		public uint ChangeNumber
		{
			get
			{
				return m_ChangeNumber;
			}
			set
			{
                m_ChangeNumber = value;
			}
		}

		public AppInfoItem(uint appid, int datasize)
		{
            m_AppID = appid;
            m_DataSize = datasize;
		}

		public void AddKeyValues(BinaryReader reader, AppInfoItemKey AIIK)
		{
			try
			{
				for (byte b = reader.ReadByte(); b != 8; b = reader.ReadByte())
				{
					string name = AppInfoItem.ReadString(reader);
                    AppInfoItemKey appInfoItemKey = new AppInfoItemKey
                    {
                        Name = name,
                        Type = b
                    };
                    string typeDescription;
					switch (b)
					{
					case 1:
					{
						string value = AppInfoItem.ReadString(reader);
						appInfoItemKey.Value = value;
						typeDescription = "String";
						break;
					}
					case 2:
						appInfoItemKey.Value = reader.ReadInt32().ToString();
						typeDescription = "Int32";
						break;
					case 3:
						appInfoItemKey.Value = reader.ReadSingle().ToString();
						typeDescription = "Single";
						break;
					case 4:
						typeDescription = "Pointer";
						break;
					case 5:
					{
						string text = AppInfoItem.ReadWideString(reader);
						appInfoItemKey.Value = text.ToString();
						typeDescription = "WString";
						break;
					}
					case 6:
					{
						byte b2 = reader.ReadByte();
						byte b3 = reader.ReadByte();
						byte b4 = reader.ReadByte();
						appInfoItemKey.Value = string.Concat(new string[]
						{
							b2.ToString(),
							" ",
							b3.ToString(),
							" ",
							b4.ToString()
						});
						typeDescription = "Color";
						break;
					}
					case 7:
						appInfoItemKey.Value = reader.ReadUInt64().ToString();
						typeDescription = "UInt64";
						break;
					default:
						typeDescription = "String";
						break;
					}
					appInfoItemKey.TypeDescription = typeDescription;
					AIIK.keys.Add(appInfoItemKey);
					if (b == 0)
					{
                        AddKeyValues(reader, appInfoItemKey);
					}
				}
			}
			catch
			{
			}
		}

		private static string ReadString(BinaryReader reader)
		{
			byte[] buffer;
			int count;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte value;
				while ((value = reader.ReadByte()) != 0)
				{
					memoryStream.WriteByte(value);
				}
				buffer = memoryStream.GetBuffer();
				count = (int)memoryStream.Length;
			}
			return Encoding.UTF8.GetString(buffer, 0, count).Replace("\v", "\\v");
		}

		private static string ReadWideString(BinaryReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (char c = (char)reader.ReadUInt16(); c != '\0'; c = (char)reader.ReadUInt16())
			{
				if (c == '\v')
				{
					stringBuilder.Append("\\v");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
