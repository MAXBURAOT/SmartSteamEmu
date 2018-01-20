using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SSELauncher
{
	[XmlRoot(ElementName = "App")]
	[Serializable]
	public class CApp
	{
		public List<string> BroadcastAddress = new List<string>();

		public List<KVDlc<string, string>> DlcList = new List<KVDlc<string, string>>();

		public List<string> DirectPatchList = new List<string>();

		[XmlIgnore]
		public object Tag
		{
			get;
			set;
		}

		[XmlIgnore]
		public bool HasGameDir
		{
			get;
			set;
		}

		public string IconPath
		{
			get;
			set;
		}

		public string GameName
		{
			get;
			set;
		}

		public string Category
		{
			get;
			set;
		}

		public string Path
		{
			get;
			set;
		}

		public string CommandLine
		{
			get;
			set;
		}

		public string StartIn
		{
			get;
			set;
		}

		public bool Persist
		{
			get;
			set;
		}

		public bool InjectDll
		{
			get;
			set;
		}

		public bool Use64Launcher
		{
			get;
			set;
		}

		public string AvatarPath
		{
			get;
			set;
		}

		public string PersonaName
		{
			get;
			set;
		}

		public int AppId
		{
			get;
			set;
		}

		public string SteamIdGeneration
		{
			get;
			set;
		}

		public long ManualSteamId
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		public int LowViolence
		{
			get;
			set;
		}

		public int StorageOnAppdata
		{
			get;
			set;
		}

		public int SeparateStorageByName
		{
			get;
			set;
		}

		public string RemoteStoragePath
		{
			get;
			set;
		}

		public int AutomaticallyJoinInvite
		{
			get;
			set;
		}

		public int EnableHTTP
		{
			get;
			set;
		}

		public int EnableInGameVoice
		{
			get;
			set;
		}

		public int EnableLobbyFilter
		{
			get;
			set;
		}

		public int DisableFriendList
		{
			get;
			set;
		}

		public int DisableLeaderboard
		{
			get;
			set;
		}

		public int SecuredServer
		{
			get;
			set;
		}

		public int VR
		{
			get;
			set;
		}

		public string QuickJoinHotkey
		{
			get;
			set;
		}

		public bool DisableGC
		{
			get;
			set;
		}

		public int Offline
		{
			get;
			set;
		}

		public string Extras
		{
			get;
			set;
		}

		public bool FailOnNonExistenceStats
		{
			get;
			set;
		}

		public int ListenPort
		{
			get;
			set;
		}

		public int MaximumPort
		{
			get;
			set;
		}

		public int DiscoveryInterval
		{
			get;
			set;
		}

		public int MaximumConnection
		{
			get;
			set;
		}

		public bool DefaultDlcSubscribed
		{
			get;
			set;
		}

        public int EnableDebugLogging { get; set; }
        public int EnableOverlay { get; set; }
        public int EnableOnlinePlay { get; set; }

        public bool EnableHookRefCount { get; set; }

		public string OnlineKey
		{
			get;
			set;
		}

		public static string GetAbsolutePath(string RelPath)
		{
			if (RelPath.Contains(':'))
			{
				return RelPath;
			}
			if (!string.IsNullOrEmpty(RelPath) && RelPath[0] == '\\')
			{
				if (RelPath.Length == 1)
				{
					RelPath = "";
				}
				else
				{
					RelPath = RelPath.Substring(1);
				}
			}
			return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RelPath);
		}

		public static string MakeRelativePath(string AbsPath, bool PrependDirSeparator = true)
		{
			string text = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
			if (text == null)
			{
				text = AppDomain.CurrentDomain.BaseDirectory;
			}
			string text2 = "";
			try
			{
				text2 = AbsPath.Replace(text, "");
			}
			catch
			{
				text2 = AbsPath;
			}
			if (PrependDirSeparator)
			{
				if (!string.IsNullOrEmpty(text2))
				{
					if (!text2.Contains(':') && text2[0] != System.IO.Path.DirectorySeparatorChar)
					{
						text2 = System.IO.Path.DirectorySeparatorChar.ToString() + text2;
					}
				}
				else
				{
					text2 += System.IO.Path.DirectorySeparatorChar.ToString();
				}
			}
			else if (!string.IsNullOrEmpty(text2) && text2[0] == System.IO.Path.DirectorySeparatorChar && text2.Length > 1)
			{
				text2 = text2.Substring(1);
			}
			return text2;
		}

		public string GetIconHash()
		{
			if (string.IsNullOrEmpty(IconPath) || string.IsNullOrWhiteSpace(IconPath))
			{
				return Path;
			}
			return IconPath;
		}

		public CApp()
		{
            ManualSteamId = -1L;
            LowViolence = -1;
            StorageOnAppdata = -1;
            SeparateStorageByName = -1;
            AutomaticallyJoinInvite = -1;
            EnableHTTP = -1;
            EnableInGameVoice = -1;
            EnableLobbyFilter = -1;
            DisableFriendList = -1;
            DisableLeaderboard = -1;
            SecuredServer = -1;
            VR = -1;
            DisableGC = false;
            InjectDll = false;
            Use64Launcher = false;
            Offline = -1;
            FailOnNonExistenceStats = false;
            ListenPort = -1;
            MaximumPort = -1;
            DiscoveryInterval = -1;
            MaximumConnection = -1;
            DefaultDlcSubscribed = true;
            EnableDebugLogging = -1;
            EnableOverlay = -1;
            EnableOnlinePlay = -1;
            EnableHookRefCount = true;
		}

		public CApp(CApp other)
		{
            Copy(other);
		}

		public void Copy(CApp other)
		{
            Tag = other.Tag;
            IconPath = other.IconPath;
            GameName = other.GameName;
            Category = other.Category;
            Path = other.Path;
            CommandLine = other.CommandLine;
            StartIn = other.StartIn;
            Persist = other.Persist;
            InjectDll = other.InjectDll;
            Use64Launcher = other.Use64Launcher;
            AvatarPath = other.AvatarPath;
            PersonaName = other.PersonaName;
            AppId = other.AppId;
            SteamIdGeneration = other.SteamIdGeneration;
            ManualSteamId = other.ManualSteamId;
            Language = other.Language;
            LowViolence = other.LowViolence;
            StorageOnAppdata = other.StorageOnAppdata;
            SeparateStorageByName = other.SeparateStorageByName;
            RemoteStoragePath = other.RemoteStoragePath;
            AutomaticallyJoinInvite = other.AutomaticallyJoinInvite;
            EnableHTTP = other.EnableHTTP;
            EnableInGameVoice = other.EnableInGameVoice;
            EnableLobbyFilter = other.EnableLobbyFilter;
            DisableFriendList = other.DisableFriendList;
            DisableLeaderboard = other.DisableLeaderboard;
            SecuredServer = other.SecuredServer;
            VR = other.VR;
            QuickJoinHotkey = other.QuickJoinHotkey;
            FailOnNonExistenceStats = other.FailOnNonExistenceStats;
            DisableGC = other.DisableGC;
            BroadcastAddress.AddRange(other.BroadcastAddress);
            ListenPort = other.ListenPort;
            MaximumPort = other.MaximumPort;
            DiscoveryInterval = other.DiscoveryInterval;
            MaximumConnection = other.MaximumConnection;
            DefaultDlcSubscribed = other.DefaultDlcSubscribed;
            DlcList.AddRange(other.DlcList);
            EnableDebugLogging = other.EnableDebugLogging;
            EnableOverlay = other.EnableOverlay;
            EnableOnlinePlay = other.EnableOnlinePlay;
            EnableHookRefCount = other.EnableHookRefCount;
            OnlineKey = other.OnlineKey;
		}
	}
}
