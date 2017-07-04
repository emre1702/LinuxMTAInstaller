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
			this.Location = Location;
			this.label1.Text = Languages.GetLang ( "mysql_database_system" );
			this.label2.Text = Languages.GetLang ( "mysql_password" );
			this.label3.Text = Languages.GetLang ( "website_application" );
			this.label4.Text = Languages.GetLang ( "phpmyadmin_url" );
			this.label5.Text = Languages.GetLang ( "hide_dbs" );
			this.button1.Text = Languages.GetLang ( "install" );
			this.newuserfordb.Text = Languages.GetLang ( "create_new_user_for_db" );
			this.labelmysqlusername.Text = Languages.GetLang ( "username" );
			this.labelmysqluserpw.Text = Languages.GetLang ( "password" );
			this.label6.Text = Languages.GetLang ( "database_name" );
			this.grantalluser.Text = Languages.GetLang ( "full_permissions" );
			this.button2.Text = Languages.GetLang ( "back" );
			this.useronlylocal.Text = Languages.GetLang ( "permissions_only_local" );
			this.Show ();
			
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
			this.error.Clear ();
			if ( this.url.Text != "" ) {
				if ( this.password.Text != "" ) {
					if ( !this.newuserfordb.Checked || this.mysqlusername.Text != "" ) {
						if ( !this.newuserfordb.Checked || this.mysqluserpw.Text != "" ) {
							Installs.Database.InstallDatabase ( this.databasesystem.Text, this.webapp.Text, this.url.Text, this.hidedb.GetItemChecked ( 0 ), this.hidedb.GetItemChecked ( 1 ), this.hidedb.GetItemChecked ( 2 ), this.hidedb.GetItemChecked ( 3 ), this.hidedb.GetItemChecked ( 4 ), this.password.Text, this.newuserfordb.Checked, this.mysqlusername.Text, this.mysqluserpw.Text, this.databasename.Text, this.grantalluser.Checked, this.useronlylocal.Checked );
						} else
							this.error.SetError ( this.mysqluserpw, Languages.GetLang ( "content_missing" ) );
					} else
						this.error.SetError ( this.mysqlusername, Languages.GetLang ( "content_missing" ) );
				} else
					this.error.SetError ( this.password, Languages.GetLang ( "content_missing" ) );
			} else 
				this.error.SetError ( this.url, Languages.GetLang ( "content_missing" ) );
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
