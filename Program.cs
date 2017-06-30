using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSCP;

namespace LinuxMTAInstaller {
	static class Program {

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main ( ) {
		
			AppDomain.CurrentDomain.AssemblyResolve += ( sender, arg ) => { if ( arg.Name.StartsWith ( "WinSCPnet" ) ) return Assembly.Load ( Properties.Resources.WinSCPnet ); return null; };

			/*foreach ( string a in Assembly.GetExecutingAssembly ().GetManifestResourceNames () ) {
				Console.WriteLine ( a );
			}*/

			using ( FileStream fs = File.Create ( "WinSCP.exe" ) ) {
				Stream resource = Assembly.GetExecutingAssembly ().GetManifestResourceStream ( "LinuxMTAInstaller.Resources.WinSCP.exe" );
				resource.CopyTo ( fs );
			}

			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault ( false );
			Application.ApplicationExit += new EventHandler ( OnProcessExit );
			Application.Run ( new Forms.StartWindow () );
		}

		private static void OnProcessExit ( object sender, EventArgs e ) {
			File.Delete ( "WinSCP.exe" );
			File.Delete ( "WinSCP.ini" );
		}
	}
}



