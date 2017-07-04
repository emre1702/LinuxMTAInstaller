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
			this.label1.Text = Languages.GetLang ( "running_user" );
			this.label2.Text = "Server-" + Languages.GetLang ( "port" ) + " (UDP):";
			this.label3.Text = "HTTP-" + Languages.GetLang ( "port" ) + " (TCP):";
			this.label4.Text = "ASE-" + Languages.GetLang ( "port" ) + " (UDP):";
			this.label5.Text = Languages.GetLang ( "server_name" );
			this.label6.Text = Languages.GetLang ( "max_players" );
			this.label7.Text = Languages.GetLang ( "server_password" );
			this.label8.Text = Languages.GetLang ( "fps_limit" );
			this.skinmodding.Text = Languages.GetLang ( "skin_modding" );
			this.bulletsync.Text = Languages.GetLang ( "bullet_sync" );
			this.button1.Text = Languages.GetLang ( "install" );
			this.label9.Text = Languages.GetLang ( "user_password" );
			this.autostart.Text = Languages.GetLang ( "auto_start" );
			this.label10.Text = Languages.GetLang ( "install_will_delete_old" );
			this.label11.Text = Languages.GetLang ( "email_address" );
			this.button2.Text = Languages.GetLang ( "back" );

			instance.Show ();
			this.Location = Location;
		}

		private void button1_Click ( object sender, EventArgs e ) {
			this.error.Clear ();
			if ( this.usernameBox.Text != "" ) {
				if ( this.servername.Text != "" ) {
					Installs.MTA.InstallMTA ( this.usernameBox.Text, this.userPasswordBox.Text, this.autostart.Checked, this.servername.Text, this.email.Text, this.serverPort.Value.ToString (), this.maxplayers.Value.ToString (), this.httpPort.Value.ToString (), this.skinmodding.Checked, this.serverpassword.Text, this.bulletsync.Checked, this.fpslimit.Value.ToString () );
				} else
					this.error.SetError ( this.servername, Languages.GetLang ( "content_missing" ) );
			} else
				this.error.SetError ( this.usernameBox, Languages.GetLang ( "content_missing" ) );
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

		private void label1_Click ( object sender, EventArgs e ) {

		}
	}
}
