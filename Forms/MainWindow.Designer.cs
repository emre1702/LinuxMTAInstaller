namespace LinuxMTAInstaller.Forms {
	partial class MainWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ( ) {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.installMTA = new System.Windows.Forms.Button();
			this.installFirewall = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.installDatabase = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// installMTA
			// 
			this.installMTA.Location = new System.Drawing.Point(12, 121);
			this.installMTA.Name = "installMTA";
			this.installMTA.Size = new System.Drawing.Size(162, 30);
			this.installMTA.TabIndex = 0;
			this.installMTA.Text = "MTA";
			this.installMTA.UseVisualStyleBackColor = true;
			this.installMTA.Click += new System.EventHandler(this.installMTA_Click);
			// 
			// installFirewall
			// 
			this.installFirewall.Location = new System.Drawing.Point(187, 157);
			this.installFirewall.Name = "installFirewall";
			this.installFirewall.Size = new System.Drawing.Size(162, 30);
			this.installFirewall.TabIndex = 3;
			this.installFirewall.Text = "Firewall";
			this.installFirewall.UseVisualStyleBackColor = true;
			this.installFirewall.Click += new System.EventHandler(this.installFirewall_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(2, -1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(356, 119);
			this.label1.TabIndex = 5;
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			// 
			// installDatabase
			// 
			this.installDatabase.Location = new System.Drawing.Point(187, 121);
			this.installDatabase.Name = "installDatabase";
			this.installDatabase.Size = new System.Drawing.Size(162, 30);
			this.installDatabase.TabIndex = 6;
			this.installDatabase.Text = "Database";
			this.installDatabase.UseVisualStyleBackColor = true;
			this.installDatabase.Click += new System.EventHandler(this.installDatabase_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 157);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(162, 30);
			this.button1.TabIndex = 7;
			this.button1.Text = "Teamspeak";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(361, 203);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.installDatabase);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.installFirewall);
			this.Controls.Add(this.installMTA);
			this.Name = "MainWindow";
			this.Text = "Installer";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button installMTA;
		private System.Windows.Forms.Button installFirewall;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.Button installDatabase;
		private System.Windows.Forms.Button button1;
	}
}