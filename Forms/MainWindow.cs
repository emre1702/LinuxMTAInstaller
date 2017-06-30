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
	public partial class MainWindow : Form {
		public static MainWindow instance;
		public MainWindow ( Point Location ) {
			InitializeComponent ();
			instance = this;
			instance.Show ();
			this.Location = Location;
		}

		private void installMTA_Click ( object sender, EventArgs e ) {
			this.Hide ();
			new Forms.MTAWindow ( this.Location );
		}

		private void installDatabase_Click ( object sender, EventArgs e ) {
			this.Hide ();
			new Forms.DatabaseWindow ( this.Location );
		}

		private void installFirewall_Click ( object sender, EventArgs e ) {
			this.Hide ();
			Connection.Connection.InstallFirewall ();
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
