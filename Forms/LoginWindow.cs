using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinuxMTAInstaller.Forms {
	public partial class LoginWindow : Form {
		public static LoginWindow instance;
		public LoginWindow ( Point Location ) {
			InitializeComponent ();
			instance = this;
			this.Location = Location;
			this.label1.Text = Languages.GetLang ( "login_with_root" );
			this.label3.Text = Languages.GetLang ( "ssh_port" );
			this.label4.Text = Languages.GetLang ( "username" );
			this.label5.Text = Languages.GetLang ( "password" );
			this.button1.Text = Languages.GetLang ( "login" );
			instance.Show ();
		}

		private void button1_Click ( object sender, EventArgs e ) {
			Connection.Connection.Login ( this.iporhostBox.Text, int.Parse ( this.portBox.Text ), this.usernameBox.Text, this.passwordBox.Text );
		}

		public static void notification ( string text, string type ) {
			if ( type == "error" ) {
				instance.notifyIcon1.Icon = SystemIcons.Error;
				instance.notifyIcon1.Visible = true;
				instance.notifyIcon1.Text = "Fehler";
				instance.notifyIcon1.BalloonTipText = text;
				instance.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Error;
				instance.notifyIcon1.ShowBalloonTip ( 10000 );
			}
		}
	}
}
