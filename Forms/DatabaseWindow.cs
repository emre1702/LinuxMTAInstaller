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
	public partial class DatabaseWindow : Form {
		public static DatabaseWindow instance;
		public DatabaseWindow ( Point Location ) {
			InitializeComponent ();
			instance = this;
			this.Show ();
			this.Location = Location;
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

		private void button1_Click ( object sender, EventArgs e ) {
			Connection.Connection.InstallDatabase ( this.databasesystem.Text, this.webapp.Text, this.url.Text, this.hidedb.GetItemChecked ( 0 ), this.hidedb.GetItemChecked ( 1 ), this.hidedb.GetItemChecked ( 2 ), this.hidedb.GetItemChecked ( 3 ), this.hidedb.GetItemChecked ( 4 ), this.password.Text, this.newuserfordb.Checked, this.mysqlusername.Text, this.mysqluserpw.Text, this.databasename.Text, this.grantalluser.Checked, this.useronlylocal.Checked );
		}

		private void newuserfordb_CheckedChanged ( object sender, EventArgs e ) {
			if ( this.newuserfordb.Checked ) {
				this.labelmysqlusername.Show ();
				this.labelmysqluserpw.Show ();
				this.mysqlusername.Show ();
				this.mysqluserpw.Show ();
				this.grantalluser.Show ();
			} else {
				this.labelmysqlusername.Hide ();
				this.labelmysqluserpw.Hide ();
				this.mysqlusername.Hide ();
				this.mysqluserpw.Hide ();
				this.grantalluser.Hide ();
			}
		}

		private void button2_Click ( object sender, EventArgs e ) {
			Forms.MainWindow.instance.Location = this.Location;
			this.Close ();
			Forms.MainWindow.instance.Show ();
		}

		private void DatabaseWindow_Load ( object sender, EventArgs e ) {
			this.hidedb.SetItemChecked ( 0, true );
			this.hidedb.SetItemChecked ( 1, true );
			this.hidedb.SetItemChecked ( 2, true );
			this.hidedb.SetItemChecked ( 3, true );
			this.hidedb.SetItemChecked ( 4, true );

			this.databasesystem.SelectedIndex = 0;
			this.webapp.SelectedIndex = 1;
		}
	}
}
