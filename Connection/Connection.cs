// used for connection to the server //
using WinSCP;
// used for Console and Exception //
using System;

namespace LinuxMTAInstaller.Connection {
	/**
	 * static class with some methods and variables for connection to the server.
	 * */
	static class Connection {
		// settings for the connection //
		public static SessionOptions options;
		// IP of the server //
		public static string serverip;
		// password of the root user on the server //
		public static string rootpw;
		// Can install php7.0 (true) or only php5 (false) //
		public static bool old;
		// 64 Bit (true) or 32 Bit (false) //
		public static bool bit64;
		// MTA server-port (for firewall) //
		public static int mtaserverport = 22003;
		// MTA HTTP-port (for firewall) //
		public static int mtahttpport = 22005;
		// SSH port (for firewall) //
		public static int sshport = 22;

		/**
		 * static Login method
		 * Used to get the server-data to be able to connect to the server
		 * 
		 * @param string hostorip The host or IP of the server
		 * @param int port SSH-port of the server
		 * @param string username The username, will be always root, dunno why I used it here
		 * @param string password The password of the user (root)
		 * */
		public static void Login ( string hostorip, int port, string username, string password ) {
			try {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				options = new SessionOptions {
					HostName = hostorip,
					UserName = username,
					Password = password,
					PortNumber = port,
					// so we don't get an error //
					GiveUpSecurityAndAcceptAnySshHostKey = true,
				};
				serverip = hostorip;
				rootpw = password;
				sshport = port;
				using ( Session session = new Session () ) {
					// needed to be able to use the self-created WinSCP.exe //
					session.DisableVersionCheck = true;
					session.Open ( options );
					// First update the server //
					Console.WriteLine ( Languages.GetLang ( "updating_the_server" ) );
					Exec ( session, "apt-get update -qq" );
					// Sometimes getting a dpkg error, so fix it //
					Exec ( session, "dpkg --configure -a" );
					Exec ( session, "apt-get dist-upgrade -qq" );
					Exec ( session, "apt-get autoremove -qq" );
					Exec ( session, "apt-get autoclean -qq" );
					Console.WriteLine ( Languages.GetLang ( "update_successful" ) );

					// Check if it's 64 Bit or 32 Bit //
					bit64 = session.ExecuteCommand ( "uname -m" ).Output == "x86_64";
					// Check if php7.0 is available or we have to use php5
					old = session.ExecuteCommand ( "apt-cache search php7.0" ).Output == null;

					session.Close ();
				}
				new Forms.MainWindow ( Forms.LoginWindow.instance.Location );
				Forms.LoginWindow.instance.Close ();
			} catch ( Exception e ) {
				Forms.LoginWindow.notification ( e.ToString (), "error" );
				Console.WriteLine ( e.ToString() );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}

		/**
		 * static method to send commands to the server 
		 * 
		 * @param Session session The opened session
		 * @param string command The command which we want to send to the server
		 * */
		public static void Exec ( Session session, string command ) {
			Console.WriteLine ( command );
			CommandExecutionResult cmd = session.ExecuteCommand ( command );
			// Check for errors //
			cmd.Check ();
			// Let the user know what we did //
			Console.WriteLine ( cmd.Output );
		}

		/**
		 * static method to download a file from the server
		 * 
		 * @param Session session The opened session
		 * @param string serverpath Path of the file on the server
		 * @param string localpath Where we want to save the file
		 * @param bool remove Default: true - Remove the file from the server?
		 * @param TransferMode mode Default: TransferMode.Binary - The Transfermode we want to use
		 * */
		public static void GetFile ( Session session, string serverpath, string localpath, bool remove = true, TransferMode mode = TransferMode.Binary ) {
			TransferOptions transferOptions = new TransferOptions ();
			transferOptions.TransferMode = mode;
			TransferOperationResult transferResult;
			transferResult = session.GetFiles ( serverpath, localpath, remove, transferOptions );
			transferResult.Check ();
			foreach ( TransferEventArgs transfer in transferResult.Transfers ) {
				Console.WriteLine ( Languages.GetLang ( "donwload_of_succeeded" ), transfer.FileName );
			}
		}

		/**
		 * static method to upload a file to the server
		 * 
		 * @param Session session The opened session
		 * @param string localpath The path of the file we want to upload
		 * @param string serverpath Where we want to put the file on the server
		 * @param bool remove Default: true - Remove the file from local?
		 * @param TransferMode mode Default: TransferMode.Binary - The Transfermode we want to use
		 * */
		public static void PutFile ( Session session, string localpath, string serverpath, bool remove = true, TransferMode mode = TransferMode.Ascii ) {
			TransferOptions transferOptions = new TransferOptions ();
			transferOptions.TransferMode = mode;
			TransferOperationResult transferResultAutostarter = session.PutFiles ( localpath, serverpath, remove, transferOptions );
			transferResultAutostarter.Check ();
			foreach ( TransferEventArgs transfer in transferResultAutostarter.Transfers ) {
				Console.WriteLine ( Languages.GetLang ( "upload_of_succeeded" ), transfer.FileName );
			}
		}

		/** 
		 * static method for adding a user to the server
		 * 
		 * @param Session session The opened session
		 * @param string user Username of the user we want to create
		 * @param string password Password of the user we want to create
		 * */
		public static void AddUser ( Session session, string user, string password ) {
			Useful.Useful.Titel ( Languages.GetLang ( "creating_user" ) );
			// Check if the user already exists - if not, create him without getting any prompt //
			Exec ( session, "id -u " + user + @" &>/dev/null || adduser --disabled-password --gecos """" " + user );
			// Change his password //
			Exec ( session, @"echo """ + user + ":" + password + @""" | chpasswd" );
			Console.WriteLine ( Languages.GetLang ( "user_created" ) );
		}

		/** 
		 * static method for saving an auto-starter in /etc/rc.local
		 * Then rc.local will execute the command on boot -> auto-start
		 * 
		 * @param Session session The opened session
		 * @param string cmd The command we want to add to rc.local
		 * */
		public static void SaveInRCLocal ( Session session, string cmd ) {
			try {
				Useful.Useful.Titel ( Languages.GetLang ( "setting_up_auto-starter" ) );
				// Remove empty lines //
				Exec ( session, "sed -i '/^$/d' /etc/rc.local" );
				// Remove the cmd if we already put it in //
				Exec ( session, "sed -i '/" + cmd + "/d' /etc/rc.local" );
				// Put the cmd in //
				Exec ( session, "sed -i '$i" + cmd + "' /etc/rc.local" );
				Console.WriteLine ( Languages.GetLang ( "auto-starter_installed" ) );
			} catch ( Exception e ) {
				Console.WriteLine ( e.ToString () );
			}
		}
	}

}
