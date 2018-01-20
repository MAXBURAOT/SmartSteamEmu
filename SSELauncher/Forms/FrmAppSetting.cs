using Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using vbAccelerator.Components.Shell;

namespace SSELauncher
{
    public class FrmAppSetting : Form
	{
		private CApp m_App;
		private List<KVDlc<string, string>> m_TempDlcList = new List<KVDlc<string, string>>();
		private BackgroundWorker m_bwVerifyer = new BackgroundWorker();
		private string m_LastAppId;
		private bool IsApiModified;

		private TabPage tabDlc;
        private TabPage tabEmu;
        private TabPage tabNet;
        private TabPage tabExtra;
		private TabPage tabDp;

		public List<string> CategoryList;

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TextBox txtAppFolder;

		private Label label1;

		private TextBox txtAppPath;

		private Label label2;

		private Label label4;

		private Label label3;

		private TabPage tabPage2;

		private TextBox txtAppId;

		private TextBox txtAppIcon;

		private Label label5;

		private TextBox txtAppName;

		private Button btnOK;

		private Button btnCancel;

		private Label label6;

		private TextBox txtAppParam;

		private Label lblApiStatusDesc;

		private Label lblSteamApiStatus86;

		private Button btnCreateShortcut;

		private TabPage tabPage3;

		private Label label9;

		private ComboBox cmbEmuSteamId;

		private Label label10;

		private ComboBox cmbPersonaName;

		private Label label11;

		private ComboBox cmbAutoJoinInvite;

		private Label label13;

		private ComboBox cmbStorageOnAppData;

		private Label label14;

		private ComboBox cmbSeparateStorageByName;

		private Label label15;

		private ComboBox cmbRemoteStoragePath;

		private Label label16;

		private ComboBox cmbEnableHTTP;

		private Label label17;

		private ComboBox cmbEnableIngameVoice;

		private Label label18;

		private ComboBox cmbEnableLobbyFilter;

		private Label label19;

		private ComboBox cmbEnableVR;

		private Label label20;

		private ComboBox cmbSecuredServer;

		private Label label21;

		private ComboBox cmbDisableFriendList;

		private Label label22;

		private ComboBox cmbQuickJoinHotkey;

		private TabPage tabPage4;

		private Label label23;

		private ComboBox cmbNetUseGlobal;

		private Label label24;

		private Label label25;

		private Label label26;

		private NumericUpDown numNetDiscoverInterval;

		private NumericUpDown numNetMaxPort;

		private NumericUpDown numNetPort;

		private Label label27;

		private ListBox lstNetBroadcast;

		private Button btnNetDel;

		private Button btnNetAdd;

		private TextBox txtNetIp;

		private TextBox txtDlcAppId;

		private Label label28;

		private ListBox lstDlc;

		private Label label29;

		private ComboBox cmbDlcName;

		private Button btnAddDlc;

		private Button btnDelDlc;

		private CheckBox chkDlcSubsDefault;

		private ToolTip toolTip1;

		private Button btnBrowseIcon;

		private Button btnBrowseFolder;

		private Button btnBrowseExe;

		private Label label31;

		private ComboBox cmbDisableLeaderboard;

		private Label label32;

		private ComboBox cmbFailOnNonStats;

		private CheckBox chkNonSteam;

		private Label label33;

		private ComboBox cmbCategory;

		private ComboBox cmbAppPersist;

		private ComboBox cmbAppLV;

		private ComboBox cmbAppLang;

		private Label label30;

		private Label label12;

		private Label label8;

		private PictureBox pb86;

		private PictureBox pb64;

		private Label lblSteamApiStatus64;

		private Label lblApiModified;

		private Label label7;

		private ComboBox cmbDisableGC;

		private CheckBox chkInject;

		private NumericUpDown numNetMaxConn;

		private Label label34;

		private TabPage tabPage5;

		private Label label35;

		private Label label36;

		private ComboBox cmbExtOnlinePlay;

		private ComboBox cmbExtOverlay;

		private TabPage tabPage6;

		private Button btnDpDel;

		private Button btnDpAdd;

		private TextBox txtDpEntry;

		private Label label37;

		private ListBox lstDp;

		private Label label38;

		private Label label39;

		private ComboBox cmbOffline;

		private TextBox txtExtras;

		private Label label40;

		private Label label41;

		private ComboBox cmbExtHookRefCount;

		private Label label42;

		private ComboBox cmbExtOnlineKey;

		private CheckBox chklauncherx64;

		private ContextMenuStrip dlcContextMenu;
        private Label label43;
        private ComboBox cmbExtLogging;
        private Button btnAddFromClipboard;
        private Button btnDelAllDlc;
        private ToolStripMenuItem mnuToggle;

		public FrmAppSetting()
		{
            InitializeComponent();
            tabDlc = tabControl1.TabPages[1];
            tabEmu = tabControl1.TabPages[2];
            tabNet = tabControl1.TabPages[3];
            tabExtra = tabControl1.TabPages[4];
            tabDp = tabControl1.TabPages[5];
		}

		private void FrmAppSetting_Load(object sender, EventArgs e)
		{
			base.AcceptButton = btnOK;
			base.CancelButton = btnCancel;
			if (CategoryList != null)
			{
				foreach (string current in CategoryList)
				{
                    cmbCategory.Items.Add(current);
				}
			}
		}

		private string ParseAppBool(int val)
		{
			switch (val)
			{
			case -1:
				return "<Use Global Setting>";
			case 0:
				return "False";
			}
			return "True";
		}

		private int SerializeAppBool(string val)
		{
			if (string.Equals(val, "<Use Global Setting>", StringComparison.OrdinalIgnoreCase))
			{
				return -1;
			}
			if (string.Equals(val, "True", StringComparison.OrdinalIgnoreCase))
			{
				return 1;
			}
			return 0;
		}

		private string SerializeAppString(string val)
		{
			if (string.Equals(val, "<Use Global Setting>", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			return val;
		}

		public void SetEditApp(CApp app, CConfig conf)
		{
            m_App = app;
            txtAppName.Text = app.GameName;
            txtAppPath.Text = app.Path;
            txtAppParam.Text = app.CommandLine;
            txtAppFolder.Text = app.StartIn;
            txtAppIcon.Text = app.IconPath;
            txtAppId.Text = app.AppId.ToString();
            chkInject.Checked = app.InjectDll;
            chklauncherx64.Checked = app.Use64Launcher;
			if (app.AppId == -1)
			{
                chkNonSteam.Checked = true;
                m_LastAppId = "0";
			}
            cmbCategory.SelectedIndex = -1;
            cmbCategory.Text = app.Category;
            cmbAppLang.SelectedIndex = -1;
            cmbAppLang.Text = (string.IsNullOrWhiteSpace(app.Language) ? "<Use Global Setting>" : app.Language);
            cmbAppLV.Text = ParseAppBool(app.LowViolence);
            cmbAppPersist.Text = ParseAppBool(app.Persist ? 1 : 0);
            cmbEmuSteamId.SelectedIndex = -1;
            cmbEmuSteamId.Text = (string.IsNullOrWhiteSpace(app.SteamIdGeneration) ? "<Use Global Setting>" : (string.Equals(app.SteamIdGeneration, "Manual", StringComparison.OrdinalIgnoreCase) ? app.ManualSteamId.ToString() : app.SteamIdGeneration));
            cmbPersonaName.SelectedIndex = -1;
            cmbPersonaName.Text = (string.IsNullOrWhiteSpace(app.PersonaName) ? "<Use Global Setting>" : app.PersonaName);
            chkDlcSubsDefault.Checked = app.DefaultDlcSubscribed;
			foreach (KVDlc<string, string> current in app.DlcList)
			{
                m_TempDlcList.Add(current);
                lstDlc.Items.Add(current.DlcId + " = " + current.DlcName);
			}
            cmbAutoJoinInvite.Text = ParseAppBool(app.AutomaticallyJoinInvite);
            cmbStorageOnAppData.Text = ParseAppBool(app.StorageOnAppdata);
            cmbSeparateStorageByName.Text = ParseAppBool(app.SeparateStorageByName);
            cmbRemoteStoragePath.SelectedIndex = -1;
            cmbRemoteStoragePath.Text = (string.IsNullOrWhiteSpace(app.RemoteStoragePath) ? "" : app.RemoteStoragePath);
            cmbSecuredServer.Text = ParseAppBool(app.SecuredServer);
            cmbDisableFriendList.Text = ParseAppBool(app.DisableFriendList);
            cmbDisableLeaderboard.Text = ParseAppBool(app.DisableLeaderboard);
            cmbQuickJoinHotkey.Text = (string.IsNullOrWhiteSpace(app.RemoteStoragePath) ? "<Use Global Setting>" : app.RemoteStoragePath);
            cmbEnableHTTP.Text = ParseAppBool(app.EnableHTTP);
            cmbEnableIngameVoice.Text = ParseAppBool(app.EnableInGameVoice);
            cmbEnableLobbyFilter.Text = ParseAppBool(app.EnableLobbyFilter);
            cmbEnableVR.Text = ParseAppBool(app.VR);
            cmbDisableGC.Text = app.DisableGC.ToString();
            cmbFailOnNonStats.Text = app.FailOnNonExistenceStats.ToString();
            cmbOffline.Text = ParseAppBool(app.Offline);
            cmbNetUseGlobal.SelectedIndex = ((app.ListenPort == -1) ? 0 : 1);
            numNetPort.Value = ((app.ListenPort == -1) ? conf.ListenPort : app.ListenPort);
            numNetMaxPort.Value = ((app.MaximumPort == -1) ? conf.MaximumPort : app.MaximumPort);
            numNetDiscoverInterval.Value = ((app.DiscoveryInterval == -1) ? conf.DiscoveryInterval : app.DiscoveryInterval);
            numNetMaxConn.Value = ((app.MaximumConnection == -1) ? conf.MaximumConnection : app.MaximumConnection);
			foreach (string current2 in app.BroadcastAddress)
			{
                lstNetBroadcast.Items.Add(current2);
			}
            cmbExtLogging.Text = ParseAppBool(app.EnableDebugLogging);
            cmbExtOverlay.Text = ParseAppBool(app.EnableOverlay);
            cmbExtOnlinePlay.Text = ParseAppBool(app.EnableOnlinePlay);
            cmbExtHookRefCount.Text = app.EnableHookRefCount.ToString();
            cmbExtOnlineKey.SelectedIndex = -1;
            cmbExtOnlineKey.Text = (string.IsNullOrWhiteSpace(app.OnlineKey) ? "<Use Global Setting>" : app.OnlineKey);
            txtExtras.Text = app.Extras;
			foreach (string current3 in app.DirectPatchList)
			{
                lstDp.Items.Add(current3);
			}
            m_bwVerifyer.WorkerReportsProgress = false;
            m_bwVerifyer.WorkerSupportsCancellation = false;
            m_bwVerifyer.DoWork += new DoWorkEventHandler(bwVerifyer_DoWork);
            m_bwVerifyer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwVerifyer_RunWorkerCompleted);
            m_bwVerifyer.RunWorkerAsync(app);
		}

		private bool SaveApp(bool DontCheckExtra = false)
		{
            m_App.GameName = txtAppName.Text;
            m_App.Path = txtAppPath.Text;
            m_App.CommandLine = txtAppParam.Text;
            m_App.StartIn = txtAppFolder.Text;
            m_App.IconPath = txtAppIcon.Text;
            m_App.Category = cmbCategory.Text;
			try
			{
				int num = Convert.ToInt32(txtAppId.Text);
				if (num == 0 && MessageBox.Show("Please specify game app id. You can find your game app id on steam store url: http://store.steampowered.com/app/<AppId> \n\nContinue?\nYou will need to setup app id later.", "Invalid app id", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					bool result = false;
					return result;
				}
                m_App.AppId = num;
			}
			catch
			{
				MessageBox.Show("Invalid Game AppId!", "Invalid input");
				bool result = false;
				return result;
			}
            m_App.Language = SerializeAppString(cmbAppLang.Text);
            m_App.LowViolence = SerializeAppBool(cmbAppLV.Text);
            m_App.Persist = (SerializeAppBool(cmbAppPersist.Text) == 1);
            m_App.InjectDll = chkInject.Checked;
            m_App.Use64Launcher = chklauncherx64.Checked;
			if (string.Equals(cmbEmuSteamId.Text, "<Use Global Setting>", StringComparison.OrdinalIgnoreCase))
			{
                m_App.SteamIdGeneration = null;
			}
			else if (string.Equals(cmbEmuSteamId.Text, "Static", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "Random", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "PersonaName", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "ip", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "GenerateRandom", StringComparison.OrdinalIgnoreCase))
			{
                m_App.SteamIdGeneration = cmbEmuSteamId.Text;
			}
			else
			{
				try
				{
                    m_App.ManualSteamId = Convert.ToInt64(cmbEmuSteamId.Text);
                    m_App.SteamIdGeneration = "Manual";
				}
				catch
				{
					MessageBox.Show("Invalid steam id!", "Invalid input");
				}
			}
            m_App.PersonaName = SerializeAppString(cmbPersonaName.Text);
            m_App.AutomaticallyJoinInvite = SerializeAppBool(cmbAutoJoinInvite.Text);
            m_App.StorageOnAppdata = SerializeAppBool(cmbStorageOnAppData.Text);
            m_App.SeparateStorageByName = SerializeAppBool(cmbSeparateStorageByName.Text);
            m_App.RemoteStoragePath = SerializeAppString(cmbRemoteStoragePath.Text);
            m_App.SecuredServer = SerializeAppBool(cmbSecuredServer.Text);
            m_App.DisableFriendList = SerializeAppBool(cmbDisableFriendList.Text);
            m_App.DisableLeaderboard = SerializeAppBool(cmbDisableLeaderboard.Text);
            m_App.QuickJoinHotkey = SerializeAppString(cmbQuickJoinHotkey.Text);
            m_App.EnableHTTP = SerializeAppBool(cmbEnableHTTP.Text);
            m_App.EnableInGameVoice = SerializeAppBool(cmbEnableIngameVoice.Text);
            m_App.EnableLobbyFilter = SerializeAppBool(cmbEnableLobbyFilter.Text);
            m_App.VR = SerializeAppBool(cmbEnableVR.Text);
            m_App.DisableGC = Convert.ToBoolean(cmbDisableGC.Text);
            m_App.FailOnNonExistenceStats = Convert.ToBoolean(cmbFailOnNonStats.Text);
            m_App.Offline = SerializeAppBool(cmbOffline.Text);
            m_App.DefaultDlcSubscribed = chkDlcSubsDefault.Checked;
            m_App.DlcList.Clear();
			foreach (KVDlc<string, string> current in m_TempDlcList)
			{
                m_App.DlcList.Add(current);
			}
			bool flag = cmbNetUseGlobal.SelectedIndex == 1;
            m_App.ListenPort = (flag ? Convert.ToInt32(numNetPort.Value) : -1);
            m_App.MaximumPort = (flag ? Convert.ToInt32(numNetMaxPort.Value) : -1);
            m_App.DiscoveryInterval = (flag ? Convert.ToInt32(numNetDiscoverInterval.Value) : -1);
            m_App.MaximumConnection = (flag ? Convert.ToInt32(numNetMaxConn.Value) : -1);
            m_App.BroadcastAddress.Clear();

			if (flag)
			{
                foreach (string item in lstNetBroadcast.Items)
                {
                    m_App.BroadcastAddress.Add(item);
                }
			}

            m_App.EnableDebugLogging = SerializeAppBool(cmbExtLogging.Text);
            m_App.EnableOverlay = SerializeAppBool(cmbExtOverlay.Text);
            m_App.EnableOnlinePlay = SerializeAppBool(cmbExtOnlinePlay.Text);
            m_App.EnableHookRefCount = (SerializeAppBool(cmbExtHookRefCount.Text) == 1);
            m_App.Extras = txtExtras.Text;
			if (string.Equals(cmbExtOnlineKey.Text, "<Use Global Setting>", StringComparison.OrdinalIgnoreCase))
			{
                m_App.OnlineKey = null;
			}
			else
			{
                m_App.OnlineKey = cmbExtOnlineKey.Text;
			}
            m_App.DirectPatchList.Clear();
			foreach (string item2 in lstDp.Items)
			{
                m_App.DirectPatchList.Add(item2);
			}
			if (!DontCheckExtra)
			{
                CheckExtra();
			}
			return true;
		}

		private void CheckExtra()
		{
			if (m_App.AppId == 218620)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (KVDlc<string, string> current in m_TempDlcList)
				{
					if (current.DlcId.Equals("src103582791433980119", StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
					}
					if (current.DlcId.Equals("src103582791435633447", StringComparison.OrdinalIgnoreCase))
					{
						flag2 = true;
					}
				}
				if (!flag || !flag2)
				{
					if (MessageBox.Show("Do you want to subscribe to payday 2 community item?", "Payday 2 DLCs", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						if (!flag)
						{
							KVDlc<string, string> item = new KVDlc<string, string>("src103582791433980119", "Payday 2 Community", false);
                            m_TempDlcList.Add(item);
                            lstDlc.Items.Add(item.DlcId + " = " + item.DlcName);
						}
						if (!flag2)
						{
							KVDlc<string, string> item2 = new KVDlc<string, string>("src103582791435633447", "Payday 2 Mod - HoxHud", false);
                            m_TempDlcList.Add(item2);
                            lstDlc.Items.Add(item2.DlcId + " = " + item2.DlcName);
						}
					}
					else
					{
						if (!flag)
						{
							KVDlc<string, string> item3 = new KVDlc<string, string>("src103582791433980119", "0", false);
                            m_TempDlcList.Add(item3);
                            lstDlc.Items.Add(item3.DlcId + " = " + item3.DlcName);
						}
						if (!flag2)
						{
							KVDlc<string, string> item4 = new KVDlc<string, string>("src103582791435633447", "0", false);
                            m_TempDlcList.Add(item4);
                            lstDlc.Items.Add(item4.DlcId + " = " + item4.DlcName);
						}
					}
                    SaveApp(true);
					return;
				}
			}
			else if ((m_App.AppId == 45770 || m_App.AppId == 45740) && m_App.DefaultDlcSubscribed)
			{
				if (MessageBox.Show("This game requires DLC to be set manually. Do you want to add all known DLCs for this game?", "Dead Rising 2", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					if (m_App.AppId == 45770)
					{
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						bool flag6 = false;
						foreach (KVDlc<string, string> current2 in m_TempDlcList)
						{
							if (current2.DlcId.Equals("45773", StringComparison.OrdinalIgnoreCase))
							{
								flag3 = true;
							}
							if (current2.DlcId.Equals("45774", StringComparison.OrdinalIgnoreCase))
							{
								flag4 = true;
							}
							if (current2.DlcId.Equals("45775", StringComparison.OrdinalIgnoreCase))
							{
								flag5 = true;
							}
							if (current2.DlcId.Equals("45776", StringComparison.OrdinalIgnoreCase))
							{
								flag6 = true;
							}
						}
						if (!flag3)
						{
							KVDlc<string, string> item5 = new KVDlc<string, string>("45773", "Dead Rising 2: Off the Record BBQ Chef Skills Pack", false);
                            m_TempDlcList.Add(item5);
                            lstDlc.Items.Add(item5.DlcId + " = " + item5.DlcName);
						}
						if (!flag4)
						{
							KVDlc<string, string> item6 = new KVDlc<string, string>("45774", "Dead Rising 2: Off the Record COSPLAY Skills Pack", false);
                            m_TempDlcList.Add(item6);
                            lstDlc.Items.Add(item6.DlcId + " = " + item6.DlcName);
						}
						if (!flag5)
						{
							KVDlc<string, string> item7 = new KVDlc<string, string>("45775", "Dead Rising 2: Off the Record Cyborg Skills Pack", false);
                            m_TempDlcList.Add(item7);
                            lstDlc.Items.Add(item7.DlcId + " = " + item7.DlcName);
						}
						if (!flag6)
						{
							KVDlc<string, string> item8 = new KVDlc<string, string>("45776", "Dead Rising 2: Off the Record Firefighter Skills Pack", false);
                            m_TempDlcList.Add(item8);
                            lstDlc.Items.Add(item8.DlcId + " = " + item8.DlcName);
						}
					}
					else
					{
						bool flag7 = false;
						bool flag8 = false;
						bool flag9 = false;
						bool flag10 = false;
						foreach (KVDlc<string, string> current3 in m_TempDlcList)
						{
							if (current3.DlcId.Equals("353050", StringComparison.OrdinalIgnoreCase))
							{
								flag7 = true;
							}
							if (current3.DlcId.Equals("353051", StringComparison.OrdinalIgnoreCase))
							{
								flag8 = true;
							}
							if (current3.DlcId.Equals("353052", StringComparison.OrdinalIgnoreCase))
							{
								flag9 = true;
							}
							if (current3.DlcId.Equals("353053", StringComparison.OrdinalIgnoreCase))
							{
								flag10 = true;
							}
						}
						if (!flag7)
						{
							KVDlc<string, string> item9 = new KVDlc<string, string>("353050", "Dead Rising 2 - Ninja Skills Pack", false);
                            m_TempDlcList.Add(item9);
                            lstDlc.Items.Add(item9.DlcId + " = " + item9.DlcName);
						}
						if (!flag8)
						{
							KVDlc<string, string> item10 = new KVDlc<string, string>("353051", "Dead Rising 2 - Psychopath Skills Pack", false);
                            m_TempDlcList.Add(item10);
                            lstDlc.Items.Add(item10.DlcId + " = " + item10.DlcName);
						}
						if (!flag9)
						{
							KVDlc<string, string> item11 = new KVDlc<string, string>("353052", "Dead Rising 2 - Soldier of Fortune Pack", false);
                            m_TempDlcList.Add(item11);
                            lstDlc.Items.Add(item11.DlcId + " = " + item11.DlcName);
						}
						if (!flag10)
						{
							KVDlc<string, string> item12 = new KVDlc<string, string>("353053", "Dead Rising 2 - Sports Fan Skills Pack", false);
                            m_TempDlcList.Add(item12);
                            lstDlc.Items.Add(item12.DlcId + " = " + item12.DlcName);
						}
					}
				}
                chkDlcSubsDefault.Checked = false;
                m_App.DefaultDlcSubscribed = false;
                SaveApp(true);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (SaveApp(false))
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		private bool VerifyDigitalSignature(string path, out bool Verified)
		{
			if (File.Exists(path))
			{
				Verified = AuthenticodeTools.IsTrusted(path);
				return true;
			}
			Verified = false;
			return false;
		}

		private void bwVerifyer_DoWork(object sender, DoWorkEventArgs e)
		{
			CApp cApp = (CApp)e.Argument;

			bool x86found = false;
			bool x64found = false;
			bool x86Verified = false;
			bool x64Verified = false;

            var pathList = new List<string>();

            var searchPaths = new List<string>(new string[] {
                "bin", "binaries",
                "binaries\\Win32", "binaries\\Win64",
                "..\\bin", "..\\binaries",
                "..\\binaries\\Win32", "..\\binaries\\Win64",
            });

            if (cApp.Path != null)
			{
                var basePath = Path.GetDirectoryName(CApp.GetAbsolutePath(cApp.Path));

                pathList.Add(basePath);

                foreach (var path in searchPaths)
                {
                    pathList.Add(Path.Combine(basePath, path));
                }
			}

			if (cApp.StartIn != null)
			{
                var basePath = Path.GetDirectoryName(CApp.GetAbsolutePath(cApp.Path));

                pathList.Add(basePath);

                foreach (var path in searchPaths)
                {
                    pathList.Add(Path.Combine(basePath, path));
                }
			}

            foreach (var path in pathList)
            {
                if (!x86found)
                {
                    x86found = VerifyDigitalSignature(Path.Combine(path, "steam_api.dll"), out x86Verified);
                }

                if (!x64found)
                {
                    x64found = VerifyDigitalSignature(Path.Combine(path, "steam_api64.dll"), out x64Verified);

                }

                if (x86found && x64found)
                {
                    break;
                }
            }
            

			e.Result = new ApiStatusParam
			{
				x86Found = x86found,
				x86Verified = x86Verified,
				x64Found = x64found,
				x64Verified = x64Verified
			};
		}

		private void bwVerifyer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled || e.Error != null)
			{
				return;
			}
			ApiStatusParam apiStatusParam = (ApiStatusParam)e.Result;
			bool flag = false;
            lblSteamApiStatus86.Text = "steam_api.dll ";
			if (apiStatusParam.x86Found)
			{
				if (apiStatusParam.x86Verified)
				{
					Label expr_45 = lblSteamApiStatus86;
					expr_45.Text += "(Verified/Original).";
                   pb86.Image = new Bitmap(Resources.bullet_green);
				}
				else
				{
					Label expr_77 = lblSteamApiStatus86;
					expr_77.Text += "(Failed/Not original).";
                    pb86.Image = new Bitmap(Resources.bullet_red);
					flag = true;
				}
			}
			else
			{
				Label expr_AB = lblSteamApiStatus86;
				expr_AB.Text += "(Not found/Not required).";
                pb86.Image = new Bitmap(Resources.bullet_gray);
			}
            lblSteamApiStatus64.Text = "steam_api64.dll ";
			if (apiStatusParam.x64Found)
			{
				if (apiStatusParam.x64Verified)
				{
					Label expr_FB = lblSteamApiStatus64;
					expr_FB.Text += "(Verified/Original).";
                    pb64.Image = new Bitmap(Resources.bullet_green);
				}
				else
				{
					Label expr_12D = lblSteamApiStatus64;
					expr_12D.Text += "(Failed/Not original).";
                    pb64.Image = new Bitmap(Resources.bullet_red);
					flag = true;
				}
			}
			else
			{
				Label expr_161 = lblSteamApiStatus64;
				expr_161.Text += "(Not found/Not required).";
                pb64.Image = new Bitmap(Resources.bullet_gray);
			}
            lblApiModified.Visible = (!chkNonSteam.Checked && flag);
            IsApiModified = flag;
		}

		private void cmbNetUseGlobal_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool enabled = cmbNetUseGlobal.SelectedIndex == 1;
            numNetPort.Enabled = enabled;
            numNetMaxPort.Enabled = enabled;
            numNetDiscoverInterval.Enabled = enabled;
            numNetMaxConn.Enabled = enabled;
            txtNetIp.Enabled = enabled;
            lstNetBroadcast.Enabled = enabled;
            btnNetAdd.Enabled = enabled;
            btnNetDel.Enabled = enabled;
		}

		private void btnNetAdd_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtNetIp.Text) || string.IsNullOrWhiteSpace(txtNetIp.Text))
			{
				return;
			}
            lstNetBroadcast.Items.Add(txtNetIp.Text);
            txtNetIp.Text = "";
		}

		private void btnNetDel_Click(object sender, EventArgs e)
		{
			if (lstNetBroadcast.SelectedIndex == -1)
			{
				return;
			}
            lstNetBroadcast.Items.RemoveAt(lstNetBroadcast.SelectedIndex);
		}

		private void btnAddDlc_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtDlcAppId.Text) || string.IsNullOrWhiteSpace(txtDlcAppId.Text))
			{
				return;
			}
			if (string.IsNullOrEmpty(cmbDlcName.Text) || string.IsNullOrWhiteSpace(cmbDlcName.Text))
			{
				return;
			}
            m_TempDlcList.Add(new KVDlc<string, string>(txtDlcAppId.Text, cmbDlcName.Text, false));
            lstDlc.Items.Add(txtDlcAppId.Text + " = " + cmbDlcName.Text);
            txtDlcAppId.Text = "";
            cmbDlcName.Text = "";
		}

		private void btnDelDlc_Click(object sender, EventArgs e)
		{
			if (lstDlc.SelectedIndex == -1)
			{
				return;
			}
            m_TempDlcList.RemoveAt(lstDlc.SelectedIndex);
            lstDlc.Items.RemoveAt(lstDlc.SelectedIndex);
		}

		private void btnCreateShortcut_Click(object sender, EventArgs e)
		{
			if (!SaveApp(false))
			{
				return;
			}
			try
			{
				string linkFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), m_App.GameName + ".lnk");
				using (ShellLink shellLink = new ShellLink())
				{
					if (m_App.AppId != -1)
					{
						shellLink.Target = Application.ExecutablePath;
						shellLink.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
						shellLink.Arguments = "-appid " + m_App.AppId;
						shellLink.IconPath = CApp.GetAbsolutePath(string.IsNullOrEmpty(m_App.IconPath) ? m_App.Path : m_App.IconPath);
						shellLink.Description = "Play " + m_App.GameName;
						shellLink.Save(linkFile);
					}
					else
					{
						shellLink.Target = CApp.GetAbsolutePath(m_App.Path);
						shellLink.WorkingDirectory = Path.GetDirectoryName(CApp.GetAbsolutePath(m_App.Path));
						shellLink.Arguments = m_App.CommandLine;
						shellLink.IconPath = CApp.GetAbsolutePath(string.IsNullOrEmpty(m_App.IconPath) ? m_App.Path : m_App.IconPath);
						shellLink.Description = "Run " + m_App.GameName;
						shellLink.Save(linkFile);
					}
				}
			}
			catch (Exception arg_182_0)
			{
				MessageBox.Show(arg_182_0.Message, "Error creating shortcut");
			}
		}

		private void btnBrowseExe_Click(object sender, EventArgs e)
		{
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select game executable",
                Filter = "Game executables (*.exe)|*.exe;*.bat;*.cmd;*.lnk|All Files|*.*",
                FilterIndex = 1,
                Multiselect = false,
                FileName = txtAppPath.Text
            };
            try
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(CApp.GetAbsolutePath(txtAppPath.Text));
			}
			catch
			{
			}
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (openFileDialog.FileName.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						using (ShellLink shellLink = new ShellLink(openFileDialog.FileName))
						{
                            txtAppPath.Text = CApp.MakeRelativePath(shellLink.Target, true);
						}
						return;
					}
					catch
					{
						return;
					}
				}
                txtAppPath.Text = CApp.MakeRelativePath(openFileDialog.FileName, true);
			}
		}

		private void btnBrowseFolder_Click(object sender, EventArgs e)
		{
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                SelectedPath = CApp.GetAbsolutePath(txtAppFolder.Text)
            };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string text = folderBrowserDialog.SelectedPath;
				if (folderBrowserDialog.SelectedPath.Substring(folderBrowserDialog.SelectedPath.Length - 1)[0] != Path.DirectorySeparatorChar)
				{
					text += "\\";
				}
                txtAppFolder.Text = CApp.MakeRelativePath(text, true);
			}
		}

		private void btnBrowseIcon_Click(object sender, EventArgs e)
		{
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select game icon",
                Filter = "Icon file (*.exe,*.ico)|*.exe;*.ico|All Files|*.*",
                FilterIndex = 1,
                Multiselect = false,
                FileName = txtAppIcon.Text
            };
            try
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(CApp.GetAbsolutePath(txtAppIcon.Text));
			}
			catch
			{
			}
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
                txtAppIcon.Text = CApp.MakeRelativePath(openFileDialog.FileName, true);
			}
		}

		private void mnuToggle_Click(object sender, EventArgs e)
		{
            Control control = null;

            if (sender is ToolStripItem toolStripItem)
            {
                if (toolStripItem.Owner is ContextMenuStrip contextMenuStrip)
                {
                    control = contextMenuStrip.SourceControl;
                }
            }

            // Called directly with double click
            if (sender == lstDlc)
            {
                control = lstDlc;
            }

            if (control != lstDlc)
			{
				if (control == lstNetBroadcast || control == lstDp)
				{
					ListBox listBox = (ListBox)control;
					if (listBox.SelectedIndex == -1)
					{
						return;
					}
					string text = listBox.Items[listBox.SelectedIndex].ToString();
					if (text.Length > 0 && text[0] == ';')
					{
						text = text.Substring(1);
					}
					else
					{
						text = ";" + text;
					}
					listBox.Items[listBox.SelectedIndex] = text;
					listBox.Refresh();
				}
				return;
			}

			if (lstDlc.SelectedIndex == -1)
			{
				return;
			}

			string dlcName = m_TempDlcList[lstDlc.SelectedIndex].DlcName;
			string dlcId = m_TempDlcList[lstDlc.SelectedIndex].DlcId;
			bool disabled = m_TempDlcList[lstDlc.SelectedIndex].Disabled;

            m_TempDlcList[lstDlc.SelectedIndex] = new KVDlc<string, string>(dlcName, dlcId, !disabled);

            lstDlc.Refresh();
		}

		private void lstDlc_DrawItem(object sender, DrawItemEventArgs e)
		{
			try
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				if (m_TempDlcList[e.Index].Disabled)
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(-31108)), e.Bounds);
				}
				graphics.DrawString(lstDlc.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF((float)e.Bounds.X, (float)e.Bounds.Y));
				e.DrawFocusRectangle();
			}
			catch (Exception)
			{
			}
		}

		private void lstDp_DrawItem(object sender, DrawItemEventArgs e)
		{
			try
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				string text = lstDp.Items[e.Index].ToString();
				if (text.Length > 0 && text[0] == ';')
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(-31108)), e.Bounds);
				}
				graphics.DrawString(lstDp.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF((float)e.Bounds.X, (float)e.Bounds.Y));
				e.DrawFocusRectangle();
			}
			catch (Exception)
			{
			}
		}

		private void lstNetBroadcast_DrawItem(object sender, DrawItemEventArgs e)
		{
			try
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				string text = lstNetBroadcast.Items[e.Index].ToString();
				if (text.Length > 0 && text[0] == ';')
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(-31108)), e.Bounds);
				}
				graphics.DrawString(lstNetBroadcast.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF((float)e.Bounds.X, (float)e.Bounds.Y));
				e.DrawFocusRectangle();
			}
			catch (Exception)
			{
			}
		}

		private void chkNonSteam_CheckedChanged(object sender, EventArgs e)
		{
			if (chkNonSteam.Checked)
			{
                m_LastAppId = txtAppId.Text;
                txtAppId.Text = "-1";
                txtAppId.Enabled = false;
                lblApiStatusDesc.Visible = false;
                lblSteamApiStatus86.Visible = false;
                lblSteamApiStatus64.Visible = false;
                pb86.Visible = false;
                pb64.Visible = false;
                lblApiModified.Visible = false;
                tabControl1.SuspendLayout();
                tabControl1.TabPages.Remove(tabDlc);
                tabControl1.TabPages.Remove(tabEmu);
                tabControl1.TabPages.Remove(tabNet);
                tabControl1.TabPages.Remove(tabExtra);
                tabControl1.TabPages.Remove(tabDp);
                tabControl1.ResumeLayout();
				return;
			}
            txtAppId.Text = m_LastAppId;
            txtAppId.Enabled = true;
            lblApiStatusDesc.Visible = true;
            lblSteamApiStatus86.Visible = true;
            lblSteamApiStatus64.Visible = true;
            pb86.Visible = true;
            pb64.Visible = true;
            lblApiModified.Visible = IsApiModified;
            tabControl1.SuspendLayout();
            tabControl1.TabPages.Add(tabDlc);
            tabControl1.TabPages.Add(tabEmu);
            tabControl1.TabPages.Add(tabNet);
            tabControl1.TabPages.Add(tabExtra);
            tabControl1.TabPages.Add(tabDp);
            tabControl1.ResumeLayout();
		}

		private void cmbRemoteStoragePath_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbRemoteStoragePath.SelectedIndex != -1 && cmbRemoteStoragePath.Text == "Browse...")
			{
                cmbRemoteStoragePath.SelectedIndex = -1;
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					string SelPath = folderBrowserDialog.SelectedPath;
					if (folderBrowserDialog.SelectedPath.Substring(folderBrowserDialog.SelectedPath.Length - 1)[0] != Path.DirectorySeparatorChar)
					{
						SelPath += "\\";
					}
					base.BeginInvoke(new Action(delegate
					{
                        cmbRemoteStoragePath.Text = CApp.MakeRelativePath(SelPath, true);
					}));
				}
			}
		}

		private void btnDpAdd_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtDpEntry.Text) || string.IsNullOrWhiteSpace(txtDpEntry.Text))
			{
				return;
			}
            lstDp.Items.Add(txtDpEntry.Text);
            txtDpEntry.Text = "";
		}

		private void btnDpDel_Click(object sender, EventArgs e)
		{
			if (lstDp.SelectedIndex == -1)
			{
				return;
			}
            lstDp.Items.RemoveAt(lstDp.SelectedIndex);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
                components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAppSetting));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chklauncherx64 = new System.Windows.Forms.CheckBox();
            this.pb64 = new System.Windows.Forms.PictureBox();
            this.pb86 = new System.Windows.Forms.PictureBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.chkInject = new System.Windows.Forms.CheckBox();
            this.chkNonSteam = new System.Windows.Forms.CheckBox();
            this.btnBrowseIcon = new System.Windows.Forms.Button();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnBrowseExe = new System.Windows.Forms.Button();
            this.lblSteamApiStatus64 = new System.Windows.Forms.Label();
            this.lblSteamApiStatus86 = new System.Windows.Forms.Label();
            this.lblApiModified = new System.Windows.Forms.Label();
            this.lblApiStatusDesc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAppName = new System.Windows.Forms.TextBox();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.txtAppIcon = new System.Windows.Forms.TextBox();
            this.txtAppFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAppParam = new System.Windows.Forms.TextBox();
            this.txtAppPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDelAllDlc = new System.Windows.Forms.Button();
            this.btnAddFromClipboard = new System.Windows.Forms.Button();
            this.chkDlcSubsDefault = new System.Windows.Forms.CheckBox();
            this.btnDelDlc = new System.Windows.Forms.Button();
            this.btnAddDlc = new System.Windows.Forms.Button();
            this.cmbDlcName = new System.Windows.Forms.ComboBox();
            this.txtDlcAppId = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lstDlc = new System.Windows.Forms.ListBox();
            this.dlcContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmbAppPersist = new System.Windows.Forms.ComboBox();
            this.cmbAppLV = new System.Windows.Forms.ComboBox();
            this.cmbAppLang = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbOffline = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbDisableGC = new System.Windows.Forms.ComboBox();
            this.cmbFailOnNonStats = new System.Windows.Forms.ComboBox();
            this.cmbEnableVR = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEnableLobbyFilter = new System.Windows.Forms.ComboBox();
            this.cmbEnableIngameVoice = new System.Windows.Forms.ComboBox();
            this.cmbEnableHTTP = new System.Windows.Forms.ComboBox();
            this.cmbQuickJoinHotkey = new System.Windows.Forms.ComboBox();
            this.cmbDisableLeaderboard = new System.Windows.Forms.ComboBox();
            this.cmbDisableFriendList = new System.Windows.Forms.ComboBox();
            this.cmbSecuredServer = new System.Windows.Forms.ComboBox();
            this.cmbRemoteStoragePath = new System.Windows.Forms.ComboBox();
            this.cmbSeparateStorageByName = new System.Windows.Forms.ComboBox();
            this.cmbStorageOnAppData = new System.Windows.Forms.ComboBox();
            this.cmbAutoJoinInvite = new System.Windows.Forms.ComboBox();
            this.cmbPersonaName = new System.Windows.Forms.ComboBox();
            this.cmbEmuSteamId = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnNetDel = new System.Windows.Forms.Button();
            this.btnNetAdd = new System.Windows.Forms.Button();
            this.txtNetIp = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.lstNetBroadcast = new System.Windows.Forms.ListBox();
            this.numNetMaxConn = new System.Windows.Forms.NumericUpDown();
            this.numNetDiscoverInterval = new System.Windows.Forms.NumericUpDown();
            this.numNetMaxPort = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.numNetPort = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbNetUseGlobal = new System.Windows.Forms.ComboBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label43 = new System.Windows.Forms.Label();
            this.cmbExtLogging = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.cmbExtOnlineKey = new System.Windows.Forms.ComboBox();
            this.txtExtras = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbExtOnlinePlay = new System.Windows.Forms.ComboBox();
            this.cmbExtHookRefCount = new System.Windows.Forms.ComboBox();
            this.cmbExtOverlay = new System.Windows.Forms.ComboBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnDpDel = new System.Windows.Forms.Button();
            this.btnDpAdd = new System.Windows.Forms.Button();
            this.txtDpEntry = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lstDp = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreateShortcut = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb86)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.dlcContextMenu.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetDiscoverInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetPort)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(568, 349);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chklauncherx64);
            this.tabPage1.Controls.Add(this.pb64);
            this.tabPage1.Controls.Add(this.pb86);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.cmbCategory);
            this.tabPage1.Controls.Add(this.chkInject);
            this.tabPage1.Controls.Add(this.chkNonSteam);
            this.tabPage1.Controls.Add(this.btnBrowseIcon);
            this.tabPage1.Controls.Add(this.btnBrowseFolder);
            this.tabPage1.Controls.Add(this.btnBrowseExe);
            this.tabPage1.Controls.Add(this.lblSteamApiStatus64);
            this.tabPage1.Controls.Add(this.lblSteamApiStatus86);
            this.tabPage1.Controls.Add(this.lblApiModified);
            this.tabPage1.Controls.Add(this.lblApiStatusDesc);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtAppName);
            this.tabPage1.Controls.Add(this.txtAppId);
            this.tabPage1.Controls.Add(this.txtAppIcon);
            this.tabPage1.Controls.Add(this.txtAppFolder);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtAppParam);
            this.tabPage1.Controls.Add(this.txtAppPath);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(560, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Game Setting";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chklauncherx64
            // 
            this.chklauncherx64.AutoSize = true;
            this.chklauncherx64.Location = new System.Drawing.Point(288, 193);
            this.chklauncherx64.Margin = new System.Windows.Forms.Padding(2);
            this.chklauncherx64.Name = "chklauncherx64";
            this.chklauncherx64.Size = new System.Drawing.Size(109, 17);
            this.chklauncherx64.TabIndex = 18;
            this.chklauncherx64.Text = "Use x64 launcher";
            this.toolTip1.SetToolTip(this.chklauncherx64, "Inject SmartSteamEmu instead of waiting for game to load it.");
            this.chklauncherx64.UseVisualStyleBackColor = true;
            // 
            // pb64
            // 
            this.pb64.Location = new System.Drawing.Point(38, 278);
            this.pb64.Margin = new System.Windows.Forms.Padding(2);
            this.pb64.Name = "pb64";
            this.pb64.Size = new System.Drawing.Size(9, 10);
            this.pb64.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb64.TabIndex = 17;
            this.pb64.TabStop = false;
            // 
            // pb86
            // 
            this.pb86.Location = new System.Drawing.Point(38, 256);
            this.pb86.Margin = new System.Windows.Forms.Padding(2);
            this.pb86.Name = "pb86";
            this.pb86.Size = new System.Drawing.Size(9, 10);
            this.pb86.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb86.TabIndex = 17;
            this.pb86.TabStop = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(286, 173);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(52, 13);
            this.label33.TabIndex = 16;
            this.label33.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(340, 171);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(162, 21);
            this.cmbCategory.TabIndex = 12;
            this.toolTip1.SetToolTip(this.cmbCategory, "Set game category. Group game list by category to view its effect.");
            // 
            // chkInject
            // 
            this.chkInject.AutoSize = true;
            this.chkInject.Location = new System.Drawing.Point(110, 209);
            this.chkInject.Margin = new System.Windows.Forms.Padding(2);
            this.chkInject.Name = "chkInject";
            this.chkInject.Size = new System.Drawing.Size(133, 17);
            this.chkInject.TabIndex = 11;
            this.chkInject.Text = "Inject SmartSteamEmu";
            this.toolTip1.SetToolTip(this.chkInject, "Inject SmartSteamEmu instead of waiting for game to load it.");
            this.chkInject.UseVisualStyleBackColor = true;
            // 
            // chkNonSteam
            // 
            this.chkNonSteam.AutoSize = true;
            this.chkNonSteam.Location = new System.Drawing.Point(110, 191);
            this.chkNonSteam.Margin = new System.Windows.Forms.Padding(2);
            this.chkNonSteam.Name = "chkNonSteam";
            this.chkNonSteam.Size = new System.Drawing.Size(98, 17);
            this.chkNonSteam.TabIndex = 10;
            this.chkNonSteam.Text = "Non steam app";
            this.toolTip1.SetToolTip(this.chkNonSteam, "Launch the apps/games without using SmartSteamEmu.");
            this.chkNonSteam.UseVisualStyleBackColor = true;
            this.chkNonSteam.CheckedChanged += new System.EventHandler(this.chkNonSteam_CheckedChanged);
            // 
            // btnBrowseIcon
            // 
            this.btnBrowseIcon.Location = new System.Drawing.Point(506, 141);
            this.btnBrowseIcon.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseIcon.Name = "btnBrowseIcon";
            this.btnBrowseIcon.Size = new System.Drawing.Size(34, 18);
            this.btnBrowseIcon.TabIndex = 8;
            this.btnBrowseIcon.Text = "...";
            this.toolTip1.SetToolTip(this.btnBrowseIcon, "Browse");
            this.btnBrowseIcon.UseVisualStyleBackColor = true;
            this.btnBrowseIcon.Click += new System.EventHandler(this.btnBrowseIcon_Click);
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(506, 111);
            this.btnBrowseFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(34, 18);
            this.btnBrowseFolder.TabIndex = 6;
            this.btnBrowseFolder.Text = "...";
            this.toolTip1.SetToolTip(this.btnBrowseFolder, "Browse");
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // btnBrowseExe
            // 
            this.btnBrowseExe.Location = new System.Drawing.Point(506, 54);
            this.btnBrowseExe.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseExe.Name = "btnBrowseExe";
            this.btnBrowseExe.Size = new System.Drawing.Size(34, 18);
            this.btnBrowseExe.TabIndex = 3;
            this.btnBrowseExe.Text = "...";
            this.toolTip1.SetToolTip(this.btnBrowseExe, "Browse");
            this.btnBrowseExe.UseVisualStyleBackColor = true;
            this.btnBrowseExe.Click += new System.EventHandler(this.btnBrowseExe_Click);
            // 
            // lblSteamApiStatus64
            // 
            this.lblSteamApiStatus64.AutoSize = true;
            this.lblSteamApiStatus64.Location = new System.Drawing.Point(51, 276);
            this.lblSteamApiStatus64.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSteamApiStatus64.Name = "lblSteamApiStatus64";
            this.lblSteamApiStatus64.Size = new System.Drawing.Size(56, 13);
            this.lblSteamApiStatus64.TabIndex = 12;
            this.lblSteamApiStatus64.Text = "Verifying...";
            // 
            // lblSteamApiStatus86
            // 
            this.lblSteamApiStatus86.AutoSize = true;
            this.lblSteamApiStatus86.Location = new System.Drawing.Point(51, 254);
            this.lblSteamApiStatus86.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSteamApiStatus86.Name = "lblSteamApiStatus86";
            this.lblSteamApiStatus86.Size = new System.Drawing.Size(56, 13);
            this.lblSteamApiStatus86.TabIndex = 12;
            this.lblSteamApiStatus86.Text = "Verifying...";
            // 
            // lblApiModified
            // 
            this.lblApiModified.AutoSize = true;
            this.lblApiModified.Location = new System.Drawing.Point(8, 300);
            this.lblApiModified.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApiModified.Name = "lblApiModified";
            this.lblApiModified.Size = new System.Drawing.Size(479, 13);
            this.lblApiModified.TabIndex = 11;
            this.lblApiModified.Text = "You need to restore original steam_api.dll file with original dll for SmartSteamE" +
    "mu to function properly.";
            this.lblApiModified.Visible = false;
            // 
            // lblApiStatusDesc
            // 
            this.lblApiStatusDesc.AutoSize = true;
            this.lblApiStatusDesc.Location = new System.Drawing.Point(8, 232);
            this.lblApiStatusDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApiStatusDesc.Name = "lblApiStatusDesc";
            this.lblApiStatusDesc.Size = new System.Drawing.Size(340, 13);
            this.lblApiStatusDesc.TabIndex = 11;
            this.lblApiStatusDesc.Text = "Status of steam_api.dll (SmartSteamEmu requires original steam_api dll)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Game Name:";
            // 
            // txtAppName
            // 
            this.txtAppName.Location = new System.Drawing.Point(110, 22);
            this.txtAppName.Margin = new System.Windows.Forms.Padding(2);
            this.txtAppName.Name = "txtAppName";
            this.txtAppName.Size = new System.Drawing.Size(392, 20);
            this.txtAppName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtAppName, "Game name that will appeared on main list.");
            // 
            // txtAppId
            // 
            this.txtAppId.Location = new System.Drawing.Point(110, 168);
            this.txtAppId.Margin = new System.Windows.Forms.Padding(2);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.Size = new System.Drawing.Size(121, 20);
            this.txtAppId.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtAppId, "Your game appid. Find your game appid on steam store http://store.steampowered.co" +
        "m/app/<AppId>.");
            // 
            // txtAppIcon
            // 
            this.txtAppIcon.Location = new System.Drawing.Point(110, 141);
            this.txtAppIcon.Margin = new System.Windows.Forms.Padding(2);
            this.txtAppIcon.Name = "txtAppIcon";
            this.txtAppIcon.Size = new System.Drawing.Size(392, 20);
            this.txtAppIcon.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtAppIcon, "Alternate game icon.");
            // 
            // txtAppFolder
            // 
            this.txtAppFolder.Location = new System.Drawing.Point(110, 111);
            this.txtAppFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtAppFolder.Name = "txtAppFolder";
            this.txtAppFolder.Size = new System.Drawing.Size(392, 20);
            this.txtAppFolder.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtAppFolder, "Where game sees the current directory.");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 85);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Game Parameter:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Game Exe:";
            // 
            // txtAppParam
            // 
            this.txtAppParam.Location = new System.Drawing.Point(110, 83);
            this.txtAppParam.Margin = new System.Windows.Forms.Padding(2);
            this.txtAppParam.Name = "txtAppParam";
            this.txtAppParam.Size = new System.Drawing.Size(392, 20);
            this.txtAppParam.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtAppParam, "Command line parameter for the game.");
            // 
            // txtAppPath
            // 
            this.txtAppPath.Location = new System.Drawing.Point(110, 54);
            this.txtAppPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtAppPath.Name = "txtAppPath";
            this.txtAppPath.Size = new System.Drawing.Size(392, 20);
            this.txtAppPath.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtAppPath, "Path to the main game executable.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Game Folder:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 171);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Game App Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Game Icon:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDelAllDlc);
            this.tabPage2.Controls.Add(this.btnAddFromClipboard);
            this.tabPage2.Controls.Add(this.chkDlcSubsDefault);
            this.tabPage2.Controls.Add(this.btnDelDlc);
            this.tabPage2.Controls.Add(this.btnAddDlc);
            this.tabPage2.Controls.Add(this.cmbDlcName);
            this.tabPage2.Controls.Add(this.txtDlcAppId);
            this.tabPage2.Controls.Add(this.label29);
            this.tabPage2.Controls.Add(this.label28);
            this.tabPage2.Controls.Add(this.lstDlc);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(560, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DLC Manager";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDelAllDlc
            // 
            this.btnDelAllDlc.Location = new System.Drawing.Point(502, 281);
            this.btnDelAllDlc.Name = "btnDelAllDlc";
            this.btnDelAllDlc.Size = new System.Drawing.Size(52, 23);
            this.btnDelAllDlc.TabIndex = 11;
            this.btnDelAllDlc.Text = "Clear";
            this.btnDelAllDlc.UseVisualStyleBackColor = true;
            this.btnDelAllDlc.Click += new System.EventHandler(this.btnDelAllDlc_Click);
            // 
            // btnAddFromClipboard
            // 
            this.btnAddFromClipboard.Location = new System.Drawing.Point(447, 13);
            this.btnAddFromClipboard.Name = "btnAddFromClipboard";
            this.btnAddFromClipboard.Size = new System.Drawing.Size(107, 23);
            this.btnAddFromClipboard.TabIndex = 9;
            this.btnAddFromClipboard.Text = "Add from clipboard";
            this.toolTip1.SetToolTip(this.btnAddFromClipboard, "It expects a list of  dlcid = dlcname format separated by new line");
            this.btnAddFromClipboard.UseVisualStyleBackColor = true;
            this.btnAddFromClipboard.Click += new System.EventHandler(this.btnAddFromClipboard_Click);
            // 
            // chkDlcSubsDefault
            // 
            this.chkDlcSubsDefault.AutoSize = true;
            this.chkDlcSubsDefault.Location = new System.Drawing.Point(11, 13);
            this.chkDlcSubsDefault.Margin = new System.Windows.Forms.Padding(2);
            this.chkDlcSubsDefault.Name = "chkDlcSubsDefault";
            this.chkDlcSubsDefault.Size = new System.Drawing.Size(176, 17);
            this.chkDlcSubsDefault.TabIndex = 0;
            this.chkDlcSubsDefault.Text = "Subscribe to all DLCs by default";
            this.chkDlcSubsDefault.UseVisualStyleBackColor = true;
            // 
            // btnDelDlc
            // 
            this.btnDelDlc.Location = new System.Drawing.Point(502, 76);
            this.btnDelDlc.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelDlc.Name = "btnDelDlc";
            this.btnDelDlc.Size = new System.Drawing.Size(52, 24);
            this.btnDelDlc.TabIndex = 5;
            this.btnDelDlc.Text = "Del";
            this.btnDelDlc.UseVisualStyleBackColor = true;
            this.btnDelDlc.Click += new System.EventHandler(this.btnDelDlc_Click);
            // 
            // btnAddDlc
            // 
            this.btnAddDlc.Location = new System.Drawing.Point(502, 47);
            this.btnAddDlc.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddDlc.Name = "btnAddDlc";
            this.btnAddDlc.Size = new System.Drawing.Size(52, 24);
            this.btnAddDlc.TabIndex = 3;
            this.btnAddDlc.Text = "Add";
            this.btnAddDlc.UseVisualStyleBackColor = true;
            this.btnAddDlc.Click += new System.EventHandler(this.btnAddDlc_Click);
            // 
            // cmbDlcName
            // 
            this.cmbDlcName.FormattingEnabled = true;
            this.cmbDlcName.Items.AddRange(new object[] {
            "1",
            "0",
            "Enter DLC Name Here"});
            this.cmbDlcName.Location = new System.Drawing.Point(279, 50);
            this.cmbDlcName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDlcName.Name = "cmbDlcName";
            this.cmbDlcName.Size = new System.Drawing.Size(219, 21);
            this.cmbDlcName.TabIndex = 2;
            // 
            // txtDlcAppId
            // 
            this.txtDlcAppId.Location = new System.Drawing.Point(57, 50);
            this.txtDlcAppId.Margin = new System.Windows.Forms.Padding(2);
            this.txtDlcAppId.Name = "txtDlcAppId";
            this.txtDlcAppId.Size = new System.Drawing.Size(152, 20);
            this.txtDlcAppId.TabIndex = 1;
            this.txtDlcAppId.TextChanged += new System.EventHandler(this.txtDlcAppId_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(213, 53);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(62, 13);
            this.label29.TabIndex = 8;
            this.label29.Text = "DLC Name:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 53);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 13);
            this.label28.TabIndex = 8;
            this.label28.Text = "DLC ID:";
            // 
            // lstDlc
            // 
            this.lstDlc.ContextMenuStrip = this.dlcContextMenu;
            this.lstDlc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDlc.FormattingEnabled = true;
            this.lstDlc.ItemHeight = 16;
            this.lstDlc.Location = new System.Drawing.Point(11, 76);
            this.lstDlc.Margin = new System.Windows.Forms.Padding(2);
            this.lstDlc.Name = "lstDlc";
            this.lstDlc.Size = new System.Drawing.Size(488, 228);
            this.lstDlc.TabIndex = 4;
            this.lstDlc.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstDlc_DrawItem);
            this.lstDlc.DoubleClick += new System.EventHandler(this.mnuToggle_Click);
            // 
            // dlcContextMenu
            // 
            this.dlcContextMenu.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.dlcContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToggle});
            this.dlcContextMenu.Name = "dlcContextMenu";
            this.dlcContextMenu.Size = new System.Drawing.Size(153, 26);
            // 
            // mnuToggle
            // 
            this.mnuToggle.Name = "mnuToggle";
            this.mnuToggle.Size = new System.Drawing.Size(152, 22);
            this.mnuToggle.Text = "Enable/Disable";
            this.mnuToggle.Click += new System.EventHandler(this.mnuToggle_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.cmbAppPersist);
            this.tabPage3.Controls.Add(this.cmbAppLV);
            this.tabPage3.Controls.Add(this.cmbAppLang);
            this.tabPage3.Controls.Add(this.label30);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label39);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label32);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.cmbOffline);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.cmbDisableGC);
            this.tabPage3.Controls.Add(this.cmbFailOnNonStats);
            this.tabPage3.Controls.Add(this.cmbEnableVR);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.cmbEnableLobbyFilter);
            this.tabPage3.Controls.Add(this.cmbEnableIngameVoice);
            this.tabPage3.Controls.Add(this.cmbEnableHTTP);
            this.tabPage3.Controls.Add(this.cmbQuickJoinHotkey);
            this.tabPage3.Controls.Add(this.cmbDisableLeaderboard);
            this.tabPage3.Controls.Add(this.cmbDisableFriendList);
            this.tabPage3.Controls.Add(this.cmbSecuredServer);
            this.tabPage3.Controls.Add(this.cmbRemoteStoragePath);
            this.tabPage3.Controls.Add(this.cmbSeparateStorageByName);
            this.tabPage3.Controls.Add(this.cmbStorageOnAppData);
            this.tabPage3.Controls.Add(this.cmbAutoJoinInvite);
            this.tabPage3.Controls.Add(this.cmbPersonaName);
            this.tabPage3.Controls.Add(this.cmbEmuSteamId);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(560, 323);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Emulator Setting";
            // 
            // cmbAppPersist
            // 
            this.cmbAppPersist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppPersist.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbAppPersist.Location = new System.Drawing.Point(385, 63);
            this.cmbAppPersist.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAppPersist.Name = "cmbAppPersist";
            this.cmbAppPersist.Size = new System.Drawing.Size(153, 21);
            this.cmbAppPersist.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cmbAppPersist, "Don\'t close the loader. When the loader is closed, the steam_api will be given ba" +
        "ck to steam. Enable this if the game start another process (.bat or another .exe" +
        ").");
            // 
            // cmbAppLV
            // 
            this.cmbAppLV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppLV.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbAppLV.Location = new System.Drawing.Point(385, 39);
            this.cmbAppLV.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAppLV.Name = "cmbAppLV";
            this.cmbAppLV.Size = new System.Drawing.Size(153, 21);
            this.cmbAppLV.TabIndex = 3;
            this.toolTip1.SetToolTip(this.cmbAppLV, "Enable this to have a low violence version of your game and/or you are residing i" +
        "n a country that requires you to have this restriction.");
            // 
            // cmbAppLang
            // 
            this.cmbAppLang.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "English",
            "Arabic",
            "Brazilian",
            "Bulgarian",
            "Croatian",
            "Czech",
            "Danish",
            "Dutch",
            "Estonian",
            "Finnish",
            "French",
            "German",
            "Greek",
            "Hungarian",
            "Italian",
            "Japanese",
            "Koreana",
            "Lithuanian",
            "Norwegian",
            "Polish",
            "Portuguese",
            "Russian",
            "Romanian",
            "Serbian",
            "Simplified Chinese",
            "Spanish",
            "Swedish",
            "Thai",
            "Traditional Chinese",
            "Turkish",
            "Ukrainian"});
            this.cmbAppLang.Location = new System.Drawing.Point(385, 15);
            this.cmbAppLang.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAppLang.Name = "cmbAppLang";
            this.cmbAppLang.Size = new System.Drawing.Size(153, 21);
            this.cmbAppLang.TabIndex = 2;
            this.cmbAppLang.Text = "<Use Global Setting>";
            this.toolTip1.SetToolTip(this.cmbAppLang, "Game language.");
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(296, 66);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 13);
            this.label30.TabIndex = 14;
            this.label30.Text = "Persist Loader:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(296, 41);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Low Violence:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Game Language:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(296, 296);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(100, 13);
            this.label39.TabIndex = 1;
            this.label39.Text = "Steam offline mode:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(296, 249);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Disable Game Coordinator:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(296, 225);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(135, 13);
            this.label32.TabIndex = 1;
            this.label32.Text = "Fail on non existance stats:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(296, 177);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Enable VR:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(296, 153);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Enable Lobby Filter";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(296, 128);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Enable In-Game Voice:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(296, 104);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Enable HTTP:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 177);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Remote Storage Path:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(13, 298);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(97, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Quick Join Hotkey:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(13, 274);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(108, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Disable Leaderboard:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 249);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Disable Friend List:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 225);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Secured Server:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 153);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Separate Storage by Name:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 128);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Storage On Appdata:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 104);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Automatically Join Invite:";
            // 
            // cmbOffline
            // 
            this.cmbOffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOffline.FormattingEnabled = true;
            this.cmbOffline.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbOffline.Location = new System.Drawing.Point(416, 293);
            this.cmbOffline.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOffline.Name = "cmbOffline";
            this.cmbOffline.Size = new System.Drawing.Size(122, 21);
            this.cmbOffline.TabIndex = 17;
            this.toolTip1.SetToolTip(this.cmbOffline, "Emulates steam offline mode.");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 45);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Name:";
            // 
            // cmbDisableGC
            // 
            this.cmbDisableGC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisableGC.FormattingEnabled = true;
            this.cmbDisableGC.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbDisableGC.Location = new System.Drawing.Point(435, 246);
            this.cmbDisableGC.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisableGC.Name = "cmbDisableGC";
            this.cmbDisableGC.Size = new System.Drawing.Size(103, 21);
            this.cmbDisableGC.TabIndex = 17;
            this.toolTip1.SetToolTip(this.cmbDisableGC, "Disable Game Coordinator (items) emulation.");
            // 
            // cmbFailOnNonStats
            // 
            this.cmbFailOnNonStats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFailOnNonStats.FormattingEnabled = true;
            this.cmbFailOnNonStats.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbFailOnNonStats.Location = new System.Drawing.Point(435, 223);
            this.cmbFailOnNonStats.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFailOnNonStats.Name = "cmbFailOnNonStats";
            this.cmbFailOnNonStats.Size = new System.Drawing.Size(103, 21);
            this.cmbFailOnNonStats.TabIndex = 17;
            this.toolTip1.SetToolTip(this.cmbFailOnNonStats, "If the game loops when reading stats/achievements, set this to true.");
            // 
            // cmbEnableVR
            // 
            this.cmbEnableVR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableVR.FormattingEnabled = true;
            this.cmbEnableVR.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbEnableVR.Location = new System.Drawing.Point(416, 175);
            this.cmbEnableVR.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableVR.Name = "cmbEnableVR";
            this.cmbEnableVR.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableVR.TabIndex = 16;
            this.toolTip1.SetToolTip(this.cmbEnableVR, "Tell the game that steam have virtual reality enabled.");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 17);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Steam ID:";
            // 
            // cmbEnableLobbyFilter
            // 
            this.cmbEnableLobbyFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableLobbyFilter.FormattingEnabled = true;
            this.cmbEnableLobbyFilter.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbEnableLobbyFilter.Location = new System.Drawing.Point(416, 150);
            this.cmbEnableLobbyFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableLobbyFilter.Name = "cmbEnableLobbyFilter";
            this.cmbEnableLobbyFilter.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableLobbyFilter.TabIndex = 15;
            this.toolTip1.SetToolTip(this.cmbEnableLobbyFilter, "Some game will filter the lobby by match type, skills, etc. Turning off will retu" +
        "rn all lobbies found to the game. This may result in unknown behaviour.");
            // 
            // cmbEnableIngameVoice
            // 
            this.cmbEnableIngameVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableIngameVoice.FormattingEnabled = true;
            this.cmbEnableIngameVoice.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbEnableIngameVoice.Location = new System.Drawing.Point(416, 126);
            this.cmbEnableIngameVoice.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableIngameVoice.Name = "cmbEnableIngameVoice";
            this.cmbEnableIngameVoice.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableIngameVoice.TabIndex = 14;
            this.toolTip1.SetToolTip(this.cmbEnableIngameVoice, "Enable in-game voice chat.");
            // 
            // cmbEnableHTTP
            // 
            this.cmbEnableHTTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableHTTP.FormattingEnabled = true;
            this.cmbEnableHTTP.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbEnableHTTP.Location = new System.Drawing.Point(416, 102);
            this.cmbEnableHTTP.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableHTTP.Name = "cmbEnableHTTP";
            this.cmbEnableHTTP.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableHTTP.TabIndex = 13;
            this.toolTip1.SetToolTip(this.cmbEnableHTTP, "If disabled, all calls to create HTTP request will fail. It is advised to keep th" +
        "is turned off.");
            // 
            // cmbQuickJoinHotkey
            // 
            this.cmbQuickJoinHotkey.FormattingEnabled = true;
            this.cmbQuickJoinHotkey.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "SHIFT + TAB"});
            this.cmbQuickJoinHotkey.Location = new System.Drawing.Point(156, 296);
            this.cmbQuickJoinHotkey.Margin = new System.Windows.Forms.Padding(2);
            this.cmbQuickJoinHotkey.Name = "cmbQuickJoinHotkey";
            this.cmbQuickJoinHotkey.Size = new System.Drawing.Size(122, 21);
            this.cmbQuickJoinHotkey.TabIndex = 12;
            this.cmbQuickJoinHotkey.Text = "<Use Global Setting>";
            this.toolTip1.SetToolTip(this.cmbQuickJoinHotkey, "When there isn\'t any lobby browser, or when lobby is created using private match," +
        " use this hotkey to join your partner game.");
            // 
            // cmbDisableLeaderboard
            // 
            this.cmbDisableLeaderboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisableLeaderboard.FormattingEnabled = true;
            this.cmbDisableLeaderboard.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbDisableLeaderboard.Location = new System.Drawing.Point(156, 271);
            this.cmbDisableLeaderboard.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisableLeaderboard.Name = "cmbDisableLeaderboard";
            this.cmbDisableLeaderboard.Size = new System.Drawing.Size(122, 21);
            this.cmbDisableLeaderboard.TabIndex = 11;
            this.toolTip1.SetToolTip(this.cmbDisableLeaderboard, "Don\'t return any leaderboard when game requested it. Disable this if the game han" +
        "gs when performing leaderboard operations.");
            // 
            // cmbDisableFriendList
            // 
            this.cmbDisableFriendList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisableFriendList.FormattingEnabled = true;
            this.cmbDisableFriendList.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbDisableFriendList.Location = new System.Drawing.Point(156, 247);
            this.cmbDisableFriendList.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisableFriendList.Name = "cmbDisableFriendList";
            this.cmbDisableFriendList.Size = new System.Drawing.Size(122, 21);
            this.cmbDisableFriendList.TabIndex = 10;
            this.toolTip1.SetToolTip(this.cmbDisableFriendList, "Don\'t tell games that you have friends (peoples that are connected to you).");
            // 
            // cmbSecuredServer
            // 
            this.cmbSecuredServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecuredServer.FormattingEnabled = true;
            this.cmbSecuredServer.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbSecuredServer.Location = new System.Drawing.Point(156, 223);
            this.cmbSecuredServer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSecuredServer.Name = "cmbSecuredServer";
            this.cmbSecuredServer.Size = new System.Drawing.Size(122, 21);
            this.cmbSecuredServer.TabIndex = 9;
            this.toolTip1.SetToolTip(this.cmbSecuredServer, "Create a VAC secured server. Disable this if the game complain it starts in insec" +
        "ure mode or demand you to remove any plugins.");
            // 
            // cmbRemoteStoragePath
            // 
            this.cmbRemoteStoragePath.FormattingEnabled = true;
            this.cmbRemoteStoragePath.Items.AddRange(new object[] {
            "Browse..."});
            this.cmbRemoteStoragePath.Location = new System.Drawing.Point(156, 175);
            this.cmbRemoteStoragePath.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRemoteStoragePath.Name = "cmbRemoteStoragePath";
            this.cmbRemoteStoragePath.Size = new System.Drawing.Size(122, 21);
            this.cmbRemoteStoragePath.TabIndex = 8;
            this.toolTip1.SetToolTip(this.cmbRemoteStoragePath, "Manually specify the remote path folder. This should not be changed.");
            this.cmbRemoteStoragePath.SelectedIndexChanged += new System.EventHandler(this.cmbRemoteStoragePath_SelectedIndexChanged);
            // 
            // cmbSeparateStorageByName
            // 
            this.cmbSeparateStorageByName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeparateStorageByName.FormattingEnabled = true;
            this.cmbSeparateStorageByName.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbSeparateStorageByName.Location = new System.Drawing.Point(156, 150);
            this.cmbSeparateStorageByName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSeparateStorageByName.Name = "cmbSeparateStorageByName";
            this.cmbSeparateStorageByName.Size = new System.Drawing.Size(122, 21);
            this.cmbSeparateStorageByName.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cmbSeparateStorageByName, "Each \"PersonaName\" will have its own storage folder for save game data.");
            // 
            // cmbStorageOnAppData
            // 
            this.cmbStorageOnAppData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageOnAppData.FormattingEnabled = true;
            this.cmbStorageOnAppData.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbStorageOnAppData.Location = new System.Drawing.Point(156, 126);
            this.cmbStorageOnAppData.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStorageOnAppData.Name = "cmbStorageOnAppData";
            this.cmbStorageOnAppData.Size = new System.Drawing.Size(122, 21);
            this.cmbStorageOnAppData.TabIndex = 6;
            this.toolTip1.SetToolTip(this.cmbStorageOnAppData, "Save game data on AppData\\Roaming\\SmartSteamEmu folder.");
            // 
            // cmbAutoJoinInvite
            // 
            this.cmbAutoJoinInvite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutoJoinInvite.FormattingEnabled = true;
            this.cmbAutoJoinInvite.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbAutoJoinInvite.Location = new System.Drawing.Point(156, 102);
            this.cmbAutoJoinInvite.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAutoJoinInvite.Name = "cmbAutoJoinInvite";
            this.cmbAutoJoinInvite.Size = new System.Drawing.Size(122, 21);
            this.cmbAutoJoinInvite.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cmbAutoJoinInvite, "When somebody invite you in-game, you will be joined to the game automatically wi" +
        "thout prompting. Disabling this will do nothing when someone invite you.");
            // 
            // cmbPersonaName
            // 
            this.cmbPersonaName.FormattingEnabled = true;
            this.cmbPersonaName.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "AccountName",
            "ComputerName"});
            this.cmbPersonaName.Location = new System.Drawing.Point(69, 42);
            this.cmbPersonaName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPersonaName.Name = "cmbPersonaName";
            this.cmbPersonaName.Size = new System.Drawing.Size(210, 21);
            this.cmbPersonaName.TabIndex = 1;
            this.cmbPersonaName.Text = "<Use Global Setting>";
            this.toolTip1.SetToolTip(this.cmbPersonaName, "Choose your name that will be appeared in-game and on friends list. You can type " +
        "your name here.");
            // 
            // cmbEmuSteamId
            // 
            this.cmbEmuSteamId.FormattingEnabled = true;
            this.cmbEmuSteamId.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "Static",
            "Random",
            "PersonaName",
            "ip",
            "GenerateRandom"});
            this.cmbEmuSteamId.Location = new System.Drawing.Point(69, 15);
            this.cmbEmuSteamId.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmuSteamId.Name = "cmbEmuSteamId";
            this.cmbEmuSteamId.Size = new System.Drawing.Size(210, 21);
            this.cmbEmuSteamId.TabIndex = 0;
            this.cmbEmuSteamId.Text = "<Use Global Setting>";
            this.toolTip1.SetToolTip(this.cmbEmuSteamId, "Choose your steam id. Some games requires this to be consistent for save game to " +
        "work correctly. You can also specify the steam id manually here.");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnNetDel);
            this.tabPage4.Controls.Add(this.btnNetAdd);
            this.tabPage4.Controls.Add(this.txtNetIp);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.lstNetBroadcast);
            this.tabPage4.Controls.Add(this.numNetMaxConn);
            this.tabPage4.Controls.Add(this.numNetDiscoverInterval);
            this.tabPage4.Controls.Add(this.numNetMaxPort);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.numNetPort);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.cmbNetUseGlobal);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(560, 323);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Networking";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnNetDel
            // 
            this.btnNetDel.Location = new System.Drawing.Point(507, 94);
            this.btnNetDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnNetDel.Name = "btnNetDel";
            this.btnNetDel.Size = new System.Drawing.Size(39, 25);
            this.btnNetDel.TabIndex = 8;
            this.btnNetDel.Text = "Del";
            this.btnNetDel.UseVisualStyleBackColor = true;
            this.btnNetDel.Click += new System.EventHandler(this.btnNetDel_Click);
            // 
            // btnNetAdd
            // 
            this.btnNetAdd.Location = new System.Drawing.Point(507, 64);
            this.btnNetAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnNetAdd.Name = "btnNetAdd";
            this.btnNetAdd.Size = new System.Drawing.Size(39, 25);
            this.btnNetAdd.TabIndex = 6;
            this.btnNetAdd.Text = "Add";
            this.btnNetAdd.UseVisualStyleBackColor = true;
            this.btnNetAdd.Click += new System.EventHandler(this.btnNetAdd_Click);
            // 
            // txtNetIp
            // 
            this.txtNetIp.Location = new System.Drawing.Point(376, 67);
            this.txtNetIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtNetIp.Name = "txtNetIp";
            this.txtNetIp.Size = new System.Drawing.Size(127, 20);
            this.txtNetIp.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtNetIp, "Broadcast address or your partner IP address to connect to.");
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(264, 67);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(110, 13);
            this.label27.TabIndex = 7;
            this.label27.Text = "Broadcast/Partner IP:";
            // 
            // lstNetBroadcast
            // 
            this.lstNetBroadcast.ContextMenuStrip = this.dlcContextMenu;
            this.lstNetBroadcast.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstNetBroadcast.FormattingEnabled = true;
            this.lstNetBroadcast.ItemHeight = 16;
            this.lstNetBroadcast.Location = new System.Drawing.Point(266, 93);
            this.lstNetBroadcast.Margin = new System.Windows.Forms.Padding(2);
            this.lstNetBroadcast.Name = "lstNetBroadcast";
            this.lstNetBroadcast.Size = new System.Drawing.Size(237, 212);
            this.lstNetBroadcast.TabIndex = 7;
            this.lstNetBroadcast.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstNetBroadcast_DrawItem);
            // 
            // numNetMaxConn
            // 
            this.numNetMaxConn.Location = new System.Drawing.Point(112, 145);
            this.numNetMaxConn.Margin = new System.Windows.Forms.Padding(2);
            this.numNetMaxConn.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numNetMaxConn.Name = "numNetMaxConn";
            this.numNetMaxConn.Size = new System.Drawing.Size(128, 20);
            this.numNetMaxConn.TabIndex = 4;
            this.numNetMaxConn.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numNetDiscoverInterval
            // 
            this.numNetDiscoverInterval.Location = new System.Drawing.Point(112, 119);
            this.numNetDiscoverInterval.Margin = new System.Windows.Forms.Padding(2);
            this.numNetDiscoverInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numNetDiscoverInterval.Name = "numNetDiscoverInterval";
            this.numNetDiscoverInterval.Size = new System.Drawing.Size(128, 20);
            this.numNetDiscoverInterval.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numNetDiscoverInterval, "How many seconds before sending another discovery packet to the broadcast address" +
        ".");
            this.numNetDiscoverInterval.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numNetMaxPort
            // 
            this.numNetMaxPort.Location = new System.Drawing.Point(112, 93);
            this.numNetMaxPort.Margin = new System.Windows.Forms.Padding(2);
            this.numNetMaxPort.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numNetMaxPort.Name = "numNetMaxPort";
            this.numNetMaxPort.Size = new System.Drawing.Size(128, 20);
            this.numNetMaxPort.TabIndex = 2;
            this.toolTip1.SetToolTip(this.numNetMaxPort, "How many attempt to use next port if the previous one is already taken.");
            this.numNetMaxPort.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(16, 146);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(87, 13);
            this.label34.TabIndex = 3;
            this.label34.Text = "Max Connection:";
            // 
            // numNetPort
            // 
            this.numNetPort.Location = new System.Drawing.Point(112, 67);
            this.numNetPort.Margin = new System.Windows.Forms.Padding(2);
            this.numNetPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numNetPort.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numNetPort.Name = "numNetPort";
            this.numNetPort.Size = new System.Drawing.Size(128, 20);
            this.numNetPort.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numNetPort, "Communication port used by this emulator, must match with other player or it won\'" +
        "t find each other.");
            this.numNetPort.Value = new decimal(new int[] {
            31313,
            0,
            0,
            0});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(16, 120);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 13);
            this.label26.TabIndex = 3;
            this.label26.Text = "Discovery Interval:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(16, 95);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Maximum Port:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 69);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "Port:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 20);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Networking:";
            // 
            // cmbNetUseGlobal
            // 
            this.cmbNetUseGlobal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetUseGlobal.FormattingEnabled = true;
            this.cmbNetUseGlobal.Items.AddRange(new object[] {
            "<Use Global Settings>",
            "<Use Custom Settings>"});
            this.cmbNetUseGlobal.Location = new System.Drawing.Point(82, 17);
            this.cmbNetUseGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.cmbNetUseGlobal.Name = "cmbNetUseGlobal";
            this.cmbNetUseGlobal.Size = new System.Drawing.Size(158, 21);
            this.cmbNetUseGlobal.TabIndex = 0;
            this.cmbNetUseGlobal.SelectedIndexChanged += new System.EventHandler(this.cmbNetUseGlobal_SelectedIndexChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label43);
            this.tabPage5.Controls.Add(this.cmbExtLogging);
            this.tabPage5.Controls.Add(this.label42);
            this.tabPage5.Controls.Add(this.cmbExtOnlineKey);
            this.tabPage5.Controls.Add(this.txtExtras);
            this.tabPage5.Controls.Add(this.label40);
            this.tabPage5.Controls.Add(this.label35);
            this.tabPage5.Controls.Add(this.label41);
            this.tabPage5.Controls.Add(this.label36);
            this.tabPage5.Controls.Add(this.cmbExtOnlinePlay);
            this.tabPage5.Controls.Add(this.cmbExtHookRefCount);
            this.tabPage5.Controls.Add(this.cmbExtOverlay);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(560, 323);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Extra";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(271, 45);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(113, 13);
            this.label43.TabIndex = 16;
            this.label43.Text = "Enable debug logging:";
            // 
            // cmbExtLogging
            // 
            this.cmbExtLogging.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtLogging.FormattingEnabled = true;
            this.cmbExtLogging.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbExtLogging.Location = new System.Drawing.Point(417, 42);
            this.cmbExtLogging.Margin = new System.Windows.Forms.Padding(2);
            this.cmbExtLogging.Name = "cmbExtLogging";
            this.cmbExtLogging.Size = new System.Drawing.Size(122, 21);
            this.cmbExtLogging.TabIndex = 15;
            this.toolTip1.SetToolTip(this.cmbExtLogging, "Enable debug logging, open SmartSteamEmu.log to see it. Warning: It may slow down" +
        " game depending on how often it does steam calls.");
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(17, 65);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(61, 13);
            this.label42.TabIndex = 13;
            this.label42.Text = "Online Key:";
            // 
            // cmbExtOnlineKey
            // 
            this.cmbExtOnlineKey.FormattingEnabled = true;
            this.cmbExtOnlineKey.Items.AddRange(new object[] {
            "<Use Global Setting>"});
            this.cmbExtOnlineKey.Location = new System.Drawing.Point(121, 63);
            this.cmbExtOnlineKey.Margin = new System.Windows.Forms.Padding(2);
            this.cmbExtOnlineKey.Name = "cmbExtOnlineKey";
            this.cmbExtOnlineKey.Size = new System.Drawing.Size(122, 21);
            this.cmbExtOnlineKey.TabIndex = 3;
            this.cmbExtOnlineKey.Text = "<Use Global Setting>";
            this.toolTip1.SetToolTip(this.cmbExtOnlineKey, "Set online key. By default, you are connected to players that play the same game." +
        " With OnlineKey set, you can find only players that uses the same key.");
            // 
            // txtExtras
            // 
            this.txtExtras.AcceptsReturn = true;
            this.txtExtras.Font = new System.Drawing.Font("Consolas", 9.310345F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtras.Location = new System.Drawing.Point(20, 110);
            this.txtExtras.Margin = new System.Windows.Forms.Padding(2);
            this.txtExtras.Multiline = true;
            this.txtExtras.Name = "txtExtras";
            this.txtExtras.Size = new System.Drawing.Size(519, 206);
            this.txtExtras.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtExtras, "Add extra settings to [SmartSteamEmu] section.");
            this.txtExtras.WordWrap = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(17, 94);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(91, 13);
            this.label40.TabIndex = 11;
            this.label40.Text = "[SmartSteamEmu]";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(17, 42);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(99, 13);
            this.label35.TabIndex = 7;
            this.label35.Text = "Enable Online Play:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(271, 18);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(151, 13);
            this.label41.TabIndex = 8;
            this.label41.Text = "Overlay reference count hook:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(17, 18);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(80, 13);
            this.label36.TabIndex = 8;
            this.label36.Text = "Enable overlay:";
            // 
            // cmbExtOnlinePlay
            // 
            this.cmbExtOnlinePlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtOnlinePlay.FormattingEnabled = true;
            this.cmbExtOnlinePlay.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbExtOnlinePlay.Location = new System.Drawing.Point(121, 39);
            this.cmbExtOnlinePlay.Margin = new System.Windows.Forms.Padding(2);
            this.cmbExtOnlinePlay.Name = "cmbExtOnlinePlay";
            this.cmbExtOnlinePlay.Size = new System.Drawing.Size(122, 21);
            this.cmbExtOnlinePlay.TabIndex = 2;
            this.toolTip1.SetToolTip(this.cmbExtOnlinePlay, "Enable searching people online, requires SSEOverlay plugin.");
            // 
            // cmbExtHookRefCount
            // 
            this.cmbExtHookRefCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtHookRefCount.FormattingEnabled = true;
            this.cmbExtHookRefCount.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbExtHookRefCount.Location = new System.Drawing.Point(417, 15);
            this.cmbExtHookRefCount.Margin = new System.Windows.Forms.Padding(2);
            this.cmbExtHookRefCount.Name = "cmbExtHookRefCount";
            this.cmbExtHookRefCount.Size = new System.Drawing.Size(122, 21);
            this.cmbExtHookRefCount.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cmbExtHookRefCount, resources.GetString("cmbExtHookRefCount.ToolTip"));
            // 
            // cmbExtOverlay
            // 
            this.cmbExtOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtOverlay.FormattingEnabled = true;
            this.cmbExtOverlay.Items.AddRange(new object[] {
            "<Use Global Setting>",
            "True",
            "False"});
            this.cmbExtOverlay.Location = new System.Drawing.Point(121, 15);
            this.cmbExtOverlay.Margin = new System.Windows.Forms.Padding(2);
            this.cmbExtOverlay.Name = "cmbExtOverlay";
            this.cmbExtOverlay.Size = new System.Drawing.Size(122, 21);
            this.cmbExtOverlay.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cmbExtOverlay, "Enable overlay in-game, requires SSEOverlay plugin.");
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnDpDel);
            this.tabPage6.Controls.Add(this.btnDpAdd);
            this.tabPage6.Controls.Add(this.txtDpEntry);
            this.tabPage6.Controls.Add(this.label38);
            this.tabPage6.Controls.Add(this.label37);
            this.tabPage6.Controls.Add(this.lstDp);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(560, 323);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Direct Patch";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnDpDel
            // 
            this.btnDpDel.Location = new System.Drawing.Point(506, 122);
            this.btnDpDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDpDel.Name = "btnDpDel";
            this.btnDpDel.Size = new System.Drawing.Size(39, 25);
            this.btnDpDel.TabIndex = 13;
            this.btnDpDel.Text = "Del";
            this.btnDpDel.UseVisualStyleBackColor = true;
            this.btnDpDel.Click += new System.EventHandler(this.btnDpDel_Click);
            // 
            // btnDpAdd
            // 
            this.btnDpAdd.Location = new System.Drawing.Point(506, 92);
            this.btnDpAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnDpAdd.Name = "btnDpAdd";
            this.btnDpAdd.Size = new System.Drawing.Size(39, 25);
            this.btnDpAdd.TabIndex = 10;
            this.btnDpAdd.Text = "Add";
            this.btnDpAdd.UseVisualStyleBackColor = true;
            this.btnDpAdd.Click += new System.EventHandler(this.btnDpAdd_Click);
            // 
            // txtDpEntry
            // 
            this.txtDpEntry.Location = new System.Drawing.Point(50, 95);
            this.txtDpEntry.Margin = new System.Windows.Forms.Padding(2);
            this.txtDpEntry.Name = "txtDpEntry";
            this.txtDpEntry.Size = new System.Drawing.Size(453, 20);
            this.txtDpEntry.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtDpEntry, "Patch modules entry where you can patch game files on memory.");
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label38.Location = new System.Drawing.Point(14, 13);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(334, 65);
            this.label38.TabIndex = 11;
            this.label38.Text = "ModuleName.ext=Location;OriginalValue;PatchValue\r\nmymodule.dll=12ab;74fe;ebfe\r\nmy" +
    "module.dll=*;74fe;ebfe\r\nmymodule.dll=*;74??;eb\r\nWhere location can be absolute a" +
    "ddress or asterisks (search memory)";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(14, 95);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(34, 13);
            this.label37.TabIndex = 11;
            this.label37.Text = "Entry:";
            // 
            // lstDp
            // 
            this.lstDp.ContextMenuStrip = this.dlcContextMenu;
            this.lstDp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDp.FormattingEnabled = true;
            this.lstDp.ItemHeight = 16;
            this.lstDp.Location = new System.Drawing.Point(16, 118);
            this.lstDp.Margin = new System.Windows.Forms.Padding(2);
            this.lstDp.Name = "lstDp";
            this.lstDp.Size = new System.Drawing.Size(487, 180);
            this.lstDp.TabIndex = 12;
            this.lstDp.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstDp_DrawItem);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(476, 364);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(374, 364);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateShortcut
            // 
            this.btnCreateShortcut.Location = new System.Drawing.Point(12, 365);
            this.btnCreateShortcut.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateShortcut.Name = "btnCreateShortcut";
            this.btnCreateShortcut.Size = new System.Drawing.Size(179, 26);
            this.btnCreateShortcut.TabIndex = 3;
            this.btnCreateShortcut.Text = "Create Desktop Shortcut";
            this.btnCreateShortcut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateShortcut.UseVisualStyleBackColor = true;
            this.btnCreateShortcut.Click += new System.EventHandler(this.btnCreateShortcut_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // FrmAppSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 401);
            this.Controls.Add(this.btnCreateShortcut);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAppSetting";
            this.ShowInTaskbar = false;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.FrmAppSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb86)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.dlcContextMenu.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetDiscoverInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetPort)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);

		}

        private void txtDlcAppId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddFromClipboard_Click(object sender, EventArgs e)
        {
            var dlcList = new List<KVDlc<string, string>>();
            using (var strReader = new StringReader(Clipboard.GetText()))
            {
                while (true)
                {
                    var line = strReader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    if (line.IndexOf('=') == -1)
                    {
                        continue;
                    }

                    var parts = line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                    {
                        continue;
                    }

                    var dlcid = parts[0].Trim();
                    var dlcname = parts[1].Trim();

                    if (!Int32.TryParse(dlcid, out int numValue))
                    {
                        // dlcid is not a number
                        continue;
                    }

                    dlcList.Add(new KVDlc<string, string>(dlcid, dlcname));
                }
            }

            if (dlcList.Count < 1)
            {
                MessageBox.Show("Couldn't find any dlc in valid form inside the clipboard.", "Nothing added", MessageBoxButtons.OK);
                return;
            }


            var removeList = new List<KVDlc<string, string>>();

            // Gather entries that will be replaced
            foreach (var dlc in m_TempDlcList)
            {
                foreach (var newdlc in dlcList)
                {
                    if (dlc.DlcId == newdlc.DlcId)
                    {
                        removeList.Add(dlc);
                        break;
                    }
                }
            }

            // Remove old entries
            foreach (var dlc in removeList)
            {
                m_TempDlcList.Remove(dlc);
            }

            m_TempDlcList.AddRange(dlcList);

            // Clear and repopulate list box
            lstDlc.Items.Clear();
            foreach (var dlc in m_TempDlcList)
            {
                lstDlc.Items.Add(dlc.DlcId + " = " + dlc.DlcName);
            }

            MessageBox.Show(String.Format("Added {0} DLCs from clipboard.", dlcList.Count), "Added", MessageBoxButtons.OK);
        }

        private void btnDelAllDlc_Click(object sender, EventArgs e)
        {
            m_TempDlcList.Clear();
            lstDlc.Items.Clear();
        }
    }
}
