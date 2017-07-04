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
	public partial class TeamspeakWindow : Form {
		public static TeamspeakWindow instance;
		public TeamspeakWindow ( Point Location ) {
			InitializeComponent ();
			this.Location = Location;
			instance = this;
			this.label1.Text = Languages.GetLang ( "running_user" );
			this.label2.Text = Languages.GetLang ( "user_password" );
			this.label3.Text = Languages.GetLang ( "serveradmin_password" );
			this.button1.Text = Languages.GetLang ( "install" );
			this.label4.Text = Languages.GetLang ( "serveradmin_password_info" );
			this.button2.Text = Languages.GetLang ( "back" );
			this.Show (); 
		}

		private void button1_Click ( object sender, EventArgs e ) {
			this.error.Clear ();
			if ( this.username.Text != "" ) {
				if ( this.userpw.Text != "" ) {  
					Installs.Teamspeak.InstallTeamspeak ( this.username.Text, this.userpw.Text, this.serveradminpw.Text );
				} else
					this.error.SetError ( this.userpw, Languages.GetLang ( "content_missing" ) );
			} else 
				this.error.SetError ( this.username, Languages.GetLang ( "content_missing" ) );
		}

		private void button2_Click ( object sender, EventArgs e ) {
			Forms.MainWindow.instance.Location = this.Location;
			this.Close ();
			Forms.MainWindow.instance.Show ();
		}
	}
}
