using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SSELauncher
{
	[XmlRoot(ElementName = "Config")]
	[Serializable]
	public class CConfig
	{
		public enum ESortBy
		{
			SortByName,
			SortByDateAdded
		}

		public enum EGroupBy
		{
			GroupByNone,
			GroupByType,
			GroupByCategory
		}

		[XmlElement(ElementName = "AppList")]
		public List<CApp> m_Apps = new List<CApp>();
		public List<string> MasterServerAddress = new List<string>();
		public List<string> BroadcastAddress = new List<string>();
		public List<string> BanList = new List<string>();

        public ESortBy SortBy { get; set; }
		public EGroupBy GroupBy { get; set; }

        public int WindowSizeX { get; set; }
        public int WindowSizeY { get; set; }
        public int WindowPosX { get; set; }
        public int WindowPosY { get; set; }

        public bool HideMissingShortcut { get; set; }
        public bool HideToTray { get; set; }
        public bool ParanoidMode { get; set; }
        public string AvatarPath { get; set; }
        public string PersonaName { get; set; }
        public string SteamIdGeneration { get; set; }
        public long ManualSteamId { get; set; }
        public string Language { get; set; }
        public bool LowViolence { get; set; }
        public bool StorageOnAppdata { get; set; }
        public bool SeparateStorageByName { get; set; }
        public bool AutomaticallyJoinInvite { get; set; }
        public bool EnableHTTP { get; set; }
        public bool EnableInGameVoice { get; set; }
        public bool EnableLobbyFilter { get; set; }
        public bool DisableFriendList { get; set; }
        public bool DisableLeaderboard { get; set; }
        public bool SecuredServer { get; set; }
        public bool EnableVR { get; set; }
        public bool Offline { get; set; }
        public string QuickJoinHotkey { get; set; }
        public bool EnableLog { get; set; }
        public bool CleanLog { get; set; }
        public string MarkLogHotkey { get; set; }
        public string LogFilter { get; set; }
        public bool Minidump { get; set; }
        public int ListenPort { get; set; }
        public int MaximumPort { get; set; }
        public int DiscoveryInterval { get; set; }
        public int MaximumConnection { get; set; }
        public bool AllowAnyoneConnect { get; set; }
        public string AdminPass { get; set; }
        public bool EnableOverlay { get; set; }
        public bool EnableOnlinePlay { get; set; }
        public string OverlayLanguage { get; set; }
        public string OverlayScreenshotHotkey { get; set; }
        public string OnlineKey { get; set; }

        public CConfig()
		{
            LoadDefault(false);
		}

		public void LoadDefault(bool PopulateArray)
		{
            SortBy = CConfig.ESortBy.SortByName;
            GroupBy = CConfig.EGroupBy.GroupByNone;
            WindowSizeX = 0;
            WindowSizeY = 0;
            WindowPosX = -1;
            WindowPosY = -1;
            HideMissingShortcut = false;
            ParanoidMode = false;
            AvatarPath = "avatar.png";
            PersonaName = "AccountName";
            SteamIdGeneration = "GenerateRandom";
            ManualSteamId = 0L;
            Language = "English";
            LowViolence = false;
            StorageOnAppdata = true;
            SeparateStorageByName = false;
            AutomaticallyJoinInvite = true;
            EnableHTTP = false;
            EnableInGameVoice = false;
            EnableLobbyFilter = true;
            DisableFriendList = false;
            DisableLeaderboard = false;
            SecuredServer = true;
            EnableVR = false;
            QuickJoinHotkey = "SHIFT + TAB";
            Offline = false;
            EnableLog = false;
            CleanLog = true;
            MarkLogHotkey = "CTRL + ALT + M";
            LogFilter = "User Logged On";
            Minidump = true;
            ListenPort = 31313;
            MaximumPort = 10;
            DiscoveryInterval = 3;
            MaximumConnection = 200;
            AllowAnyoneConnect = true;
            AdminPass = "";
			if (PopulateArray)
			{
                MasterServerAddress.Add("188.40.40.201:27010");
                BroadcastAddress.Add("255.255.255.255");
			}
            EnableOverlay = true;
            EnableOnlinePlay = true;
            OverlayScreenshotHotkey = "F12";
            OnlineKey = null;
		}
	}
}
