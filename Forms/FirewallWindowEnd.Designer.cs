﻿namespace LinuxMTAInstaller.Forms {
	partial class FirewallWindowEnd {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirewallWindowEnd));
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(400, 159);
			this.label1.TabIndex = 0;
			this.label1.Text = Languages.GetLang ( "firewall_installed_form" );
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(144, 200);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(129, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = Languages.GetLang ( "back" );
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FirewallWindowEnd
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(425, 235);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Name = "FirewallWindowEnd";
			this.Text = "FirewallWindowEnd";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
	}
}