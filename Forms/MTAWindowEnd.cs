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
	public partial class MTAWindowEnd : Form {
		public MTAWindowEnd ( Point Location ) {
			InitializeComponent ();
			this.Show ();
			this.Location = Location;
		}

		private void button1_Click ( object sender, EventArgs e ) {
			this.Close ();
			Forms.MainWindow.instance.Location = this.Location;
			Forms.MainWindow.instance.Show ();
		}
	}
}
