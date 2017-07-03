// used for connection to the server //
using WinSCP;
// used for Console and Exception //
using System;

/**
 * Install Teamspeak 3
 * */
namespace LinuxMTAInstaller.Installs {
	static class Teamspeak {

		/**
		 * static method to install Teamspeak3
		 * gets called by MainWindow button
		 * 
		 * @param string username The name of the user who will run TS3
		 * @param string userpassword The password of the user who will run TS3
		 * @param string serveradminpassword The password for TS3-serveradmin
		 * */
		public static void InstallTeamspeak ( string username, string userpassword, string serveradminpassword ) {
			try {
				using ( Session session = new Session () ) {
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
					// needed to be able to use the self-created WinSCP.exe //
					session.DisableVersionCheck = true;
					session.Open ( Connection.Connection.options );
					// Remove previous installation //
					Connection.Connection.Exec ( session, "killall teamspeak 2>/dev/null" );
					Connection.Connection.Exec ( session, "rm -rf /home/" + username + "/Teamspeak" );
					Connection.Connection.AddUser ( session, username, userpassword );
					DownloadTeamspeak ( session, username );
					InstallDownloadedTeamspeak ( session, username );
					GetServeradminPasswordAndToken ( session, username, serveradminpassword, out serveradminpassword, out string admintoken );
					InstallTeamspeakAutostarter ( session, username );

					Console.WriteLine ( "Serveradmin Token: " + admintoken );
					Console.WriteLine ( "Serveradmin Passwort: " + serveradminpassword );
					session.Close ();
					new Forms.TeamspeakWindowEnd ( Forms.TeamspeakWindow.instance.Location, admintoken, serveradminpassword );
					Forms.TeamspeakWindow.instance.Close ();
				}
			} catch ( Exception e ) {
				Console.Write ( e.ToString () );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}

		/**
		 * static method to get the latest stable Teamspeak3 version
		 * Gets the version from the official Teamspeak website.
		 * 
		 * @param Session session The opened session
		 * 
		 * @return string Returns the latest stable TS3 version
		 * */
		private static string GetLatestStableTeamspeakVersion ( Session session ) {
			Connection.Connection.Exec ( session, "apt-get install -qq jshon" );
			string output = session.ExecuteCommand ( "wget - t 1 - T 3 https://www.teamspeak.com/versions/server.json -q -O - | jshon -e linux -e " + ( Connection.Connection.old ? "x86" : "x86_64" ) + @" -e version | tr -d '""'" ).Output;
			return output;
		}

		/**
		 * static method to get the download path for the latest stable Teamspeak3 version
		 * 
		 * @param Session session The opened session
		 * 
		 * @return string Return the download-path for the latest TS3 
		 * */
		private static string GetTeamspeakDownloadPath ( Session session ) {
			string path = "http://dl.4players.de/ts/releases/VERSION/teamspeak3-server_linux_BITVERS-VERSION.tar.bz2";
			string version = GetLatestStableTeamspeakVersion ( session );
			path = path.Replace ( "VERSION", version );
			path = path.Replace ( "BITVERS", Connection.Connection.bit64 ? "amd64" : "x86" );
			return path;
		}

		/**
		 * static method to download the TS3 files
		 * 
		 * @param Session session The opened session
		 * @param string user The name of the user who will run TS3
		 * */
		private static void DownloadTeamspeak ( Session session, string user ) {
			string downloadpath = GetTeamspeakDownloadPath ( session );
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "Teamspeak3 ..." );
			Connection.Connection.Exec ( session, "cd /home/" + user + "/" );
			Connection.Connection.Exec ( session, "wget " + @downloadpath + " -q -O Teamspeak.tar.bz2" );
			Connection.Connection.Exec ( session, "apt-get -qq install tar" );
			Connection.Connection.Exec ( session, "tar -xf Teamspeak.tar.bz2" );
			Connection.Connection.Exec ( session, "mv teamspeak3-server_linux_" + ( Connection.Connection.bit64 ? "amd64" : "x86" ) + " Teamspeak" );
			Connection.Connection.Exec ( session, "rm Teamspeak.tar.bz2" );
			Console.WriteLine ( "Teamspeak3" + Languages.GetLang ( "was_installed" ) );
		}

		/**
		 * static method to install the downloaded TS3 files
		 * 
		 * @param Session session The opened session
		 * @param string user The name of the user who will run TS3
		 * @param string originalserveradminpassword used in GetTeamspeakFirstExecuteInfos
		 * @param out string serveradminpassword Get it from GetTeamspeakFirstExecuteInfos
		 * @param out string admintoken Get it from GetTeamspeakFirstExecuteInfos
		 * */
		private static void InstallDownloadedTeamspeak ( Session session, string user ) {
			Useful.Useful.Titel ( Languages.GetLang ( "setting_up_teamspeak3" ) );
			Connection.Connection.Exec ( session, "cd /home/" + user + "/Teamspeak/" );
			Connection.Connection.Exec ( session, "chmod u+x ts3server" );
			Connection.Connection.Exec ( session, "chmod u+x ts3server_minimal_runscript.sh" );
			Connection.Connection.Exec ( session, "chmod u+x ts3server_startscript.sh" );
			Connection.Connection.Exec ( session, "./ts3server createinifile=1" );
			Connection.Connection.Exec ( session, "touch query_ip_blacklist.txt query_ip_whitelist.txt " );
			Connection.Connection.Exec ( session, "chown -cR " + user + " /home/" + user );
		}

		/**
		 * static method to get serveradmin-password and token for serveradmin
		 * 
		 * @param Session session The opened session
		 * @param string user The name of the user who will run TS3
		 * @param string originalserveradminpassword used in GetTeamspeakFirstExecuteInfos
		 * @param out string serveradminpassword Get it from GetTeamspeakFirstExecuteInfos
		 * @param out string admintoken Get it from GetTeamspeakFirstExecuteInfos
		 * */
		private static void GetServeradminPasswordAndToken ( Session session, string user, string originalserveradminpassword, out string serveradminpassword, out string admintoken ) {
			Useful.Useful.Titel ( Languages.GetLang ( "getting_serveradmin_password_and_token" ) );
			Connection.Connection.Exec ( session, "cd /home/" + user + "/Teamspeak/" );
			// Anti-Bug //
			Connection.Connection.Exec ( session, "mount -t tmpfs tmpfs /dev/shm" );
			Connection.Connection.Exec ( session, "fuser -k 30033/tcp" );
			/////////////
			if ( originalserveradminpassword == "" ) {
				Connection.Connection.Exec ( session, "./ts3server_startscript.sh start inifile=ts3server.ini" );
			} else {
				Connection.Connection.Exec ( session, "./ts3server_startscript.sh start inifile=ts3server.ini serveradmin_password='" + originalserveradminpassword + "'" );
			}
			// Workaround - WinSCP detects the password and token info as error-output in later executes, dunno why //
			CommandExecutionResult cmd = session.ExecuteCommand ( "apt-get -y update && apt-get -y upgrade && apt-get -y autoremove && apt-get -y autoclean" );
			string output = cmd.ErrorOutput;
			Console.WriteLine ( output );
			serveradminpassword = Strings.Strings.GetStringBetween ( output, "password= \"", "\"" );
			admintoken = Strings.Strings.GetStringBetween ( output, "token=", "\n" );
		}

		/**
		 * static method to install the auto-starter for TS3
		 * 
		 * @param Session session The opened session
		 * @param string user The name of the user who will run TS3
		 * */
		private static void InstallTeamspeakAutostarter ( Session session, string user ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing_teamspeak_autostarter" ) );
			Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.TeamspeakAutoStartInstaller.sh", "ts.sh", user );
			Connection.Connection.PutFile ( session, "ts.sh", "/etc/ts.sh" );
			Connection.Connection.Exec ( session, "chmod u+x /etc/ts.sh" );
			Connection.Connection.SaveInRCLocal ( session, @"\/etc\/ts.sh" );
		}
	}
}