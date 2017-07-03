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
	public partial class TeamspeakWindowEnd : Form {
		public TeamspeakWindowEnd ( Point Location, string token, string serveradminpw ) {
			InitializeComponent ();
			this.Location = Location;
			this.label1.Text = Strings.Strings.GetReplacedString ( Languages.GetLang ( "teamspeak_installed_info" ), token, serveradminpw );
			this.button1.Text = Languages.GetLang ( "back" );
			this.Show ();
		}

		private void button1_Click ( object sender, EventArgs e ) {
			Forms.MainWindow.instance.Location = this.Location;
			this.Close ();
			Forms.MainWindow.instance.Show ();
		}
	}
}
