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
	public partial class StartWindow : Form {
		public StartWindow ( ) {
			InitializeComponent ();
		}

		private void button1_Click ( object sender, EventArgs e ) {
			this.Hide ();
			new Forms.LoginWindow ( this.Location );
			
		}

		private void StartWindow_Load ( object sender, EventArgs e ) {
			this.comboBox1.SelectedIndex = 0;
			Languages.lang = "English";
			this.label1.Text = Languages.GetLang ( "StartWindow_info" );
			this.label2.Text = Languages.GetLang ( "language" );
		}

		private void ChangeLanguage ( object sender, EventArgs e ) {
			Languages.lang = this.comboBox1.GetItemText ( this.comboBox1.SelectedItem );
			this.label1.Text = Languages.GetLang ( "StartWindow_info" );
			this.label2.Text = Languages.GetLang ( "language" );
		}
	}
}
