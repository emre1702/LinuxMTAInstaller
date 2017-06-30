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
	public partial class MTAWindow : Form {
		public static MTAWindow instance;
		public MTAWindow ( Point Location ) {
			InitializeComponent ();
			instance = this;
			instance.Show ();
			this.Location = Location;
		}

		private void button1_Click ( object sender, EventArgs e ) {
			if ( instance.usernameBox.Text.Length > 0 ) {
				Connection.Connection.InstallMTA ( this.usernameBox.Text, this.userPasswordBox.Text, this.autostart.Checked, this.servername.Text, this.email.Text, this.serverPort.Value.ToString(), this.maxplayers.Value.ToString (), this.httpPort.Value.ToString(), this.skinmodding.Checked, this.serverpassword.Text, this.bulletsync.Checked, this.fpslimit.Value.ToString() );
			}
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

		private void calculateAsePort ( object sender, EventArgs e ) {
			this.asePort.Text = ( this.serverPort.Value + 123 ).ToString(); 
		}

		private void button2_Click ( object sender, EventArgs e ) {
			Forms.MainWindow.instance.Location = this.Location;
			this.Close ();
			Forms.MainWindow.instance.Show();
		}
	}
}
