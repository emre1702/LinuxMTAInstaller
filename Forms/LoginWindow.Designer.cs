namespace LinuxMTAInstaller.Forms {
	partial class LoginWindow {
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
			this.iporhostBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.usernameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.passwordBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.portBox = new System.Windows.Forms.NumericUpDown();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			((System.ComponentModel.ISupportInitialize)(this.portBox)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(36, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(233, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Melde dich zuerst in deinen Server MIT root ein.";
			// 
			// iporhostBox
			// 
			this.iporhostBox.Location = new System.Drawing.Point(118, 59);
			this.iporhostBox.Name = "iporhostBox";
			this.iporhostBox.Size = new System.Drawing.Size(177, 20);
			this.iporhostBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Server-IP/Host:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "SSH-Port:";
			// 
			// usernameBox
			// 
			this.usernameBox.Enabled = false;
			this.usernameBox.Location = new System.Drawing.Point(118, 111);
			this.usernameBox.Name = "usernameBox";
			this.usernameBox.ReadOnly = true;
			this.usernameBox.Size = new System.Drawing.Size(177, 20);
			this.usernameBox.TabIndex = 5;
			this.usernameBox.Text = "root";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(24, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Benutzername:";
			// 
			// passwordBox
			// 
			this.passwordBox.Location = new System.Drawing.Point(118, 137);
			this.passwordBox.Name = "passwordBox";
			this.passwordBox.Size = new System.Drawing.Size(177, 20);
			this.passwordBox.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(24, 140);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(50, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Passwort";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(118, 181);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(86, 32);
			this.button1.TabIndex = 9;
			this.button1.Text = "Anmelden";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// portBox
			// 
			this.portBox.Location = new System.Drawing.Point(118, 86);
			this.portBox.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.portBox.Name = "portBox";
			this.portBox.Size = new System.Drawing.Size(177, 20);
			this.portBox.TabIndex = 10;
			this.portBox.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Error;
			this.notifyIcon1.Text = "notifyIcon1";
			// 
			// LoginWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 225);
			this.Controls.Add(this.portBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.passwordBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.usernameBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.iporhostBox);
			this.Controls.Add(this.label1);
			this.Name = "LoginWindow";
			this.Text = "Server-Login";
			((System.ComponentModel.ISupportInitialize)(this.portBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox iporhostBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox usernameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox passwordBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NumericUpDown portBox;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
	}
}