namespace LinuxMTAInstaller.Forms {
	partial class MTAWindow {
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
			this.usernameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.serverPort = new System.Windows.Forms.NumericUpDown();
			this.httpPort = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.asePort = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.servername = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.maxplayers = new System.Windows.Forms.NumericUpDown();
			this.serverpassword = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.fpslimit = new System.Windows.Forms.NumericUpDown();
			this.skinmodding = new System.Windows.Forms.CheckBox();
			this.bulletsync = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.userPasswordBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.autostart = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.email = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.error = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.serverPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.httpPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.maxplayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpslimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Running user:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// usernameBox
			// 
			this.usernameBox.Location = new System.Drawing.Point(130, 21);
			this.usernameBox.Name = "usernameBox";
			this.usernameBox.Size = new System.Drawing.Size(226, 20);
			this.usernameBox.TabIndex = 1;
			this.usernameBox.Text = "mta";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(15, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Server-port (UDP):";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// serverPort
			// 
			this.serverPort.Location = new System.Drawing.Point(130, 76);
			this.serverPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.serverPort.Name = "serverPort";
			this.serverPort.Size = new System.Drawing.Size(226, 20);
			this.serverPort.TabIndex = 3;
			this.serverPort.Value = new decimal(new int[] {
            22003,
            0,
            0,
            0});
			this.serverPort.TextChanged += new System.EventHandler(this.calculateAsePort);
			// 
			// httpPort
			// 
			this.httpPort.Location = new System.Drawing.Point(130, 102);
			this.httpPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.httpPort.Name = "httpPort";
			this.httpPort.Size = new System.Drawing.Size(226, 20);
			this.httpPort.TabIndex = 4;
			this.httpPort.Value = new decimal(new int[] {
            22005,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(102, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "HTTP-port (TCP):";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 131);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(108, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "ASE-port (UDP):";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// asePort
			// 
			this.asePort.Enabled = false;
			this.asePort.Location = new System.Drawing.Point(130, 128);
			this.asePort.Name = "asePort";
			this.asePort.ReadOnly = true;
			this.asePort.Size = new System.Drawing.Size(226, 20);
			this.asePort.TabIndex = 7;
			this.asePort.Text = "22126";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 178);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Server-name:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// servername
			// 
			this.servername.Location = new System.Drawing.Point(130, 175);
			this.servername.Name = "servername";
			this.servername.Size = new System.Drawing.Size(226, 20);
			this.servername.TabIndex = 9;
			this.servername.Text = "MTA Server | Created By [TDS]Bonus Tool";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 203);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(116, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Max. players:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maxplayers
			// 
			this.maxplayers.Location = new System.Drawing.Point(130, 201);
			this.maxplayers.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.maxplayers.Name = "maxplayers";
			this.maxplayers.Size = new System.Drawing.Size(226, 20);
			this.maxplayers.TabIndex = 11;
			this.maxplayers.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// serverpassword
			// 
			this.serverpassword.Location = new System.Drawing.Point(130, 227);
			this.serverpassword.Name = "serverpassword";
			this.serverpassword.Size = new System.Drawing.Size(226, 20);
			this.serverpassword.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 230);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "Server-password:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(15, 255);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(102, 13);
			this.label8.TabIndex = 14;
			this.label8.Text = "FPS-limit:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fpslimit
			// 
			this.fpslimit.Location = new System.Drawing.Point(130, 253);
			this.fpslimit.Maximum = new decimal(new int[] {
            65,
            0,
            0,
            0});
			this.fpslimit.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.fpslimit.Name = "fpslimit";
			this.fpslimit.Size = new System.Drawing.Size(226, 20);
			this.fpslimit.TabIndex = 15;
			this.fpslimit.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// skinmodding
			// 
			this.skinmodding.AutoSize = true;
			this.skinmodding.Location = new System.Drawing.Point(262, 343);
			this.skinmodding.Name = "skinmodding";
			this.skinmodding.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.skinmodding.Size = new System.Drawing.Size(93, 17);
			this.skinmodding.TabIndex = 16;
			this.skinmodding.Text = ":Skin-modding";
			this.skinmodding.UseVisualStyleBackColor = true;
			// 
			// bulletsync
			// 
			this.bulletsync.AutoSize = true;
			this.bulletsync.Checked = true;
			this.bulletsync.CheckState = System.Windows.Forms.CheckState.Checked;
			this.bulletsync.Location = new System.Drawing.Point(143, 343);
			this.bulletsync.Name = "bulletsync";
			this.bulletsync.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.bulletsync.Size = new System.Drawing.Size(80, 17);
			this.bulletsync.TabIndex = 17;
			this.bulletsync.Text = ":Bullet-sync";
			this.bulletsync.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(49, 436);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(109, 23);
			this.button1.TabIndex = 18;
			this.button1.Text = "Install";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			// 
			// userPasswordBox
			// 
			this.userPasswordBox.Location = new System.Drawing.Point(130, 47);
			this.userPasswordBox.Name = "userPasswordBox";
			this.userPasswordBox.Size = new System.Drawing.Size(226, 20);
			this.userPasswordBox.TabIndex = 19;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 50);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(105, 13);
			this.label9.TabIndex = 20;
			this.label9.Text = "User-password:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// autostart
			// 
			this.autostart.Checked = true;
			this.autostart.CheckState = System.Windows.Forms.CheckState.Checked;
			this.autostart.Location = new System.Drawing.Point(6, 343);
			this.autostart.Name = "autostart";
			this.autostart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.autostart.Size = new System.Drawing.Size(89, 17);
			this.autostart.TabIndex = 21;
			this.autostart.Text = ":Auto-start";
			this.autostart.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3, 379);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(361, 43);
			this.label10.TabIndex = 22;
			this.label10.Text = "CARE: \r\nIf you already installed MTA the old folder will be removed!";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// email
			// 
			this.email.Location = new System.Drawing.Point(130, 303);
			this.email.Name = "email";
			this.email.Size = new System.Drawing.Size(226, 20);
			this.email.TabIndex = 23;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 306);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(107, 13);
			this.label11.TabIndex = 24;
			this.label11.Text = "E-mail-address:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(202, 436);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(109, 23);
			this.button2.TabIndex = 25;
			this.button2.Text = "Back";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// error
			// 
			this.error.ContainerControl = this;
			// 
			// MTAWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(368, 469);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.email);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.autostart);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.userPasswordBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.bulletsync);
			this.Controls.Add(this.skinmodding);
			this.Controls.Add(this.fpslimit);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.serverpassword);
			this.Controls.Add(this.maxplayers);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.servername);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.asePort);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.httpPort);
			this.Controls.Add(this.serverPort);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.usernameBox);
			this.Controls.Add(this.label1);
			this.Name = "MTAWindow";
			this.Text = "MTA Installer";
			((System.ComponentModel.ISupportInitialize)(this.serverPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.httpPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.maxplayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpslimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox usernameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown serverPort;
		private System.Windows.Forms.NumericUpDown httpPort;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox asePort;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox servername;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown maxplayers;
		private System.Windows.Forms.TextBox serverpassword;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown fpslimit;
		private System.Windows.Forms.CheckBox skinmodding;
		private System.Windows.Forms.CheckBox bulletsync;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.TextBox userPasswordBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox autostart;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox email;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ErrorProvider error;
	}
}