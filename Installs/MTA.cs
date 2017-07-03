// used for connection to the server //
using WinSCP;
// used for Console and Exception //
using System;

/**
 * Installs MTA
 * */
namespace LinuxMTAInstaller.Installs {
	static class MTA {

		/**
		 * static method to install MTA
		 * getting called by a button in MainWindow
		 * 
		 * @param string user username of the user running MTA
		 * @param string userpassword password of the user running MTA
		 * @param bool autostart Create an auto-starter?
		 * @param string servername Name of the server
		 * @param string email E-Mail of the server-owner, used for mtaserver.conf
		 * @param string serverPort server-port of server (default: 22003)
		 * @param string maxPlayers maxplayers setting
		 * @param string httpPort http-port of server (default: 22005)
		 * @param bool skinmodding Skin-modding allowed?
		 * @param string serverpassword Password of the server
		 * @param bool bulletsync Bullet-sync on
		 * @param string fpslimit FPS-Limit
		 * */
		public static void InstallMTA ( string user, string userpassword, bool autostart, string servername, string email, string serverPort, string maxPlayers, string httpPort, bool skinmodding, string serverpassword, bool bulletsync, string fpslimit ) {
			try {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				using ( Session session = new Session () ) {
					// needed to be able to use the self-created WinSCP.exe //
					session.DisableVersionCheck = true;
					session.Open ( Connection.Connection.options );
					Connection.Connection.AddUser ( session, user, userpassword );
					InstallMTAFiles ( session, user );
					InstallMTAShell ( session, user, autostart );
					SetupMTA ( session, user, servername, email, serverPort, maxPlayers, httpPort, skinmodding, serverpassword, bulletsync, fpslimit );
					Connection.Connection.Exec ( session, "chown -cR "+user+" /home/"+user );
					session.Close ();
					new Forms.MTAWindowEnd ( Forms.MTAWindow.instance.Location, user );
					Forms.MTAWindow.instance.Close ();
				}
			} catch ( Exception e ) {
				Console.Write ( e.ToString () );
				Forms.MTAWindow.notification ( e.ToString (), "error" );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}

		/**
		 * static method to create the shell-script to easily start and stop MTA on a screen
		 * 
		 * @param Session session The opened session
		 * @param string user The username of the user who will run MTA
		 * @param bool autostart Will this shell-script get executed on every boot?
		 * */
		private static void InstallMTAShell ( Session session, string user, bool autostart ) {
			try {
				Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "Screen ..." );
				Connection.Connection.Exec ( session, "apt-get -qq install screen" );

				Useful.Useful.Titel ( Languages.GetLang ( "creating" ) + "Shell-Script ..." );
				if ( Connection.Connection.bit64 ) {
					Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.MTAAutoStartInstaller64.sh", "mta.sh", user );
				} else
					Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.MTAAutoStartInstaller.sh", "mta.sh", user );
				Connection.Connection.PutFile ( session, "mta.sh", "/etc/" );
				Connection.Connection.Exec ( session, "chmod 777 /etc/mta.sh" );
				if ( autostart )
					Connection.Connection.SaveInRCLocal ( session, @"\/etc\/mta.sh start" );
			} catch ( Exception e ) {
				Console.WriteLine ( e.ToString () );
			}
		}

		/**
		 * static method to setup MTA like the user wanted
		 * 
		 * @param Session session The opened session
		 * @param string user The name of the user who will run MTA
		 * @param string servername Theh name of the server on MTA
		 * @param string email MTA needs your email, so give it to them
		 * @param string serverPort The port for the MTA-server (default: 22003)
		 * @param string maxPlayers maxplayers setting
		 * @param string httpPort http-port of server (default: 22005)
		 * @param bool skinmodding Skin-modding allowed?
		 * @param string serverpassword Password of the server
		 * @param bool bulletsync Bullet-sync on
		 * @param string fpslimit FPS-Limit
		 * */
		private static void SetupMTA ( Session session, string user, string servername, string email, string serverPort, string maxPlayers, string httpPort, bool skinmodding, string serverpassword, bool bulletsync, string fpslimit ) {
			Connection.Connection.GetFile ( session, "/home/" + user + "/MTA/mods/deathmatch/mtaserver.conf", "mtaserver.xml", false );

			Useful.Useful.Titel ( "MTA-Setup" );

			Console.WriteLine ( Languages.GetLang ( "changing_server_name" ) );

			// mtaserver.conf is a xml file, so use it like that ;) //
			Files.Files.ChangeXMLValue ( "mtaserver.xml", "servername", servername );

			if ( email != "" ) {
				Console.WriteLine ( Languages.GetLang ( "changing_server_email" ) );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "owner_email_address", email );
			}

			if ( serverPort != "22003" ) {
				Console.WriteLine ( Languages.GetLang ( "changing_server_port" ) );
				Connection.Connection.mtaserverport = int.Parse ( serverPort );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "serverport", serverPort );
			}

			if ( httpPort != "22005" ) {
				Console.WriteLine ( Languages.GetLang ( "changing_server_http_port" ) );
				Connection.Connection.mtahttpport = int.Parse ( httpPort );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "httpport", httpPort );
			}

			if ( maxPlayers != "32" ) {
				Console.WriteLine ( Languages.GetLang ( "changing_server_max_players" ) );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "maxplayers", maxPlayers );
			}

			if ( skinmodding ) {
				Console.WriteLine ( Languages.GetLang ( "activate_server_skin_modding" ) );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "allowing_gta3_img_mods", "peds" );
			}

			if ( serverpassword != "" ) {
				Console.WriteLine ( Languages.GetLang ( "setting_server_password" ) );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "password", serverpassword );
			}

			if ( !bulletsync ) {
				Console.WriteLine ( Languages.GetLang ( "deactivating_bullet_sync" ) );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "bullet_sync", "0" );
			}

			if ( fpslimit != "36" ) {
				Console.WriteLine ( Languages.GetLang ( "changing_server_fps_limit" ) );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "fpslimit", fpslimit );
			}

			Connection.Connection.PutFile ( session, "mtaserver.xml", "/home/" + user + "/MTA/mods/deathmatch/mtaserver.conf" );

			Console.WriteLine ( Languages.GetLang ( "changing_server_fps_limit" ) );
		}

		/**
		 * static method to install the MTA files
		 * MTA will be at /home/[USER]/MTA/
		 * 
		 * @param Session session The opened session
		 * @param string user The name of the user who will run MTA
		 * */
		private static void InstallMTAFiles ( Session session, string user ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "MTA ..." );
			Connection.Connection.Exec ( session, "cd /home/" + user + "/" );
			Console.WriteLine ( Languages.GetLang ( "creating_sh_installer" ) );
			Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.MTAInstaller.sh", "MTAInstaller.sh" );
			Connection.Connection.PutFile ( session, "MTAInstaller.sh", "/home/" + user + "/" );

			Connection.Connection.Exec ( session, "chmod 777 MTAInstaller.sh" );

			Console.WriteLine ( Languages.GetLang ( "installing_needed_packages" ) );
			// used by MTAInstaller.sh //
			Connection.Connection.Exec ( session, "apt-get -qq install bash" );
			Connection.Connection.Exec ( session, "apt-get -qq install unzip" );
			Connection.Connection.Exec ( session, "apt-get -qq install tar" );
			Connection.Connection.Exec ( session, "apt-get -qq install wget" );

			Console.WriteLine ( Languages.GetLang ( "removing_old_MTA_files" ) );
			Connection.Connection.Exec ( session, "rm -rf MTA" );

			Console.WriteLine ( Languages.GetLang ( "downloading_MTA_files" ) );
			Connection.Connection.Exec ( session, "./MTAInstaller.sh" );
			Console.WriteLine ( "MTA" + Languages.GetLang ( "was_installed" ) );
			Connection.Connection.Exec ( session, "cd /home/" + user + "/" );
			Connection.Connection.Exec ( session, "rm -f ./MTAInstaller.sh" );
		}

	}
}
