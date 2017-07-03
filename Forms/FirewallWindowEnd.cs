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
	public partial class FirewallWindowEnd : Form {
		public FirewallWindowEnd ( Point Location ) {
			InitializeComponent ();
			this.Location = Location;
			this.label1.Text = Languages.GetLang ( "firewall_installed_form" );
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
