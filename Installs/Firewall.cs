// used for connection to the server //
using WinSCP;
// used for Console and Exception //
using System;

/** 
 * Installs the Firewall
 * sends a .sh file to /etc/firewall.sh
 * The shell-script uses IPTables
 * */
namespace LinuxMTAInstaller.Installs {
	static class Firewall {

		/** 
		* Installs the Firewall
		* getting called by a button in MainWindow
		* */
		public static void InstallFirewall ( ) {
			try {
				using ( Session session = new Session () ) {
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
					// needed to be able to use the self-created WinSCP.exe //
					session.DisableVersionCheck = true;
					session.Open ( Connection.Connection.options );
					Useful.Useful.Titel ( Languages.GetLang ( "installing_firewall_in" ) );
					Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.firewall.sh", "firewall.sh", Connection.Connection.mtaserverport.ToString (), Connection.Connection.mtahttpport.ToString (), ( Connection.Connection.mtaserverport + 123 ).ToString (), Connection.Connection.sshport.ToString () );
					Connection.Connection.PutFile ( session, "firewall.sh", "/etc/firewall.sh" );
					Connection.Connection.Exec ( session, "chmod u+x /etc/firewall.sh" );
					Connection.Connection.SaveInRCLocal ( session, @"\/etc\/firewall.sh" );
					Connection.Connection.Exec ( session, "/etc/firewall.sh" );
					session.Close ();
					new Forms.FirewallWindowEnd ( Forms.MainWindow.instance.Location );
				}
			} catch ( Exception e ) {
				Console.Write ( e.ToString () );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}
	}
}