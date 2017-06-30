using WinSCP;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Reflection;

namespace LinuxMTAInstaller.Connection {
	static class Connection {
		private static SessionOptions options;
		private static string serverip;
		private static string rootpw;
		private static bool old;
		private static bool bit64;
		private static int mtaserverport = 22003;
		private static int mtahttpport = 22005;

		public static void Login ( string hostorip, int port, string username, string password ) {
			try {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				options = new SessionOptions {
					Protocol = Protocol.Sftp,
					HostName = hostorip,
					UserName = username,
					Password = password,
					PortNumber = port,
					GiveUpSecurityAndAcceptAnySshHostKey = true,
				};
				serverip = hostorip;
				rootpw = password;
				using ( Session session = new Session() ) {
					session.DisableVersionCheck = true;
					session.Open ( options );
					Console.WriteLine ( "Update den Server ..." );
					Exec ( session, "apt-get update -qq" );
					Exec ( session, "apt-get dist-upgrade -qq" );
					Exec ( session, "apt-get autoremove -qq" );
					Exec ( session, "apt-get autoclean -qq" );
					Console.WriteLine ( "Update erfolgreich" );

					bit64 = session.ExecuteCommand ( "uname -m" ).Output == "x86_64";

					session.Close ();
				}
				new Forms.MainWindow ( Forms.LoginWindow.instance.Location );
				Forms.LoginWindow.instance.Close ();
			} catch ( Exception e ) {
				if ( e is SocketException )
					Forms.LoginWindow.notification ( "Keine Verbindung zum Server!", "error" );
				else
					Forms.LoginWindow.notification ( e.ToString (), "error" );
				Console.WriteLine ( e ); 
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}


		public static void Exec ( Session session, string command ) {
			Console.WriteLine ( command );
			CommandExecutionResult cmd = session.ExecuteCommand ( command );
			cmd.Check ();
			Console.WriteLine ( cmd.Output );
		}

		private static void GetFile ( Session session, string serverpath, string localpath, bool remove = true, TransferMode mode = TransferMode.Binary ) {
			TransferOptions transferOptions = new TransferOptions ();
			transferOptions.TransferMode = mode;

			TransferOperationResult transferResult;
			transferResult = session.GetFiles ( serverpath, localpath, remove, transferOptions );
			transferResult.Check ();
			foreach ( TransferEventArgs transfer in transferResult.Transfers ) {
				Console.WriteLine ( "Download of {0} succeeded", transfer.FileName );
			}
		}

		private static void PutFile ( Session session, string localpath, string serverpath, bool remove = true, TransferMode mode = TransferMode.Ascii ) {
			TransferOptions transferOptions = new TransferOptions ();
			transferOptions.TransferMode = mode;
			TransferOperationResult transferResultAutostarter = session.PutFiles ( localpath, serverpath, remove, transferOptions );
			transferResultAutostarter.Check ();
			foreach ( TransferEventArgs transfer in transferResultAutostarter.Transfers ) {
				Console.WriteLine ( "Upload of {0} succeeded", transfer.FileName );
			}
		}

		private static void Titel ( string info ) {
			Console.WriteLine ( "" );
			Console.WriteLine ( "__________________________________" );
			Console.WriteLine ( info );
			Console.WriteLine ( "__________________________________" );
		}

		private static void AddUser ( Session session, string user, string password ) {
			Titel ( "Erstelle Benutzer ..." );
			Exec ( session, "id -u " + user + @" &>/dev/null || adduser --disabled-password --gecos """" " + user );
			Exec ( session, @"echo """ + user+":"+ password + @""" | chpasswd" );
			Console.WriteLine ( "Benutzer erstellt" );
		}

		private static void SaveInRCLocal ( Session session, string cmd ) {
			try {
				Titel ( "Richte Auto-Installier ein" );
				Exec ( session, "sed -i '/^$/d' /etc/rc.local" );
				Exec ( session, "sed -i '/" + cmd+"/d' /etc/rc.local" );
				Exec ( session, "sed -i '$i" + cmd+"' /etc/rc.local" );
				Console.WriteLine ( "AutoStart erfolgreich installiert" );

			} catch ( Exception e ) {
				Console.WriteLine ( "Error beim Autostarter: " + e );
			}
		}

		private static void InstallMTAShell ( Session session, string user, bool autostart ) {
			try {
				Titel ( "Installiere Screen" );
				Exec ( session, "apt-get install screen" );

				Titel ( "Erstelle Shell-Script" );
				if ( bit64 ) {
					Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.MTAAutoStartInstaller64.sh", "mta.sh", user );
				} else
					Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.MTAAutoStartInstaller.sh", "mta.sh", user );
				PutFile ( session, "mta.sh", "/etc/" );
				Exec ( session, "chmod 777 /etc/mta.sh" );
				if ( autostart )
					SaveInRCLocal ( session, @"\/etc\/mta.sh start" );
			} catch ( Exception e ) {
				Console.WriteLine ( "Error beim Shell-Script: " + e );
			}
		}

		private static void SetupMTA ( Session session, string user, string servername, string email, string serverPort, string maxPlayers, string httpPort, bool skinmodding, string serverpassword, bool bulletsync, string fpslimit ) {
			GetFile ( session, "/home/"+user+"/MTA/mods/deathmatch/mtaserver.conf", "mtaserver.xml", false );

			Titel ( "MTA-Setup" );

			Console.WriteLine ( "Ändere Server-Namen ..." );
			Files.Files.ChangeXMLValue ( "mtaserver.xml", "servername", servername );

			if ( email != "" ) {
				Console.WriteLine ( "Ändere Server-E-Mail-Adresse ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "owner_email_address", email );
			}

			if ( serverPort != "22003" ) {
				Console.WriteLine ( "Ändere Server-Port ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "serverport", serverPort );
			}

			if ( httpPort != "22005" ) {
				Console.WriteLine ( "Ändere HTTP-Port ..." );
				mtahttpport = int.Parse ( httpPort );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "httpport", httpPort );
			}

			if ( maxPlayers != "32" ) {
				Console.WriteLine ( "Ändere Max-Spieler ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "maxplayers", maxPlayers );
			}

			if ( skinmodding ) {
				Console.WriteLine ( "Aktiviere Skin-Modding ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "allow_gta3_img_mods", "peds" );
			}

			if ( serverpassword != "" ) {
				Console.WriteLine ( "Stelle Server-Passwort ein ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "password", serverpassword );
			}

			if ( !bulletsync ) {
				Console.WriteLine ( "Deaktiviere Bullet-Sync ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "bullet_sync", "0" );
			}

			if ( fpslimit != "36" ) {
				Console.WriteLine ( "Ändere FPS-Limit ..." );
				Files.Files.ChangeXMLValue ( "mtaserver.xml", "fpslimit", fpslimit );
			}

			PutFile ( session, "mtaserver.xml", "/home/"+user+"/MTA/mods/deathmatch/mtaserver.conf" );

			Console.WriteLine ( "MTA wurde erfolgreich eingestellt!" );
		}

		private static void InstallMTAFiles ( Session session, string user ) {
			Titel ( "Installiere MTA ..." );
			Exec ( session, "cd /home/" + user+"/" );
			Console.WriteLine ( "Erstelle .sh Installer" );
			Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.MTAInstaller.sh", "MTAInstaller.sh" );
			PutFile ( session, "MTAInstaller.sh", "/home/" + user + "/" );

			Exec ( session, "chmod 777 MTAInstaller.sh" );

			Console.WriteLine ( "Installiere die benötigten Tools" );
			Exec ( session, "apt-get -y install bash" );
			Exec ( session, "apt-get -y install unzip" );
			Exec ( session, "apt-get -y install tar" );
			Exec ( session, "apt-get -y install wget" );

			Console.WriteLine ( "Entferne alte MTA-Daten" );
			Exec ( session, "rm -rf MTA" );

			Console.WriteLine ( "Lade die MTA-Daten runter" );
			Exec ( session, "./MTAInstaller.sh" );
			Console.WriteLine ( "MTA erfolgreich runtergeladen!" );
			Exec ( session, "cd /home/" + user + "/" );
			Exec ( session, "rm -f ./MTAInstaller.sh" );
		}

		private static void InstallMySQL ( Session session, string password ) {
			Titel ( "Installiere MySQL ..." );

			Exec ( session, "apt-get -y install debconf-utils" );
			Exec ( session, "debconf-set-selections <<< 'mysql-server mysql-server/root_password password " + password+"'" );
			Exec ( session, "debconf-set-selections <<< 'mysql-server mysql-server/root_password_again password " + password +"'" );
			Exec ( session, "apt-get -y install -qq mysql-server mysql-client" );
			Exec ( session, "printf $'[client]\nuser = root\npassword = " + password+"' > /etc/mysql/mylogin.cnf" );
			//Exec ( session, @"mysql --user=""root"" --password=""" + rootpw + @""" --execute=""UPDATE mysql.user SET password=PASSWORD('"+password+@"') WHERE User='root'; FLUSH PRIVILEGES;""" );

			Console.WriteLine ( "MySQL wurde installiert" );
		}

		private static void InstallMariaDB ( Session session, string password ) {
			Titel ( "Installiere MariaDB ..." );

			Exec ( session, "apt-get -y install debconf-utils" );
			Exec ( session, "debconf-set-selections <<< 'mariadb-server mysql-server/root_password password " + password + "'" );
			Exec ( session, "debconf-set-selections <<< 'mariadb-server mysql-server/root_password_again password " + password + "'" );
			Exec ( session, "apt-get -y install -qq mariadb-server mariadb-client" );
			Exec ( session, @"mysql --user=""root"" --password=""" + rootpw + @""" --execute=""UPDATE mysql.user SET password=PASSWORD('" + password + @"') WHERE User='root' AND Host='localhost'; FLUSH PRIVILEGES;""" );
			Console.WriteLine ( "MariaDB wurde installiert" );
		}

		private static void CreateMySQLDatabase ( Session session, string dbname, string password, string database ) {
			Titel ( "Erstelle Datenbank-Eintrag ..." );
			if ( database == "MariaDB" )
				Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""CREATE DATABASE IF NOT EXISTS " + dbname+@";""" );
			else if ( database == "MySQL" ) {
				Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute="" CREATE DATABASE IF NOT EXISTS " + dbname + @";""" );
			}
		}

		private static void CreateMySQLUser ( Session session, string username, string userpassword, string databasename, string password, bool grantall, bool onlylocal, string database ) {
			Titel ( "Erstelle Datenbank-Benutzer ..." );
			string localstr = onlylocal ? "localhost" : "%";
			if ( databasename == "" ) {
				if ( database == "MariaDB" ) {
					Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""CREATE USER IF NOT EXISTS '" + username + "'@'" + localstr + @"'; FLUSH PRIVILEGES;""" );
					Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""SET PASSWORD FOR '" + username + "'@'" + localstr + "' = '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
				} else if ( database == "MySQL" ) {
					Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute =""CREATE USER IF NOT EXISTS '" + username + "'@'" + localstr + @"'; FLUSH PRIVILEGES;""" );
					Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute=""SET PASSWORD FOR '" + username + "'@'" + localstr + "' = '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
				}
				Console.WriteLine ( "Benutzer " + username + " für MySQL Datenbank erstellt" );
			} else {
				//Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""CREATE USER '" + username + "'@'"+onlylocal+"' IDENTIFIED BY '" + userpassword + @"';""" );
				if ( grantall ) {
					if ( database == "MariaDB" ) {
						Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""GRANT ALL PRIVILEGES ON *.* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"' WITH GRANT OPTION; FLUSH PRIVILEGES;""" );
						Console.WriteLine ( "Benutzer " + username + " für MySQL erstellt und ihm volle Rechte gegeben" );
					} else {
						Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute=""GRANT ALL PRIVILEGES ON *.* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"' WITH GRANT OPTION; FLUSH PRIVILEGES;""" );
					}
				} else {
					if ( database == "MariaDB" ) {
						Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""GRANT ALL PRIVILEGES ON " + databasename + @".* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
						Console.WriteLine ( "Benutzer " + username + " für MySQL erstellt und ihm volle Rechte für die Datenbank " + databasename + " gegeben" );
					} else {
						Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute=""GRANT ALL PRIVILEGES ON " + databasename + @".* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
						Console.WriteLine ( "Benutzer " + username + " für MySQL erstellt und ihm volle Rechte für die Datenbank " + databasename + " gegeben" );
					}
				}
				
			}
		}

		private static void InstallApache ( Session session ) {
			Titel ( "Installiere Apache 2 ..." );

			Exec ( session, "apt-get -y install apache2" );
		}

		private static void InstallNginx ( Session session, string url ) {
			Titel ( "Installiere Nginx ..." );

			Exec ( session, "apt-get -y install nginx" );
			Exec ( session, "systemctl start nginx.service" );
			string amountcpu = session.ExecuteCommand ( "nproc" ).Output;
			Exec ( session, "sed -i -e 's/index index.html/index index.php index.html/' /etc/nginx/sites-available/default" );
			Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.default", "default", url, serverip, old ? "unix:/var/run/php5-fpm.sock" : "unix:/run/php/php7.0-fpm.sock" );
			PutFile ( session, "default", "/etc/nginx/sites-available/default" );
			Exec ( session, "service nginx restart" );
		}

		private static void InstallPhp ( Session session, string webapp ) {
			CommandExecutionResult cmd = session.ExecuteCommand ( "apt-cache search php7.0" );
			if ( cmd.Output == null ) {
				old = true;
				if ( webapp == "Apache" ) {
					Titel ( "Installiere PHP 5 für Apache 2 ..." );

					Exec ( session, "apt-get -y install php5 libapache2-mod-php5 php5-mysqlnd" );
					Exec ( session, "apt-get -y install php5-apcu" );
					Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php5/apache2/php.ini" );

					Exec ( session, "systemctl restart apache2" );
				} else {
					Titel ( "Installiere PHP 5 für Nginx ..." );

					Exec ( session, "apt-get -y install php5 php5-fpm php5-mysql php5-cli php5-curl php5-gd php5-mcrypt" );

					Exec ( session, @"sed -i ""s/;cgi.fix_pathinfo=1/cgi.fix_pathinfo=0/"" /etc/php5/fpm/php.ini" );
					Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php5/fpm/php.ini" );
					Exec ( session, "php5enmod mcrypt" );
					Exec ( session, @"sed -i -e 's/try_files $uri $uri\/ =404;/try_files $uri $uri\/ \/index.php;/' /etc/nginx/sites-available/default" );

					Exec ( session, "apt-get -y install php5-apcu" );

					Exec ( session, "service php5-fpm restart" );
				}
			} else {
				if ( webapp == "Apache" ) {
					Titel ( "Installiere PHP 7.0 für Apache 2 ..." );

					Exec ( session, "apt-get -y install php7.0 libapache2-mod-php7.0 php7.0-mysql" );
					Exec ( session, "apt-get -y install php7.0-opcache php-apcu" );
					Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php/7.0/apache2/php.ini" );

					Exec ( session, "systemctl restart apache2" );
					
				} else {
					Titel ( "Installiere PHP 7.0 für Nginx ..." );

					Exec ( session, "apt-get -y install php7.0 php7.0-fpm php7.0-mysql php7.0-mbstring php7.0-common php7.0-gd php7.0-mcrypt php-gettext php7.0-curl php7.0-cli php7.0-xml" );

					Exec ( session, @"sed -i ""s/;cgi.fix_pathinfo=1/cgi.fix_pathinfo=0/"" /etc/php/7.0/fpm/php.ini" );
					Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php/7.0/fpm/php.ini" );
					Exec ( session, "apt-get -y install php-apcu" );
					Exec ( session, @"sed -i -e 's/try_files $uri $uri\/ =404;/try_files $uri $uri\/ \/index.php;/' /etc/nginx/sites-available/default" );

					Exec ( session, "service php7.0-fpm reload" );
				}
			}
			Console.WriteLine ( "Erfolgreich installiert" );
		}

		private static void InstallPhpMyAdmin ( Session session, string mysqlpassword, string webapp, bool hideperformance, bool hidemysql, bool hidephpmyadmin, bool hideinformation, bool hidesys, string url ) {
			Titel ( "Installiere phpMyAdmin..." );

			Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/dbconfig-install boolean true'" );
			Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/app-password-confirm password " + mysqlpassword+@"'" );
			Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/mysql/admin-pass password " + mysqlpassword+@"'" );
			Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/mysql/app-pass password " + mysqlpassword+@"'" );
			Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/reconfigure-webserver multiselect "+ ( webapp == "Apache" ? "apache2" : "none" )+ "'" );
			Exec ( session, "apt-get install -y phpmyadmin" );

			if ( webapp == "Apache" ) {
				Console.WriteLine ( "Binde phpMyAdmin an Apache 2" );
				Exec ( session, "sed -i '$ a Include /etc/phpmyadmin/apache.conf' /etc/apache2/apache2.conf" );
				Exec ( session, "service apache2 restart" );
			} else if ( webapp == "Nginx" ) { 
				Console.WriteLine ( "Binde phpMyAdmin an Nginx" );
				Exec ( session, "ln -s /usr/share/phpmyadmin /var/www/html" );
				Exec ( session, "cd /var/www/html" );
				Exec ( session, "mv phpmyadmin " + url );
				Exec ( session, "cd" );
			}

			int amount = 0;
			if ( hideperformance ) amount++;
			if ( hidemysql ) amount++;
			if ( hidephpmyadmin ) amount++;
			if ( hideinformation ) amount++;
			if ( hidesys ) amount++;

			string hidingdb = "";
			if ( amount > 0 ) {
				Titel ( "Verstecke Datenbanken in phpMyAdmin" );

				if ( amount == 1 ) {
					if ( hideperformance )
						hidingdb = "performance_schema";
					else if ( hidemysql )
						hidingdb = "mysql";
					else if ( hidephpmyadmin )
						hidingdb = "phpmyadmin";
					else if ( hideinformation )
						hidingdb = "information_schema";
					else if ( hidesys )
						hidingdb = "sys";
					
				} else {
					hidingdb = "^(";
					int gotamount = 0;
					if ( hideperformance ) {
						hidingdb += "performance_schema";
						gotamount++;
					}
					if ( hidemysql ) {
						if ( gotamount >= 1 ) {
							hidingdb += "|";
						}
						hidingdb += "mysql";
					}
					if ( hidephpmyadmin ) {
						if ( gotamount >= 1 ) {
							hidingdb += "|";
						}
						hidingdb += "phpmyadmin";
					}
					if ( hideinformation ) {
						if ( gotamount >= 1 ) {
							hidingdb += "|";
						}
						hidingdb += "information_schema";
					}
					if ( hidesys ) {
						if ( gotamount >= 1 ) {
							hidingdb += "|";
						}
						hidingdb += "sys";
					}
					hidingdb += ")$";
				}
				Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.config.inc.php", "config.inc.php", hidingdb );
				PutFile ( session, "config.inc.php", "/etc/phpmyadmin/config.inc.php" );

				Console.WriteLine ( "Datenbanken sind nun in phpMyAdmin versteckt" );
			}

			if ( webapp == "Apache" ) {
				if ( url != "phpmyadmin" ) {
					Exec ( session, @"sed -e 's/^Alias \/phpmyadmin/Alias \/" + url + @"/' -i /etc/phpmyadmin/apache.conf" );
				}
			}
		}

		public static void InstallMTA ( string user, string userpassword, bool autostart, string servername, string email, string serverPort, string maxPlayers, string httpPort, bool skinmodding, string serverpassword, bool bulletsync, string fpslimit ) {
			try {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				using ( Session session = new Session () ) {
					session.DisableVersionCheck = true;
					session.Open ( options );
					AddUser ( session, user, userpassword );
					InstallMTAFiles ( session, user );
					InstallMTAShell ( session, user, autostart );
					SetupMTA ( session, user, servername, email, serverPort, maxPlayers, httpPort, skinmodding, serverpassword, bulletsync, fpslimit );
					Exec ( session, "chown -cR mta /home/mta" );
					session.Close ();
					new Forms.MTAWindowEnd ( Forms.MTAWindow.instance.Location );
					Forms.MTAWindow.instance.Close ();
				}
			} catch ( Exception e ) {
				Console.Write ( e.ToString () );
				Forms.MTAWindow.notification ( e.ToString (), "error" );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}


		public static void InstallDatabase ( string database, string webapp, string url, bool hideperformance, bool hidemysql, bool hidephpmyadmin, bool hideinformation, bool hidesys, string mysqlpassword, bool createmysqluser, string mysqlusername, string mysqluserpw, string databasename, bool grantalltouser, bool useronlylocal ) {
			try {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				using ( Session session = new Session () ) {
					session.DisableVersionCheck = true;
					session.Open ( options );
					if ( database == "MySQL" )
						InstallMySQL ( session, mysqlpassword );
					else if ( database == "MariaDB" )
						InstallMariaDB ( session, mysqlpassword );
					if ( databasename != "" )
						CreateMySQLDatabase ( session, databasename, mysqlpassword, database );
					if ( createmysqluser )
						CreateMySQLUser ( session, mysqlusername, mysqluserpw, databasename, mysqlpassword, grantalltouser, useronlylocal, database );
					if ( webapp == "Apache" )
						InstallApache ( session );
					else if ( webapp == "Nginx" )
						InstallNginx ( session, url );
					InstallPhp ( session, webapp );
					InstallPhpMyAdmin ( session, mysqlpassword, webapp, hideperformance, hidemysql, hidephpmyadmin, hideinformation, hidesys, url );
					session.Close ();
					Console.WriteLine ( "Datenbank wurde erfolgreich installiert! Sie ist zu erreichen unter: "+ serverip +"/"+url );
					new Forms.DatabaseWindowEnd ( Forms.DatabaseWindow.instance.Location );
					Forms.DatabaseWindow.instance.Close ();
				}
			} catch ( Exception e ) {
				Console.Write ( e.ToString () );
				Forms.DatabaseWindow.notification ( e.ToString (), "error" );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}

		public static void InstallFirewall ( ) {
			try {
				using ( Session session = new Session () ) {
					session.DisableVersionCheck = true;
					session.Open ( options );
					Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.firewall.sh", "firewall.sh", mtaserverport.ToString (), mtahttpport.ToString (), ( mtaserverport + 123 ).ToString () );
					PutFile ( session, "firewall.sh", "/etc/firewall.sh" );
					Exec ( session, "chmod u+x /etc/firewall.sh" );
					SaveInRCLocal ( session, @"\/etc\/firewall.sh" );
					Exec ( session, "/etc/firewall.sh" );
					session.Close ();
					Forms.MainWindow.instance.Show ();
				}
			} catch ( Exception e ) {
				Console.Write ( e.ToString () );
			} finally {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
		}
	}

}
