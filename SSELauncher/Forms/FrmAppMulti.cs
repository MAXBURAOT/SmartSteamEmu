using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SSELauncher
{
	public class FrmAppMulti : Form
	{
		public List<CApp> Apps;

		public List<CApp> SelectedApps;

		public CApp SelectedApp;

		private Label label1;

		private Button btnAdd;

		private Button btnManual;

		private Button btnCancel;

		private CheckedListBox lstMulti;

		public FrmAppMulti()
		{
            InitializeComponent();
		}

		private void FrmAppMulti_Load(object sender, EventArgs e)
		{
			foreach (CApp current in Apps)
			{
				string text = current.GameName;
				if (current.CommandLine.Length > 0)
				{
					text = text + " (" + current.CommandLine + ")";
				}
				int index = lstMulti.Items.Add(text);
                lstMulti.SetItemChecked(index, current.HasGameDir);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		private void btnManual_Click(object sender, EventArgs e)
		{
			try
			{
                SelectedApp = Apps[lstMulti.SelectedIndex];
			}
			catch
			{
			}
			base.DialogResult = DialogResult.No;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
            SelectedApps = new List<CApp>();
			foreach (int index in lstMulti.CheckedIndices)
			{
                SelectedApps.Add(Apps[index]);
			}
			base.DialogResult = DialogResult.Yes;
		}

		private void InitializeComponent()
		{
            label1 = new Label();
            btnAdd = new Button();
            btnManual = new Button();
            btnCancel = new Button();
            lstMulti = new CheckedListBox();
			base.SuspendLayout();
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(704, 36);
            label1.TabIndex = 1;
            label1.Text = "Multiple configurations detected for this game. Select game(s) you want to add or click \"Go Manual\" to configure manually.";
            btnAdd.Location = new Point(573, 272);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(141, 34);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += new EventHandler(btnAdd_Click);
            btnManual.Location = new Point(425, 273);
            btnManual.Margin = new Padding(3, 2, 3, 2);
            btnManual.Name = "btnManual";
            btnManual.Size = new Size(141, 33);
            btnManual.TabIndex = 3;
            btnManual.Text = "Go Manual";
            btnManual.UseVisualStyleBackColor = true;
            btnManual.Click += new EventHandler(btnManual_Click);
            btnCancel.Location = new Point(15, 273);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(123, 34);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new EventHandler(btnCancel_Click);
            lstMulti.FormattingEnabled = true;
            lstMulti.Location = new Point(15, 48);
            lstMulti.Margin = new Padding(3, 2, 3, 2);
            lstMulti.Name = "lstMulti";
            lstMulti.Size = new Size(700, 191);
            lstMulti.TabIndex = 5;
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(728, 319);
			base.Controls.Add(lstMulti);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnManual);
			base.Controls.Add(btnAdd);
			base.Controls.Add(label1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Margin = new Padding(3, 2, 3, 2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FrmAppMulti";
			base.ShowInTaskbar = false;
            Text = "Multiple Game Configuration";
			base.Load += new EventHandler(FrmAppMulti_Load);
			base.ResumeLayout(false);
		}
	}
}
