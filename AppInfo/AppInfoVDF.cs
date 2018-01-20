using System;
using System.Collections.Generic;
using System.IO;

namespace AppInfo
{
	public class AppInfoVDF
	{
		private uint m_VersionSignature;

		private uint m_Universe;

		public List<AppInfoItem> AppInfoItems = new List<AppInfoItem>();

		public uint VersionSignature
		{
			get
			{
				return m_VersionSignature;
			}
			set
			{
                m_VersionSignature = value;
			}
		}

		public uint Universe
		{
			get
			{
				return m_Universe;
			}
			set
			{
                m_Universe = value;
			}
		}

		public AppInfoItem GetAppInfoItem(ulong AppID)
		{
			AppInfoItem result = null;
			foreach (AppInfoItem current in AppInfoItems)
			{
				if ((ulong)current.AppID == AppID)
				{
					result = current;
					break;
				}
			}
			return result;
		}

		public List<AppInfoItem> GetAppInfoItem(string AppPath, string AppExe)
		{
			List<AppInfoItem> list = new List<AppInfoItem>();
			foreach (AppInfoItem current in AppInfoItems)
			{
				string a = current.AppInfoKey.GetKeyValue("installdir").ToLower();
				AppInfoItemKey key = current.AppInfoKey.GetKey("launch", current.AppInfoKey);
				if (key != null)
				{
					foreach (AppInfoItemKey arg_72_0 in key.keys)
					{
						string a2 = key.GetKeyValue("executable").ToLower();
						if (a == AppPath.ToLower() && (a2 == AppExe.ToLower() || a2 == AppExe.ToLower().Replace("\\", "/")))
						{
							list.Add(current);
							break;
						}
					}
				}
			}
			return list;
		}

		public AppInfoVDF(string appinfofile)
		{
			if (new FileInfo(appinfofile).Exists)
			{
				using (FileStream fileStream = new FileStream(appinfofile, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					BinaryReader binaryReader = new BinaryReader(fileStream);
                    m_VersionSignature = binaryReader.ReadUInt32();
                    m_Universe = binaryReader.ReadUInt32();
					if (m_VersionSignature == 123094054u)
					{
						bool flag = true;
						while (flag)
						{
							uint num = binaryReader.ReadUInt32();
							if (num == 0u)
							{
								flag = false;
							}
							else
							{
								int num2 = binaryReader.ReadInt32();
								byte[] arg_8D_0 = binaryReader.ReadBytes(num2);
								AppInfoItem appInfoItem = new AppInfoItem(num, num2);
								using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(arg_8D_0, false)))
								{
									appInfoItem.State = binaryReader2.ReadUInt32();
									appInfoItem.LastUpdated = binaryReader2.ReadUInt32();
									appInfoItem.AccessToken = binaryReader2.ReadUInt64();
									string text = "";
									for (int i = 0; i < 20; i++)
									{
										text += binaryReader2.ReadByte().ToString();
									}
									appInfoItem.SHA1 = text;
									appInfoItem.ChangeNumber = binaryReader2.ReadUInt32();

                                    var appInfoItemKey = new AppInfoItemKey
                                    {
                                        Name = "App Info Section",
                                        Type = 1
                                    };

                                    bool flag2 = true;
									while (flag2)
									{
										if (binaryReader2.ReadByte() == 0)
										{
											flag2 = false;
										}
										else
										{
											appInfoItem.AddKeyValues(binaryReader2, appInfoItemKey);
										}
									}
									appInfoItem.AppInfoKey = appInfoItemKey;
								}
                                AppInfoItems.Add(appInfoItem);
							}
						}
					}
				}
			}
		}
	}
}
