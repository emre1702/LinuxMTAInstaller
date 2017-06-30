using System.Windows.Forms;

namespace LinuxMTAInstaller.Forms {
	partial class DatabaseWindow {
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.url = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.hidedb = new System.Windows.Forms.CheckedListBox();
			this.databasesystem = new System.Windows.Forms.ComboBox();
			this.webapp = new System.Windows.Forms.ComboBox();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.newuserfordb = new System.Windows.Forms.CheckBox();
			this.labelmysqlusername = new System.Windows.Forms.Label();
			this.mysqlusername = new System.Windows.Forms.TextBox();
			this.labelmysqluserpw = new System.Windows.Forms.Label();
			this.mysqluserpw = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.databasename = new System.Windows.Forms.TextBox();
			this.grantalluser = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.useronlylocal = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(25, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(142, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "MySQL-Datenbank-System:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 242);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "MySQL-Passwort:";
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(101, 239);
			this.password.Name = "password";
			this.password.Size = new System.Drawing.Size(237, 20);
			this.password.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(59, 448);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 28);
			this.button1.TabIndex = 12;
			this.button1.Text = "Installieren";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(56, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 17);
			this.label3.TabIndex = 9;
			this.label3.Text = "Websiten-Applikation:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(249, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "PhpMyAdmin-Link - z.B. 151.31.41.54/phpmyadmin";
			// 
			// url
			// 
			this.url.Location = new System.Drawing.Point(12, 105);
			this.url.Name = "url";
			this.url.Size = new System.Drawing.Size(326, 20);
			this.url.TabIndex = 2;
			this.url.Text = "phpmyadmin";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(-6, 151);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 64);
			this.label5.TabIndex = 12;
			this.label5.Text = "Verstecke Datenbanken:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// hidedb
			// 
			this.hidedb.CheckOnClick = true;
			this.hidedb.FormattingEnabled = true;
			this.hidedb.Items.AddRange(new object[] {
            "performance_schema",
            "mysql",
            "phpmyadmin",
            "information_schema",
            "sys"});
			this.hidedb.Location = new System.Drawing.Point(101, 151);
			this.hidedb.Name = "hidedb";
			this.hidedb.Size = new System.Drawing.Size(237, 79);
			this.hidedb.TabIndex = 3;
			// 
			// databasesystem
			// 
			this.databasesystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.databasesystem.FormattingEnabled = true;
			this.databasesystem.Items.AddRange(new object[] {
            "MariaDB",
            "MySQL"});
			this.databasesystem.Location = new System.Drawing.Point(187, 18);
			this.databasesystem.Name = "databasesystem";
			this.databasesystem.Size = new System.Drawing.Size(151, 21);
			this.databasesystem.TabIndex = 0;
			// 
			// webapp
			// 
			this.webapp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.webapp.FormattingEnabled = true;
			this.webapp.Items.AddRange(new object[] {
            "Apache",
            "Nginx"});
			this.webapp.Location = new System.Drawing.Point(187, 45);
			this.webapp.Name = "webapp";
			this.webapp.Size = new System.Drawing.Size(151, 21);
			this.webapp.TabIndex = 1;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Error;
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			// 
			// newuserfordb
			// 
			this.newuserfordb.AutoSize = true;
			this.newuserfordb.Checked = true;
			this.newuserfordb.CheckState = System.Windows.Forms.CheckState.Checked;
			this.newuserfordb.Location = new System.Drawing.Point(18, 328);
			this.newuserfordb.Name = "newuserfordb";
			this.newuserfordb.Size = new System.Drawing.Size(216, 17);
			this.newuserfordb.TabIndex = 7;
			this.newuserfordb.Text = "Neuen Benutzer für Datenbank erstellen";
			this.newuserfordb.UseVisualStyleBackColor = true;
			this.newuserfordb.CheckedChanged += new System.EventHandler(this.newuserfordb_CheckedChanged);
			// 
			// labelmysqlusername
			// 
			this.labelmysqlusername.AutoSize = true;
			this.labelmysqlusername.Location = new System.Drawing.Point(15, 354);
			this.labelmysqlusername.Name = "labelmysqlusername";
			this.labelmysqlusername.Size = new System.Drawing.Size(83, 13);
			this.labelmysqlusername.TabIndex = 17;
			this.labelmysqlusername.Text = "Benutzer-Name:";
			// 
			// mysqlusername
			// 
			this.mysqlusername.Location = new System.Drawing.Point(101, 351);
			this.mysqlusername.Name = "mysqlusername";
			this.mysqlusername.Size = new System.Drawing.Size(237, 20);
			this.mysqlusername.TabIndex = 8;
			// 
			// labelmysqluserpw
			// 
			this.labelmysqluserpw.AutoSize = true;
			this.labelmysqluserpw.Location = new System.Drawing.Point(45, 383);
			this.labelmysqluserpw.Name = "labelmysqluserpw";
			this.labelmysqluserpw.Size = new System.Drawing.Size(50, 13);
			this.labelmysqluserpw.TabIndex = 19;
			this.labelmysqluserpw.Text = "Passwort";
			// 
			// mysqluserpw
			// 
			this.mysqluserpw.Location = new System.Drawing.Point(101, 380);
			this.mysqluserpw.Name = "mysqluserpw";
			this.mysqluserpw.Size = new System.Drawing.Size(237, 20);
			this.mysqluserpw.TabIndex = 9;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 271);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Datenbank-Name:";
			// 
			// databasename
			// 
			this.databasename.Location = new System.Drawing.Point(101, 268);
			this.databasename.Name = "databasename";
			this.databasename.Size = new System.Drawing.Size(237, 20);
			this.databasename.TabIndex = 5;
			// 
			// grantalluser
			// 
			this.grantalluser.AutoSize = true;
			this.grantalluser.Checked = true;
			this.grantalluser.CheckState = System.Windows.Forms.CheckState.Checked;
			this.grantalluser.Location = new System.Drawing.Point(59, 406);
			this.grantalluser.Name = "grantalluser";
			this.grantalluser.Size = new System.Drawing.Size(87, 17);
			this.grantalluser.TabIndex = 10;
			this.grantalluser.Text = "Volle Rechte";
			this.grantalluser.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(217, 448);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(87, 28);
			this.button2.TabIndex = 13;
			this.button2.Text = "Zurück";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// useronlylocal
			// 
			this.useronlylocal.AutoSize = true;
			this.useronlylocal.Checked = true;
			this.useronlylocal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.useronlylocal.Location = new System.Drawing.Point(207, 406);
			this.useronlylocal.Name = "useronlylocal";
			this.useronlylocal.Size = new System.Drawing.Size(108, 17);
			this.useronlylocal.TabIndex = 11;
			this.useronlylocal.Text = "Rechte nur Lokal";
			this.useronlylocal.UseVisualStyleBackColor = true;
			// 
			// DatabaseWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 488);
			this.Controls.Add(this.useronlylocal);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.grantalluser);
			this.Controls.Add(this.databasename);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.mysqluserpw);
			this.Controls.Add(this.labelmysqluserpw);
			this.Controls.Add(this.mysqlusername);
			this.Controls.Add(this.labelmysqlusername);
			this.Controls.Add(this.newuserfordb);
			this.Controls.Add(this.webapp);
			this.Controls.Add(this.databasesystem);
			this.Controls.Add(this.hidedb);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.url);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.password);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "DatabaseWindow";
			this.Text = "DatabaseWindow";
			this.Load += new System.EventHandler(this.DatabaseWindow_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox url;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckedListBox hidedb;
		private System.Windows.Forms.ComboBox databasesystem;
		private ComboBox webapp;
		private NotifyIcon notifyIcon1;
		private CheckBox newuserfordb;
		private Label labelmysqlusername;
		private TextBox mysqlusername;
		private Label labelmysqluserpw;
		private TextBox mysqluserpw;
		private Label label6;
		private TextBox databasename;
		private CheckBox grantalluser;
		private Button button2;
		private CheckBox useronlylocal;
	}
}