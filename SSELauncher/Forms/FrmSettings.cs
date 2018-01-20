using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SSELauncher
{
	public class FrmSettings : Form
	{
		private CConfig m_Conf;

		private string m_TempAvatarPath;

		private IContainer components;

		private Button btnCancel;

		private Button btnOK;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private Label label22;

		private Label label10;

		private Label label9;

		private ComboBox cmbEmuQuickJoin;

		private ComboBox cmbEmuPersonaName;

		private ComboBox cmbEmuSteamId;

		private TabPage tabPage2;

		private Label label1;

		private Label label19;

		private Label label18;

		private Label label17;

		private Label label16;

		private Label label21;

		private Label label20;

		private Label label14;

		private Label label13;

		private Label label11;

		private ComboBox cmbEnableLog;

		private ComboBox cmbEnableVR;

		private ComboBox cmbEnableLobbyFilter;

		private ComboBox cmbEnableIngameVoice;

		private ComboBox cmbEnableHttp;

		private ComboBox cmbDisableFriendList;

		private ComboBox cmbSecuredServer;

		private ComboBox cmbSeparateStorageByName;

		private ComboBox cmbStorageOnAppData;

		private ComboBox cmbAutoJoinInvite;

		private Label lblChangeAvatar;

		private PictureBox pbAvatar;

		private TabPage tabPage3;

		private Button btnNetDelIp;

		private Button btnNetAddIp;

		private TextBox txtNetIp;

		private Label label27;

		private ListBox lstNetBroadcast;

		private NumericUpDown numNetDiscoveryInterval;

		private NumericUpDown numNetMaxPort;

		private NumericUpDown numNetListenPort;

		private Label label26;

		private Label label25;

		private Label label24;

		private ToolTip toolTip1;

		private TabPage tabPage4;

		private Button btnDelMasterServer;

		private Button btnAddMasterServer;

		private TextBox txtMasterServerIp;

		private Label label28;

		private ListBox lstMasterServer;

		private TabPage tabPage5;

		private GroupBox groupBox1;

		private Button btnBanDel;

		private Button btnBanAdd;

		private TextBox txtBan;

		private Label label2;

		private ListBox lstBan;

		private CheckBox chkAllowAnyToConnect;

		private Label label3;

		private ComboBox cmbDisableLeaderboard;

		private Label label4;

		private ComboBox cmbParanoidMode;

		private TextBox txtAdminPass;

		private Label label5;

		private CheckBox chkHideToTray;

		private NumericUpDown numNetMaxConn;

		private Label label34;

		private Label label8;

		private ComboBox cmbOnlinePlay;

		private Label label7;

		private ComboBox cmbOverlay;

		private Label label6;

		private ComboBox cmbLang;

		private Label label12;

		private ComboBox cmbOffline;

		private Label label41;

		private ComboBox cmbOverlayLang;

		private Label label15;

		private ComboBox cmbOverlayScreenshot;

		private TextBox txtOnlineKey;

		private Label label42;

		private ContextMenuStrip contextMenuStrip1;
        private CheckBox chkCleanLog;
        private ToolStripMenuItem enableDisableToolStripMenuItem;

		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		private static extern uint GetPrivateProfileSectionNamesW(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

		public FrmSettings()
		{
            InitializeComponent();
		}

		public void SetConfig(CConfig config)
		{
            m_Conf = config;
            RepopulateLanguage();
            cmbEmuSteamId.SelectedIndex = -1;
            cmbEmuSteamId.Text = (string.Equals(config.SteamIdGeneration, "Manual", StringComparison.OrdinalIgnoreCase) ? config.ManualSteamId.ToString() : config.SteamIdGeneration);
            cmbEmuPersonaName.SelectedIndex = -1;
            cmbEmuPersonaName.Text = config.PersonaName;
            cmbEmuQuickJoin.SelectedIndex = -1;
            cmbEmuQuickJoin.Text = config.QuickJoinHotkey;
            m_TempAvatarPath = CApp.GetAbsolutePath(config.AvatarPath);
            cmbParanoidMode.Text = m_Conf.ParanoidMode.ToString();
            chkHideToTray.Checked = m_Conf.HideToTray;
            cmbLang.Text = m_Conf.Language;
            cmbOverlay.Text = m_Conf.EnableOverlay.ToString();
            cmbOnlinePlay.Text = m_Conf.EnableOnlinePlay.ToString();
            cmbOverlayLang.Text = m_Conf.OverlayLanguage;
            cmbOverlayScreenshot.SelectedIndex = -1;
            cmbOverlayScreenshot.Text = m_Conf.OverlayScreenshotHotkey;
			try
			{
				if (config.AvatarPath == "avatar.png")
				{
                    pbAvatar.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SmartSteamEmu\\SmartSteamEmu\\Common\\" + config.AvatarPath));
				}
				else
				{
                    pbAvatar.Image = Image.FromFile(m_TempAvatarPath);
				}
			}
			catch
			{
			}
            cmbAutoJoinInvite.Text = config.AutomaticallyJoinInvite.ToString();
            cmbStorageOnAppData.Text = config.StorageOnAppdata.ToString();
            cmbSeparateStorageByName.Text = config.SeparateStorageByName.ToString();
            cmbSecuredServer.Text = config.SecuredServer.ToString();
            cmbDisableFriendList.Text = config.DisableFriendList.ToString();
            cmbDisableLeaderboard.Text = config.DisableLeaderboard.ToString();
            cmbEnableHttp.Text = config.EnableHTTP.ToString();
            cmbEnableIngameVoice.Text = config.EnableInGameVoice.ToString();
            cmbEnableLobbyFilter.Text = config.EnableLobbyFilter.ToString();
            cmbEnableVR.Text = config.EnableVR.ToString();
            cmbOffline.Text = config.Offline.ToString();
            txtOnlineKey.Text = config.OnlineKey;
			foreach (string current in config.MasterServerAddress)
			{
                lstMasterServer.Items.Add(current);
			}
            cmbEnableLog.Text = config.EnableLog.ToString();
            chkCleanLog.Checked = config.CleanLog;

            numNetListenPort.Value = config.ListenPort;
            numNetMaxPort.Value = config.MaximumPort;
            numNetDiscoveryInterval.Value = config.DiscoveryInterval;
            numNetMaxConn.Value = config.MaximumConnection;
			foreach (string current2 in config.BroadcastAddress)
			{
                lstNetBroadcast.Items.Add(current2);
			}
            chkAllowAnyToConnect.Checked = config.AllowAnyoneConnect;
            txtAdminPass.Text = config.AdminPass;
			foreach (string current3 in config.BanList)
			{
                lstBan.Items.Add(current3);
			}
		}

		private void RepopulateLanguage()
		{
			string lpFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SmartSteamEmu\\SmartSteamEmu\\Plugins\\SSEOverlay\\Language.ini");
			IntPtr intPtr = Marshal.AllocCoTaskMem(65534);
			uint privateProfileSectionNamesW = FrmSettings.GetPrivateProfileSectionNamesW(intPtr, 32767u, lpFileName);
			if (privateProfileSectionNamesW == 0u || privateProfileSectionNamesW == 32765u)
			{
				Marshal.FreeCoTaskMem(intPtr);
				return;
			}
			string text = Marshal.PtrToStringUni(intPtr, (int)privateProfileSectionNamesW).ToString();
			Marshal.FreeCoTaskMem(intPtr);
			string[] array = text.Substring(0, text.Length - 1).Split(new char[1]);
			if (array.Count<string>() > 0)
			{
                cmbOverlayLang.Items.Clear();
                cmbOverlayLang.Items.Add("");
                cmbOverlayLang.Items.Add("English");
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					if (!text2.Equals("English", StringComparison.OrdinalIgnoreCase))
					{
                        cmbOverlayLang.Items.Add(text2);
					}
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (string.Equals(cmbEmuSteamId.Text, "Static", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "Random", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "PersonaName", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "ip", StringComparison.OrdinalIgnoreCase) || string.Equals(cmbEmuSteamId.Text, "GenerateRandom", StringComparison.OrdinalIgnoreCase))
			{
                m_Conf.SteamIdGeneration = cmbEmuSteamId.Text;
			}
			else
			{
				try
				{
                    m_Conf.ManualSteamId = Convert.ToInt64(cmbEmuSteamId.Text);
                    m_Conf.SteamIdGeneration = "Manual";
				}
				catch
				{
					MessageBox.Show("Invalid steam id!", "Invalid input");
				}
			}
            m_Conf.PersonaName = cmbEmuPersonaName.Text;
            m_Conf.AvatarPath = CApp.MakeRelativePath(m_TempAvatarPath, false);
            m_Conf.QuickJoinHotkey = cmbEmuQuickJoin.Text;
            m_Conf.ParanoidMode = Convert.ToBoolean(cmbParanoidMode.Text);
            m_Conf.HideToTray = chkHideToTray.Checked;
            m_Conf.Language = cmbLang.Text;
            m_Conf.EnableOverlay = Convert.ToBoolean(cmbOverlay.Text);
            m_Conf.EnableOnlinePlay = Convert.ToBoolean(cmbOnlinePlay.Text);
            m_Conf.OverlayLanguage = cmbOverlayLang.Text;
            m_Conf.OverlayScreenshotHotkey = cmbOverlayScreenshot.Text;
            m_Conf.AutomaticallyJoinInvite = Convert.ToBoolean(cmbAutoJoinInvite.Text);
            m_Conf.StorageOnAppdata = Convert.ToBoolean(cmbStorageOnAppData.Text);
            m_Conf.SeparateStorageByName = Convert.ToBoolean(cmbSeparateStorageByName.Text);
            m_Conf.SecuredServer = Convert.ToBoolean(cmbSecuredServer.Text);
            m_Conf.DisableFriendList = Convert.ToBoolean(cmbDisableFriendList.Text);
            m_Conf.DisableLeaderboard = Convert.ToBoolean(cmbDisableLeaderboard.Text);
            m_Conf.EnableHTTP = Convert.ToBoolean(cmbEnableHttp.Text);
            m_Conf.EnableInGameVoice = Convert.ToBoolean(cmbEnableIngameVoice.Text);
            m_Conf.EnableLobbyFilter = Convert.ToBoolean(cmbEnableLobbyFilter.Text);
            m_Conf.EnableVR = Convert.ToBoolean(cmbEnableVR.Text);
            m_Conf.Offline = Convert.ToBoolean(cmbOffline.Text);
            m_Conf.OnlineKey = (string.IsNullOrWhiteSpace(txtOnlineKey.Text) ? null : txtOnlineKey.Text);
            m_Conf.MasterServerAddress.Clear();
			foreach (string item in lstMasterServer.Items)
			{
                m_Conf.MasterServerAddress.Add(item);
			}
            m_Conf.EnableLog = Convert.ToBoolean(cmbEnableLog.Text);
            m_Conf.CleanLog = chkCleanLog.Checked;
            m_Conf.ListenPort = Convert.ToInt32(numNetListenPort.Value);
            m_Conf.MaximumPort = Convert.ToInt32(numNetMaxPort.Value);
            m_Conf.DiscoveryInterval = Convert.ToInt32(numNetDiscoveryInterval.Value);
            m_Conf.MaximumConnection = Convert.ToInt32(numNetMaxConn.Value);
            m_Conf.BroadcastAddress.Clear();
			foreach (string item2 in lstNetBroadcast.Items)
			{
                m_Conf.BroadcastAddress.Add(item2);
			}
            m_Conf.AllowAnyoneConnect = chkAllowAnyToConnect.Checked;
            m_Conf.AdminPass = txtAdminPass.Text;
            m_Conf.BanList.Clear();
			foreach (string item3 in lstBan.Items)
			{
                m_Conf.BanList.Add(item3);
			}
			base.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		private void FrmSettings_Load(object sender, EventArgs e)
		{
			base.AcceptButton = btnOK;
			base.CancelButton = btnCancel;
		}

		private void DoShowAvatarDlg()
		{
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Portable Network Graphics (*.png)|*.png|All Files|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
                    m_TempAvatarPath = openFileDialog.FileName;
                    pbAvatar.Image = Image.FromFile(openFileDialog.FileName);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Unable to show image");
				}
			}
		}

		private void lblChangeAvatar_Click(object sender, EventArgs e)
		{
            DoShowAvatarDlg();
		}

		private void pbAvatar_Click(object sender, EventArgs e)
		{
            DoShowAvatarDlg();
		}

		private void btnNetAddIp_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtNetIp.Text) || string.IsNullOrWhiteSpace(txtNetIp.Text))
			{
				return;
			}
            lstNetBroadcast.Items.Add(txtNetIp.Text);
            txtNetIp.Text = "";
		}

		private void btnNetDelIp_Click(object sender, EventArgs e)
		{
			if (lstNetBroadcast.SelectedIndex == -1)
			{
				return;
			}
            lstNetBroadcast.Items.RemoveAt(lstNetBroadcast.SelectedIndex);
		}

		private void btnAddMasterServer_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtMasterServerIp.Text) || string.IsNullOrWhiteSpace(txtMasterServerIp.Text))
			{
				return;
			}
            lstMasterServer.Items.Add(txtMasterServerIp.Text);
            txtMasterServerIp.Text = "";
		}

		private void btnDelMasterServer_Click(object sender, EventArgs e)
		{
			if (lstMasterServer.SelectedIndex == -1)
			{
				return;
			}
            lstMasterServer.Items.RemoveAt(lstMasterServer.SelectedIndex);
		}

		private void btnBanAdd_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtBan.Text) || string.IsNullOrWhiteSpace(txtBan.Text))
			{
				return;
			}
            lstBan.Items.Add(txtBan.Text);
            txtBan.Text = "";
		}

		private void btnBanDel_Click(object sender, EventArgs e)
		{
			if (lstBan.SelectedIndex == -1)
			{
				return;
			}
            lstBan.Items.RemoveAt(lstBan.SelectedIndex);
		}

		private void enableDisableToolStripMenuItem_Click(object sender, EventArgs e)
		{
            Control control = null;

            if (sender is ToolStripItem toolStripItem)
            {
                if (toolStripItem.Owner is ContextMenuStrip contextMenuStrip)
                {
                    control = contextMenuStrip.SourceControl;
                }
            }

            if (control == null)
			{
				return;
			}

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

		private void lstBan_DrawItem(object sender, DrawItemEventArgs e)
		{
			try
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				string text = lstBan.Items[e.Index].ToString();
				if (text.Length > 0 && text[0] == ';')
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(-31108)), e.Bounds);
				}
				graphics.DrawString(lstBan.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF((float)e.Bounds.X, (float)e.Bounds.Y));
				e.DrawFocusRectangle();
			}
			catch (Exception)
			{
			}
		}

		private void lstMasterServer_DrawItem(object sender, DrawItemEventArgs e)
		{
			try
			{
				e.DrawBackground();
				Graphics graphics = e.Graphics;
				string text = lstMasterServer.Items[e.Index].ToString();
				if (text.Length > 0 && text[0] == ';')
				{
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(-31108)), e.Bounds);
				}
				graphics.DrawString(lstMasterServer.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new PointF((float)e.Bounds.X, (float)e.Bounds.Y));
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbOverlayScreenshot = new System.Windows.Forms.ComboBox();
            this.cmbOverlayLang = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbOnlinePlay = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOverlay = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.chkHideToTray = new System.Windows.Forms.CheckBox();
            this.pbAvatar = new System.Windows.Forms.PictureBox();
            this.lblChangeAvatar = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEmuQuickJoin = new System.Windows.Forms.ComboBox();
            this.cmbParanoidMode = new System.Windows.Forms.ComboBox();
            this.cmbEmuPersonaName = new System.Windows.Forms.ComboBox();
            this.cmbEmuSteamId = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkCleanLog = new System.Windows.Forms.CheckBox();
            this.txtOnlineKey = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbOffline = new System.Windows.Forms.ComboBox();
            this.cmbEnableLog = new System.Windows.Forms.ComboBox();
            this.cmbEnableVR = new System.Windows.Forms.ComboBox();
            this.cmbEnableLobbyFilter = new System.Windows.Forms.ComboBox();
            this.cmbEnableIngameVoice = new System.Windows.Forms.ComboBox();
            this.cmbEnableHttp = new System.Windows.Forms.ComboBox();
            this.cmbDisableLeaderboard = new System.Windows.Forms.ComboBox();
            this.cmbDisableFriendList = new System.Windows.Forms.ComboBox();
            this.cmbSecuredServer = new System.Windows.Forms.ComboBox();
            this.cmbSeparateStorageByName = new System.Windows.Forms.ComboBox();
            this.cmbStorageOnAppData = new System.Windows.Forms.ComboBox();
            this.cmbAutoJoinInvite = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.numNetMaxConn = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.btnNetDelIp = new System.Windows.Forms.Button();
            this.btnNetAddIp = new System.Windows.Forms.Button();
            this.txtNetIp = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.lstNetBroadcast = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableDisableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numNetDiscoveryInterval = new System.Windows.Forms.NumericUpDown();
            this.numNetMaxPort = new System.Windows.Forms.NumericUpDown();
            this.numNetListenPort = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnDelMasterServer = new System.Windows.Forms.Button();
            this.btnAddMasterServer = new System.Windows.Forms.Button();
            this.txtMasterServerIp = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.lstMasterServer = new System.Windows.Forms.ListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBanDel = new System.Windows.Forms.Button();
            this.btnBanAdd = new System.Windows.Forms.Button();
            this.txtBan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstBan = new System.Windows.Forms.ListBox();
            this.chkAllowAnyToConnect = new System.Windows.Forms.CheckBox();
            this.txtAdminPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxConn)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNetDiscoveryInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetListenPort)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(364, 306);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(467, 306);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 292);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.cmbOverlayScreenshot);
            this.tabPage1.Controls.Add(this.cmbOverlayLang);
            this.tabPage1.Controls.Add(this.label41);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.cmbOnlinePlay);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.cmbOverlay);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.cmbLang);
            this.tabPage1.Controls.Add(this.chkHideToTray);
            this.tabPage1.Controls.Add(this.pbAvatar);
            this.tabPage1.Controls.Add(this.lblChangeAvatar);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.cmbEmuQuickJoin);
            this.tabPage1.Controls.Add(this.cmbParanoidMode);
            this.tabPage1.Controls.Add(this.cmbEmuPersonaName);
            this.tabPage1.Controls.Add(this.cmbEmuSteamId);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(552, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(118, 228);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 13);
            this.label15.TabIndex = 66;
            this.label15.Text = "Screenshot Hotkey:";
            // 
            // cmbOverlayScreenshot
            // 
            this.cmbOverlayScreenshot.FormattingEnabled = true;
            this.cmbOverlayScreenshot.Items.AddRange(new object[] {
            "F12"});
            this.cmbOverlayScreenshot.Location = new System.Drawing.Point(237, 225);
            this.cmbOverlayScreenshot.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOverlayScreenshot.Name = "cmbOverlayScreenshot";
            this.cmbOverlayScreenshot.Size = new System.Drawing.Size(122, 21);
            this.cmbOverlayScreenshot.TabIndex = 8;
            this.cmbOverlayScreenshot.Text = "F12";
            this.toolTip1.SetToolTip(this.cmbOverlayScreenshot, "Set overlay screenshot hotkey to capture the game screen.");
            // 
            // cmbOverlayLang
            // 
            this.cmbOverlayLang.Items.AddRange(new object[] {
            "",
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
            "Serbian (latin)",
            "Simplified Chinese",
            "Spanish",
            "Swedish",
            "Thai",
            "Traditional Chinese",
            "Turkish",
            "Ukrainian"});
            this.cmbOverlayLang.Location = new System.Drawing.Point(237, 201);
            this.cmbOverlayLang.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOverlayLang.Name = "cmbOverlayLang";
            this.cmbOverlayLang.Size = new System.Drawing.Size(122, 21);
            this.cmbOverlayLang.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cmbOverlayLang, "Override overlay language. Keeps empty to use default language.");
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(118, 203);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(97, 13);
            this.label41.TabIndex = 63;
            this.label41.Text = "Overlay Language:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(118, 180);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 61;
            this.label8.Text = "Online Play:";
            // 
            // cmbOnlinePlay
            // 
            this.cmbOnlinePlay.FormattingEnabled = true;
            this.cmbOnlinePlay.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbOnlinePlay.Location = new System.Drawing.Point(237, 177);
            this.cmbOnlinePlay.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOnlinePlay.Name = "cmbOnlinePlay";
            this.cmbOnlinePlay.Size = new System.Drawing.Size(122, 21);
            this.cmbOnlinePlay.TabIndex = 6;
            this.cmbOnlinePlay.Text = "True";
            this.toolTip1.SetToolTip(this.cmbOnlinePlay, "Enable searching people online, requires SSEOverlay plugin.");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(118, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Overlay:";
            // 
            // cmbOverlay
            // 
            this.cmbOverlay.FormattingEnabled = true;
            this.cmbOverlay.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbOverlay.Location = new System.Drawing.Point(237, 153);
            this.cmbOverlay.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOverlay.Name = "cmbOverlay";
            this.cmbOverlay.Size = new System.Drawing.Size(122, 21);
            this.cmbOverlay.TabIndex = 5;
            this.cmbOverlay.Text = "True";
            this.toolTip1.SetToolTip(this.cmbOverlay, "Enable overlay in-game, requires SSEOverlay plugin.");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(118, 73);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Language:";
            // 
            // cmbLang
            // 
            this.cmbLang.FormattingEnabled = true;
            this.cmbLang.Items.AddRange(new object[] {
            "",
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
            this.cmbLang.Location = new System.Drawing.Point(174, 71);
            this.cmbLang.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Size = new System.Drawing.Size(185, 21);
            this.cmbLang.TabIndex = 2;
            this.cmbLang.Text = "English";
            this.toolTip1.SetToolTip(this.cmbLang, "Choose your name that will be appeared in-game and on friends list. You can type " +
        "your name here.");
            // 
            // chkHideToTray
            // 
            this.chkHideToTray.AutoSize = true;
            this.chkHideToTray.Location = new System.Drawing.Point(386, 14);
            this.chkHideToTray.Margin = new System.Windows.Forms.Padding(2);
            this.chkHideToTray.Name = "chkHideToTray";
            this.chkHideToTray.Size = new System.Drawing.Size(80, 17);
            this.chkHideToTray.TabIndex = 9;
            this.chkHideToTray.Text = "Hide to tray";
            this.toolTip1.SetToolTip(this.chkHideToTray, "When minimize, hide launcher into tray.");
            this.chkHideToTray.UseVisualStyleBackColor = true;
            // 
            // pbAvatar
            // 
            this.pbAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAvatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAvatar.Location = new System.Drawing.Point(13, 15);
            this.pbAvatar.Margin = new System.Windows.Forms.Padding(2);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(96, 104);
            this.pbAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAvatar.TabIndex = 54;
            this.pbAvatar.TabStop = false;
            this.toolTip1.SetToolTip(this.pbAvatar, "Click to change your avatar.");
            this.pbAvatar.Click += new System.EventHandler(this.pbAvatar_Click);
            // 
            // lblChangeAvatar
            // 
            this.lblChangeAvatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChangeAvatar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblChangeAvatar.Location = new System.Drawing.Point(13, 121);
            this.lblChangeAvatar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeAvatar.Name = "lblChangeAvatar";
            this.lblChangeAvatar.Size = new System.Drawing.Size(96, 14);
            this.lblChangeAvatar.TabIndex = 3;
            this.lblChangeAvatar.Text = "Change Avatar";
            this.lblChangeAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblChangeAvatar, "Click to change your avatar.");
            this.lblChangeAvatar.Click += new System.EventHandler(this.lblChangeAvatar_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(118, 106);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(97, 13);
            this.label22.TabIndex = 48;
            this.label22.Text = "Quick Join Hotkey:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 131);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Warn if steam running:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(118, 45);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(118, 17);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Steam ID:";
            // 
            // cmbEmuQuickJoin
            // 
            this.cmbEmuQuickJoin.FormattingEnabled = true;
            this.cmbEmuQuickJoin.Items.AddRange(new object[] {
            "SHIFT + TAB"});
            this.cmbEmuQuickJoin.Location = new System.Drawing.Point(237, 104);
            this.cmbEmuQuickJoin.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmuQuickJoin.Name = "cmbEmuQuickJoin";
            this.cmbEmuQuickJoin.Size = new System.Drawing.Size(122, 21);
            this.cmbEmuQuickJoin.TabIndex = 3;
            this.cmbEmuQuickJoin.Text = "SHIFT + TAB";
            this.toolTip1.SetToolTip(this.cmbEmuQuickJoin, "When there isn\'t any lobby browser, or when lobby is created using private match," +
        " use this hotkey to join your partner game.");
            // 
            // cmbParanoidMode
            // 
            this.cmbParanoidMode.FormattingEnabled = true;
            this.cmbParanoidMode.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbParanoidMode.Location = new System.Drawing.Point(237, 128);
            this.cmbParanoidMode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbParanoidMode.Name = "cmbParanoidMode";
            this.cmbParanoidMode.Size = new System.Drawing.Size(122, 21);
            this.cmbParanoidMode.TabIndex = 4;
            this.cmbParanoidMode.Text = "False";
            this.toolTip1.SetToolTip(this.cmbParanoidMode, "Issue a warning before launching when steam is running. SmartSteamEmu can launch " +
        "fine with steam and don\'t communicate with steam. Enable if you are paranoid.");
            // 
            // cmbEmuPersonaName
            // 
            this.cmbEmuPersonaName.FormattingEnabled = true;
            this.cmbEmuPersonaName.Items.AddRange(new object[] {
            "AccountName",
            "ComputerName"});
            this.cmbEmuPersonaName.Location = new System.Drawing.Point(174, 42);
            this.cmbEmuPersonaName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmuPersonaName.Name = "cmbEmuPersonaName";
            this.cmbEmuPersonaName.Size = new System.Drawing.Size(185, 21);
            this.cmbEmuPersonaName.TabIndex = 1;
            this.cmbEmuPersonaName.Text = "AccountName";
            this.toolTip1.SetToolTip(this.cmbEmuPersonaName, "Choose your name that will be appeared in-game and on friends list. You can type " +
        "your name here.");
            // 
            // cmbEmuSteamId
            // 
            this.cmbEmuSteamId.FormattingEnabled = true;
            this.cmbEmuSteamId.Items.AddRange(new object[] {
            "Static",
            "Random",
            "PersonaName",
            "ip",
            "GenerateRandom"});
            this.cmbEmuSteamId.Location = new System.Drawing.Point(174, 15);
            this.cmbEmuSteamId.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmuSteamId.Name = "cmbEmuSteamId";
            this.cmbEmuSteamId.Size = new System.Drawing.Size(185, 21);
            this.cmbEmuSteamId.TabIndex = 0;
            this.cmbEmuSteamId.Text = "GenerateRandom";
            this.toolTip1.SetToolTip(this.cmbEmuSteamId, "Choose your steam id. Some games requires this to be consistent for save game to " +
        "work correctly. You can also specify the steam id manually here.");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkCleanLog);
            this.tabPage2.Controls.Add(this.txtOnlineKey);
            this.tabPage2.Controls.Add(this.label42);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.cmbOffline);
            this.tabPage2.Controls.Add(this.cmbEnableLog);
            this.tabPage2.Controls.Add(this.cmbEnableVR);
            this.tabPage2.Controls.Add(this.cmbEnableLobbyFilter);
            this.tabPage2.Controls.Add(this.cmbEnableIngameVoice);
            this.tabPage2.Controls.Add(this.cmbEnableHttp);
            this.tabPage2.Controls.Add(this.cmbDisableLeaderboard);
            this.tabPage2.Controls.Add(this.cmbDisableFriendList);
            this.tabPage2.Controls.Add(this.cmbSecuredServer);
            this.tabPage2.Controls.Add(this.cmbSeparateStorageByName);
            this.tabPage2.Controls.Add(this.cmbStorageOnAppData);
            this.tabPage2.Controls.Add(this.cmbAutoJoinInvite);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(552, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkCleanLog
            // 
            this.chkCleanLog.AutoSize = true;
            this.chkCleanLog.Location = new System.Drawing.Point(301, 160);
            this.chkCleanLog.Name = "chkCleanLog";
            this.chkCleanLog.Size = new System.Drawing.Size(196, 17);
            this.chkCleanLog.TabIndex = 80;
            this.chkCleanLog.Text = "Empty log before starting application";
            this.chkCleanLog.UseVisualStyleBackColor = true;
            // 
            // txtOnlineKey
            // 
            this.txtOnlineKey.Location = new System.Drawing.Point(156, 210);
            this.txtOnlineKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtOnlineKey.Name = "txtOnlineKey";
            this.txtOnlineKey.Size = new System.Drawing.Size(122, 20);
            this.txtOnlineKey.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtOnlineKey, "Set online key. By default, you are connected to players that play the same game." +
        " With OnlineKey set, you can find only players that uses the same key.");
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(13, 213);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(61, 13);
            this.label42.TabIndex = 79;
            this.label42.Text = "Online Key:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(298, 188);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 77;
            this.label12.Text = "Steam offline mode:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 136);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "Enable Log:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(298, 92);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 13);
            this.label19.TabIndex = 76;
            this.label19.Text = "Enable VR:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(298, 67);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 13);
            this.label18.TabIndex = 75;
            this.label18.Text = "Enable Lobby Filter";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(298, 43);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 13);
            this.label17.TabIndex = 74;
            this.label17.Text = "Enable In-Game Voice:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(298, 19);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 13);
            this.label16.TabIndex = 73;
            this.label16.Text = "Enable HTTP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Disable Leaderboard:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 164);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 13);
            this.label21.TabIndex = 71;
            this.label21.Text = "Disable Friend List:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 140);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 13);
            this.label20.TabIndex = 70;
            this.label20.Text = "Secured Server:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 67);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 13);
            this.label14.TabIndex = 69;
            this.label14.Text = "Separate Storage by Name:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 43);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 68;
            this.label13.Text = "Storage On Appdata:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 19);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 13);
            this.label11.TabIndex = 67;
            this.label11.Text = "Automatically Join Invite:";
            // 
            // cmbOffline
            // 
            this.cmbOffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOffline.FormattingEnabled = true;
            this.cmbOffline.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbOffline.Location = new System.Drawing.Point(418, 186);
            this.cmbOffline.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOffline.Name = "cmbOffline";
            this.cmbOffline.Size = new System.Drawing.Size(122, 21);
            this.cmbOffline.TabIndex = 12;
            this.toolTip1.SetToolTip(this.cmbOffline, "Emulates steam offline mode.");
            // 
            // cmbEnableLog
            // 
            this.cmbEnableLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableLog.FormattingEnabled = true;
            this.cmbEnableLog.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbEnableLog.Location = new System.Drawing.Point(418, 134);
            this.cmbEnableLog.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableLog.Name = "cmbEnableLog";
            this.cmbEnableLog.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableLog.TabIndex = 11;
            this.toolTip1.SetToolTip(this.cmbEnableLog, "Enable debug logging.");
            // 
            // cmbEnableVR
            // 
            this.cmbEnableVR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableVR.FormattingEnabled = true;
            this.cmbEnableVR.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbEnableVR.Location = new System.Drawing.Point(418, 89);
            this.cmbEnableVR.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableVR.Name = "cmbEnableVR";
            this.cmbEnableVR.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableVR.TabIndex = 10;
            this.toolTip1.SetToolTip(this.cmbEnableVR, "Tell the game that steam have virtual reality enabled.");
            // 
            // cmbEnableLobbyFilter
            // 
            this.cmbEnableLobbyFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableLobbyFilter.FormattingEnabled = true;
            this.cmbEnableLobbyFilter.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbEnableLobbyFilter.Location = new System.Drawing.Point(418, 65);
            this.cmbEnableLobbyFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableLobbyFilter.Name = "cmbEnableLobbyFilter";
            this.cmbEnableLobbyFilter.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableLobbyFilter.TabIndex = 9;
            this.toolTip1.SetToolTip(this.cmbEnableLobbyFilter, "Some game will filter the lobby by match type, skills, etc. Turning off will retu" +
        "rn all lobbies found to the game. This may result in unknown behaviour.");
            // 
            // cmbEnableIngameVoice
            // 
            this.cmbEnableIngameVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableIngameVoice.FormattingEnabled = true;
            this.cmbEnableIngameVoice.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbEnableIngameVoice.Location = new System.Drawing.Point(418, 41);
            this.cmbEnableIngameVoice.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableIngameVoice.Name = "cmbEnableIngameVoice";
            this.cmbEnableIngameVoice.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableIngameVoice.TabIndex = 8;
            this.toolTip1.SetToolTip(this.cmbEnableIngameVoice, "Enable in-game voice chat.");
            // 
            // cmbEnableHttp
            // 
            this.cmbEnableHttp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableHttp.FormattingEnabled = true;
            this.cmbEnableHttp.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbEnableHttp.Location = new System.Drawing.Point(418, 16);
            this.cmbEnableHttp.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEnableHttp.Name = "cmbEnableHttp";
            this.cmbEnableHttp.Size = new System.Drawing.Size(122, 21);
            this.cmbEnableHttp.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cmbEnableHttp, "If disabled, all calls to create HTTP request will fail. It is advised to keep th" +
        "is turned off.");
            // 
            // cmbDisableLeaderboard
            // 
            this.cmbDisableLeaderboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisableLeaderboard.FormattingEnabled = true;
            this.cmbDisableLeaderboard.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbDisableLeaderboard.Location = new System.Drawing.Point(156, 186);
            this.cmbDisableLeaderboard.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisableLeaderboard.Name = "cmbDisableLeaderboard";
            this.cmbDisableLeaderboard.Size = new System.Drawing.Size(122, 21);
            this.cmbDisableLeaderboard.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cmbDisableLeaderboard, "Don\'t return any leaderboard when game requested it. Disable this if the game han" +
        "gs when performing leaderboard operations.");
            // 
            // cmbDisableFriendList
            // 
            this.cmbDisableFriendList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisableFriendList.FormattingEnabled = true;
            this.cmbDisableFriendList.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbDisableFriendList.Location = new System.Drawing.Point(156, 162);
            this.cmbDisableFriendList.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDisableFriendList.Name = "cmbDisableFriendList";
            this.cmbDisableFriendList.Size = new System.Drawing.Size(122, 21);
            this.cmbDisableFriendList.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cmbDisableFriendList, "Disable in-game friend list.");
            // 
            // cmbSecuredServer
            // 
            this.cmbSecuredServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecuredServer.FormattingEnabled = true;
            this.cmbSecuredServer.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbSecuredServer.Location = new System.Drawing.Point(156, 137);
            this.cmbSecuredServer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSecuredServer.Name = "cmbSecuredServer";
            this.cmbSecuredServer.Size = new System.Drawing.Size(122, 21);
            this.cmbSecuredServer.TabIndex = 3;
            this.toolTip1.SetToolTip(this.cmbSecuredServer, "Create a VAC secured server. Disable this if the game complain it starts in insec" +
        "ure mode or demand you to remove any plugins.");
            // 
            // cmbSeparateStorageByName
            // 
            this.cmbSeparateStorageByName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeparateStorageByName.FormattingEnabled = true;
            this.cmbSeparateStorageByName.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbSeparateStorageByName.Location = new System.Drawing.Point(156, 65);
            this.cmbSeparateStorageByName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSeparateStorageByName.Name = "cmbSeparateStorageByName";
            this.cmbSeparateStorageByName.Size = new System.Drawing.Size(122, 21);
            this.cmbSeparateStorageByName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.cmbSeparateStorageByName, "Each \"PersonaName\" will have its own storage folder for save game data.");
            // 
            // cmbStorageOnAppData
            // 
            this.cmbStorageOnAppData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageOnAppData.FormattingEnabled = true;
            this.cmbStorageOnAppData.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbStorageOnAppData.Location = new System.Drawing.Point(156, 41);
            this.cmbStorageOnAppData.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStorageOnAppData.Name = "cmbStorageOnAppData";
            this.cmbStorageOnAppData.Size = new System.Drawing.Size(122, 21);
            this.cmbStorageOnAppData.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cmbStorageOnAppData, "Save game data on AppData\\Roaming\\SmartSteamEmu folder.");
            // 
            // cmbAutoJoinInvite
            // 
            this.cmbAutoJoinInvite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutoJoinInvite.FormattingEnabled = true;
            this.cmbAutoJoinInvite.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbAutoJoinInvite.Location = new System.Drawing.Point(156, 16);
            this.cmbAutoJoinInvite.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAutoJoinInvite.Name = "cmbAutoJoinInvite";
            this.cmbAutoJoinInvite.Size = new System.Drawing.Size(122, 21);
            this.cmbAutoJoinInvite.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cmbAutoJoinInvite, "When somebody invite you in-game, you will be joined to the game automatically wi" +
        "thout prompting. Disabling this will do nothing when someone invite you.");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.numNetMaxConn);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Controls.Add(this.btnNetDelIp);
            this.tabPage3.Controls.Add(this.btnNetAddIp);
            this.tabPage3.Controls.Add(this.txtNetIp);
            this.tabPage3.Controls.Add(this.label27);
            this.tabPage3.Controls.Add(this.lstNetBroadcast);
            this.tabPage3.Controls.Add(this.numNetDiscoveryInterval);
            this.tabPage3.Controls.Add(this.numNetMaxPort);
            this.tabPage3.Controls.Add(this.numNetListenPort);
            this.tabPage3.Controls.Add(this.label26);
            this.tabPage3.Controls.Add(this.label25);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(552, 266);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Networking";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // numNetMaxConn
            // 
            this.numNetMaxConn.Location = new System.Drawing.Point(108, 95);
            this.numNetMaxConn.Margin = new System.Windows.Forms.Padding(2);
            this.numNetMaxConn.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numNetMaxConn.Name = "numNetMaxConn";
            this.numNetMaxConn.Size = new System.Drawing.Size(128, 20);
            this.numNetMaxConn.TabIndex = 3;
            this.numNetMaxConn.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(12, 97);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(87, 13);
            this.label34.TabIndex = 18;
            this.label34.Text = "Max Connection:";
            // 
            // btnNetDelIp
            // 
            this.btnNetDelIp.Location = new System.Drawing.Point(502, 44);
            this.btnNetDelIp.Margin = new System.Windows.Forms.Padding(2);
            this.btnNetDelIp.Name = "btnNetDelIp";
            this.btnNetDelIp.Size = new System.Drawing.Size(39, 25);
            this.btnNetDelIp.TabIndex = 7;
            this.btnNetDelIp.Text = "Del";
            this.btnNetDelIp.UseVisualStyleBackColor = true;
            this.btnNetDelIp.Click += new System.EventHandler(this.btnNetDelIp_Click);
            // 
            // btnNetAddIp
            // 
            this.btnNetAddIp.Location = new System.Drawing.Point(502, 14);
            this.btnNetAddIp.Margin = new System.Windows.Forms.Padding(2);
            this.btnNetAddIp.Name = "btnNetAddIp";
            this.btnNetAddIp.Size = new System.Drawing.Size(39, 25);
            this.btnNetAddIp.TabIndex = 5;
            this.btnNetAddIp.Text = "Add";
            this.btnNetAddIp.UseVisualStyleBackColor = true;
            this.btnNetAddIp.Click += new System.EventHandler(this.btnNetAddIp_Click);
            // 
            // txtNetIp
            // 
            this.txtNetIp.Location = new System.Drawing.Point(372, 17);
            this.txtNetIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtNetIp.Name = "txtNetIp";
            this.txtNetIp.Size = new System.Drawing.Size(127, 20);
            this.txtNetIp.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtNetIp, "Broadcast address or your partner IP address to connect to.");
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(260, 17);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(110, 13);
            this.label27.TabIndex = 17;
            this.label27.Text = "Broadcast/Partner IP:";
            // 
            // lstNetBroadcast
            // 
            this.lstNetBroadcast.ContextMenuStrip = this.contextMenuStrip1;
            this.lstNetBroadcast.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstNetBroadcast.FormattingEnabled = true;
            this.lstNetBroadcast.ItemHeight = 16;
            this.lstNetBroadcast.Location = new System.Drawing.Point(262, 43);
            this.lstNetBroadcast.Margin = new System.Windows.Forms.Padding(2);
            this.lstNetBroadcast.Name = "lstNetBroadcast";
            this.lstNetBroadcast.Size = new System.Drawing.Size(237, 212);
            this.lstNetBroadcast.TabIndex = 6;
            this.lstNetBroadcast.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstNetBroadcast_DrawItem);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableDisableToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // enableDisableToolStripMenuItem
            // 
            this.enableDisableToolStripMenuItem.Name = "enableDisableToolStripMenuItem";
            this.enableDisableToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.enableDisableToolStripMenuItem.Text = "Enable/Disable";
            this.enableDisableToolStripMenuItem.Click += new System.EventHandler(this.enableDisableToolStripMenuItem_Click);
            // 
            // numNetDiscoveryInterval
            // 
            this.numNetDiscoveryInterval.Location = new System.Drawing.Point(108, 68);
            this.numNetDiscoveryInterval.Margin = new System.Windows.Forms.Padding(2);
            this.numNetDiscoveryInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numNetDiscoveryInterval.Name = "numNetDiscoveryInterval";
            this.numNetDiscoveryInterval.Size = new System.Drawing.Size(128, 20);
            this.numNetDiscoveryInterval.TabIndex = 2;
            this.toolTip1.SetToolTip(this.numNetDiscoveryInterval, "How many seconds before sending another discovery packet to the broadcast address" +
        ".");
            this.numNetDiscoveryInterval.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numNetMaxPort
            // 
            this.numNetMaxPort.Location = new System.Drawing.Point(108, 43);
            this.numNetMaxPort.Margin = new System.Windows.Forms.Padding(2);
            this.numNetMaxPort.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numNetMaxPort.Name = "numNetMaxPort";
            this.numNetMaxPort.Size = new System.Drawing.Size(128, 20);
            this.numNetMaxPort.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numNetMaxPort, "How many attempt to use next port if the previous one is already taken.");
            this.numNetMaxPort.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numNetListenPort
            // 
            this.numNetListenPort.Location = new System.Drawing.Point(108, 17);
            this.numNetListenPort.Margin = new System.Windows.Forms.Padding(2);
            this.numNetListenPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numNetListenPort.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numNetListenPort.Name = "numNetListenPort";
            this.numNetListenPort.Size = new System.Drawing.Size(128, 20);
            this.numNetListenPort.TabIndex = 0;
            this.toolTip1.SetToolTip(this.numNetListenPort, "Communication port used by this emulator, must match with other player or it won\'" +
        "t find each other.");
            this.numNetListenPort.Value = new decimal(new int[] {
            31313,
            0,
            0,
            0});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 70);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 13);
            this.label26.TabIndex = 10;
            this.label26.Text = "Discovery Interval:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 45);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 13);
            this.label25.TabIndex = 11;
            this.label25.Text = "Maximum Port:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 19);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 12;
            this.label24.Text = "Port:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnDelMasterServer);
            this.tabPage4.Controls.Add(this.btnAddMasterServer);
            this.tabPage4.Controls.Add(this.txtMasterServerIp);
            this.tabPage4.Controls.Add(this.label28);
            this.tabPage4.Controls.Add(this.lstMasterServer);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(552, 266);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Master Server";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnDelMasterServer
            // 
            this.btnDelMasterServer.Location = new System.Drawing.Point(490, 43);
            this.btnDelMasterServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelMasterServer.Name = "btnDelMasterServer";
            this.btnDelMasterServer.Size = new System.Drawing.Size(52, 24);
            this.btnDelMasterServer.TabIndex = 12;
            this.btnDelMasterServer.Text = "Del";
            this.btnDelMasterServer.UseVisualStyleBackColor = true;
            this.btnDelMasterServer.Click += new System.EventHandler(this.btnDelMasterServer_Click);
            // 
            // btnAddMasterServer
            // 
            this.btnAddMasterServer.Location = new System.Drawing.Point(490, 15);
            this.btnAddMasterServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddMasterServer.Name = "btnAddMasterServer";
            this.btnAddMasterServer.Size = new System.Drawing.Size(52, 24);
            this.btnAddMasterServer.TabIndex = 10;
            this.btnAddMasterServer.Text = "Add";
            this.btnAddMasterServer.UseVisualStyleBackColor = true;
            this.btnAddMasterServer.Click += new System.EventHandler(this.btnAddMasterServer_Click);
            // 
            // txtMasterServerIp
            // 
            this.txtMasterServerIp.Location = new System.Drawing.Point(56, 17);
            this.txtMasterServerIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtMasterServerIp.Name = "txtMasterServerIp";
            this.txtMasterServerIp.Size = new System.Drawing.Size(431, 20);
            this.txtMasterServerIp.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtMasterServerIp, "Master server address, specify format in IP:PORT");
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 20);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(39, 13);
            this.label28.TabIndex = 13;
            this.label28.Text = "IP:Port";
            // 
            // lstMasterServer
            // 
            this.lstMasterServer.ContextMenuStrip = this.contextMenuStrip1;
            this.lstMasterServer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstMasterServer.FormattingEnabled = true;
            this.lstMasterServer.ItemHeight = 16;
            this.lstMasterServer.Location = new System.Drawing.Point(14, 43);
            this.lstMasterServer.Margin = new System.Windows.Forms.Padding(2);
            this.lstMasterServer.Name = "lstMasterServer";
            this.lstMasterServer.Size = new System.Drawing.Size(472, 212);
            this.lstMasterServer.TabIndex = 11;
            this.lstMasterServer.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstMasterServer_DrawItem);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Controls.Add(this.chkAllowAnyToConnect);
            this.tabPage5.Controls.Add(this.txtAdminPass);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(552, 266);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Player Management";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBanDel);
            this.groupBox1.Controls.Add(this.btnBanAdd);
            this.groupBox1.Controls.Add(this.txtBan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstBan);
            this.groupBox1.Location = new System.Drawing.Point(18, 75);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(509, 181);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ban List";
            // 
            // btnBanDel
            // 
            this.btnBanDel.Location = new System.Drawing.Point(453, 43);
            this.btnBanDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnBanDel.Name = "btnBanDel";
            this.btnBanDel.Size = new System.Drawing.Size(52, 24);
            this.btnBanDel.TabIndex = 17;
            this.btnBanDel.Text = "Del";
            this.btnBanDel.UseVisualStyleBackColor = true;
            this.btnBanDel.Click += new System.EventHandler(this.btnBanDel_Click);
            // 
            // btnBanAdd
            // 
            this.btnBanAdd.Location = new System.Drawing.Point(453, 15);
            this.btnBanAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnBanAdd.Name = "btnBanAdd";
            this.btnBanAdd.Size = new System.Drawing.Size(52, 24);
            this.btnBanAdd.TabIndex = 15;
            this.btnBanAdd.Text = "Add";
            this.btnBanAdd.UseVisualStyleBackColor = true;
            this.btnBanAdd.Click += new System.EventHandler(this.btnBanAdd_Click);
            // 
            // txtBan
            // 
            this.txtBan.Location = new System.Drawing.Point(80, 17);
            this.txtBan.Margin = new System.Windows.Forms.Padding(2);
            this.txtBan.Name = "txtBan";
            this.txtBan.Size = new System.Drawing.Size(370, 20);
            this.txtBan.TabIndex = 14;
            this.toolTip1.SetToolTip(this.txtBan, "The format is STEAM:X:YYYY");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Steam ID:";
            // 
            // lstBan
            // 
            this.lstBan.ContextMenuStrip = this.contextMenuStrip1;
            this.lstBan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBan.FormattingEnabled = true;
            this.lstBan.ItemHeight = 16;
            this.lstBan.Location = new System.Drawing.Point(26, 40);
            this.lstBan.Margin = new System.Windows.Forms.Padding(2);
            this.lstBan.Name = "lstBan";
            this.lstBan.Size = new System.Drawing.Size(424, 132);
            this.lstBan.TabIndex = 16;
            this.lstBan.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBan_DrawItem);
            // 
            // chkAllowAnyToConnect
            // 
            this.chkAllowAnyToConnect.AutoSize = true;
            this.chkAllowAnyToConnect.Location = new System.Drawing.Point(18, 14);
            this.chkAllowAnyToConnect.Margin = new System.Windows.Forms.Padding(2);
            this.chkAllowAnyToConnect.Name = "chkAllowAnyToConnect";
            this.chkAllowAnyToConnect.Size = new System.Drawing.Size(143, 17);
            this.chkAllowAnyToConnect.TabIndex = 0;
            this.chkAllowAnyToConnect.Text = "Allow anyone to connect";
            this.toolTip1.SetToolTip(this.chkAllowAnyToConnect, "Allow anyone to connect. If unchecked, only allow connection from steam and recog" +
        "nized emulator.");
            this.chkAllowAnyToConnect.UseVisualStyleBackColor = true;
            // 
            // txtAdminPass
            // 
            this.txtAdminPass.Location = new System.Drawing.Point(109, 36);
            this.txtAdminPass.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdminPass.Name = "txtAdminPass";
            this.txtAdminPass.Size = new System.Drawing.Size(415, 20);
            this.txtAdminPass.TabIndex = 14;
            this.toolTip1.SetToolTip(this.txtAdminPass, "Password for remote management. Must > 4 chars. WARNING: Password stored in plain" +
        "text.");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Admin Password:";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 340);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.Text = "Global Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxConn)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numNetDiscoveryInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetMaxPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetListenPort)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
	}
}
