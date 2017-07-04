// used for connection to the server //
using WinSCP;
// used for Console and Exception //
using System;

/**
 * Installer for the database
 * */

namespace LinuxMTAInstaller.Installs {
	static class Database {

		/**
		 * static method to create the Database
		 * method is getting called by the button in MainWindow
		 * 
		 * @param string database MySQL or MariaDB
		 * @param string webapp Apache or Nginx
		 * @param string url The string we attach to the IP to get to phpmyadmin (123.456.789/URL)
		 * @param bool hideperformance Hide the performance database in phpmyadmin
		 * @param bool hidemysql Hide the mysql database in phpmyadmin
		 * @param bool hidephpmyadmin Hide the phpmyadmin database in phpmyadmin (created for default user phpmyadmin)
		 * @param bool hideinformation Hide the information database in phpmyadmin
		 * @param bool hidesys Hide the sys database in phpmyadmin
		 * @param string mysqlpassword MySQL password for the root user 
		 * @param bool createmysqluser Create a new mysql-user
		 * @param string mysqlusername Name for the new mysql-user (createmysqluser == true)
		 * @param string mysqluserpw Password for the new mysql-user (createmysqluser == true)
		 * @param string databasename Name for the database we want to create - the new created mysql-user will get full permission here (databasename != "")
		 * @param bool grantalltouser Gives full permissions for everything to the new created mysq-user (createmysqluser == true)
		 * @param bool useronlylocal User created in localhost (instead of %) (createmysqluser == true)
		 * */
		public static void InstallDatabase ( string database, string webapp, string url, bool hideperformance, bool hidemysql, bool hidephpmyadmin, bool hideinformation, bool hidesys, string mysqlpassword, bool createmysqluser, string mysqlusername, string mysqluserpw, string databasename, bool grantalltouser, bool useronlylocal ) {
			try {
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				using ( Session session = new Session () ) {
					// needed to be able to use the self-created WinSCP.exe //
					session.DisableVersionCheck = true;
					session.Open ( Connection.Connection.options );
					RemovePreviousInstallation ( session );
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
					if ( database == "MySQL" ) {
						// delete the file we used to send commands with root //
						Connection.Connection.Exec ( session, "rm -f /etc/mysql/mylogin.cnf" );
					}
					session.Close ();

					Console.WriteLine ( Languages.GetLang ( "database_installed_successfully" ), Connection.Connection.serverip, url );
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

		/** 
		 * static method to remove all previous installations
		 * That way you can install database as many times as you want without getting any problems.
		 * 
		 * @param Session session The opened session
		 * */
		private static void RemovePreviousInstallation ( Session session ) {
			try {
				Useful.Useful.Titel ( Languages.GetLang ( "remove_previous_installations" ) );
				// Sometimes getting a dpkg error, so fix it //
				Connection.Connection.Exec ( session, "dpkg --configure -a" );
				// Sometimes getting errors, so fix it //
				Connection.Connection.Exec ( session, "apt-get -qq autoremove" );
				Connection.Connection.Exec ( session, "apt-get purge -qq apache2*" );
				Connection.Connection.Exec ( session, "apt-get purge -qq mysql-*" );
				Connection.Connection.Exec ( session, "apt-get purge -qq libapache2*" );
				Connection.Connection.Exec ( session, "apt-get purge -qq phpmyadmin" );
				Connection.Connection.Exec ( session, "apt-get purge -qq mariadb-*" );
				Connection.Connection.Exec ( session, "apt-get purge -qq nginx*" );
				Connection.Connection.Exec ( session, "rm -rf /etc/apache2" );
				Connection.Connection.Exec ( session, "rm -rf /etc/phpmyadmin" );
				Connection.Connection.Exec ( session, "rm -rf /var/lib/mysql" );
				Connection.Connection.Exec ( session, "rm -rf /var/lib/nginx" );
				Connection.Connection.Exec ( session, "rm -rf /etc/mysql" );
				Connection.Connection.Exec ( session, "rm -rf /etc/phpmyadmin" );
				Connection.Connection.Exec ( session, "rm -rf /etc/nginx" );
				Connection.Connection.Exec ( session, "apt-get -qq autoremove" );
				Connection.Connection.Exec ( session, "apt-get -qq autoclean" );
			} catch ( Exception e ) {
				Console.WriteLine ( e.ToString() );
			}
		}

		/** 
		 * static method to install MySQL (webapp == MySQL)
		 * 
		 * @param Session session The opened session
		 * @param string password The root-password for MySQL
		 * */
		private static void InstallMySQL ( Session session, string password ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "MySQL ..." );

			// Needed to install mysql without getting a prompt //
			Connection.Connection.Exec ( session, "apt-get -qq install debconf-utils" );
			Connection.Connection.Exec ( session, "debconf-set-selections <<< 'mysql-server mysql-server/root_password password " + password + "'" );
			Connection.Connection.Exec ( session, "debconf-set-selections <<< 'mysql-server mysql-server/root_password_again password " + password + "'" );
			Connection.Connection.Exec ( session, "apt-get -y install -qq mysql-server mysql-client" );
			// Needed to send commands with root (store the password and use the file) //
			Connection.Connection.Exec ( session, "printf $'[client]\nuser = root\npassword = " + password + "' > /etc/mysql/mylogin.cnf" );

			Console.WriteLine ( "MySQL" + Languages.GetLang ( "was_installed" ) );
		}

		/** 
		 * static method to install MariaDB (webapp == MariaDB)
		 * 
		 * @param Session session The opened session
		 * @param string password The root-password for MariaDB
		 * */
		private static void InstallMariaDB ( Session session, string password ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "MariaDB ..." );

			// needed to install mariadb without getting a prompt //
			Connection.Connection.Exec ( session, "apt-get -qq install debconf-utils" );
			Connection.Connection.Exec ( session, "debconf-set-selections <<< 'mariadb-server mysql-server/root_password password " + password + "'" );
			Connection.Connection.Exec ( session, "debconf-set-selections <<< 'mariadb-server mysql-server/root_password_again password " + password + "'" );
			Connection.Connection.Exec ( session, "apt-get -y install -qq mariadb-server mariadb-client" );
			// seems like mariadb doesn't change the root-password, so we have to do it by ourselves //
			Connection.Connection.Exec ( session, @"mysql --user=""root"" --password=""" + Connection.Connection.rootpw + @""" --execute=""UPDATE mysql.user SET password=PASSWORD('" + password + @"') WHERE User='root' AND Host='localhost'; FLUSH PRIVILEGES;""" );
			Console.WriteLine ( "MariaDB" + Languages.GetLang ( "was_installed" ) );
		}

		/** 
		 * static method to install the MySQL Database (databasename != "")
		 * 
		 * @param Session session The opened session
		 * @param string dbname The name of the database
		 * @param string password The password of root
		 * @param string database The used database (MariaDB or MySQL)
		 * */
		private static void CreateMySQLDatabase ( Session session, string dbname, string password, string database ) {
			Useful.Useful.Titel ( Languages.GetLang ( "creating_database" ) );
			if ( database == "MariaDB" )
				Connection.Connection.Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""CREATE DATABASE IF NOT EXISTS " + dbname + @";""" );
			else if ( database == "MySQL" ) {
				// In MySQL we have to get the password from a file, else we will get an error-code //
				Connection.Connection.Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute="" CREATE DATABASE IF NOT EXISTS " + dbname + @";""" );
			}
		}

		/** 
		 * static method to create the MySQL user (createmysqluser == true)
		 * 
		 * @param Session session The opened session 
		 * @param string username The username of the user we want to create
		 * @param string userpassword The password of the user we want to create
		 * @param string password The password of mysql-root
		 * @param bool grantall Grant all permissions to the new user
		 * @param bool onlylocal Only in localhost
		 * @param string database Installed database (MariaDB or MySQL)
		 * */
		private static void CreateMySQLUser ( Session session, string username, string userpassword, string databasename, string password, bool grantall, bool onlylocal, string database ) {
			Useful.Useful.Titel ( Languages.GetLang ( "creating_database_user" ) );
			string localstr = onlylocal ? "localhost" : "%";
			if ( databasename == "" ) {
				if ( database == "MariaDB" ) {
					Connection.Connection.Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""CREATE USER IF NOT EXISTS '" + username + "'@'" + localstr + @"'; FLUSH PRIVILEGES;""" );
					Connection.Connection.Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""SET PASSWORD FOR '" + username + "'@'" + localstr + "' = '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
				} else if ( database == "MySQL" ) {
					// In MySQL we have to get the password from a file, else we will get an error-code //
					Connection.Connection.Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute =""CREATE USER IF NOT EXISTS '" + username + "'@'" + localstr + @"'; FLUSH PRIVILEGES;""" );
					Connection.Connection.Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute=""SET PASSWORD FOR '" + username + "'@'" + localstr + "' = '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
				}
				Console.WriteLine ( Languages.GetLang ( "database_user_created" ), username );
			} else {
				// Don't need to create user because GRANT ALL does it if the user doesn't exist //
				//Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""CREATE USER '" + username + "'@'"+onlylocal+"' IDENTIFIED BY '" + userpassword + @"';""" );
				if ( grantall ) {
					if ( database == "MariaDB" ) {
						Connection.Connection.Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""GRANT ALL PRIVILEGES ON *.* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"' WITH GRANT OPTION; FLUSH PRIVILEGES;""" );
					} else if ( database == "MySQL" ) {
						// In MySQL we have to get the password from a file, else we will get an error-code //
						Connection.Connection.Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute=""GRANT ALL PRIVILEGES ON *.* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"' WITH GRANT OPTION; FLUSH PRIVILEGES;""" );
					}
					Console.WriteLine ( Languages.GetLang ( "database_user_created_full_rights" ), username );
				} else {
					if ( database == "MariaDB" ) {
						Connection.Connection.Exec ( session, @"mysql --user=""root"" --password=""" + password + @""" --execute=""GRANT ALL PRIVILEGES ON " + databasename + @".* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
					} else if ( database == "MySQL" ) {
						// In MySQL we have to get the password from a file, else we will get an error-code //
						Connection.Connection.Exec ( session, @"mysql --defaults-file=/etc/mysql/mylogin.cnf -u root --execute=""GRANT ALL PRIVILEGES ON " + databasename + @".* TO '" + username + @"'@'" + localstr + "' IDENTIFIED BY '" + userpassword + @"'; FLUSH PRIVILEGES;""" );
					}
					Console.WriteLine ( Languages.GetLang ( "database_user_created_db_rights" ), username, databasename );
				}

			}
		}

		/**
		 * static method to install Apache2
		 * 
		 * @param Session session The opened session
		 * */
		private static void InstallApache ( Session session ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "Apache 2 ..." );
			Connection.Connection.Exec ( session, "apt-get -qq install apache2" );
		}

		/**
		 * static method to install Nginx
		 * 
		 * @param Session session The opened session
		 * @param string url The url we want to use to access to phpmyadmin
		 * */
		private static void InstallNginx ( Session session, string url ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "Nginx ..." );

			Connection.Connection.Exec ( session, "apt-get -qq install nginx" );
			Connection.Connection.Exec ( session, "systemctl start nginx.service" );
			// string amountcpu = session.ExecuteCommand ( "nproc" ).Output;
			// adding index.php //
			Connection.Connection.Exec ( session, "sed -i -e 's/index index.html/index index.php index.html/' /etc/nginx/sites-available/default" );
			Files.Files.WriteResourceToFile ( "LinuxMTAInstaller.Files.default", "default", url, Connection.Connection.serverip, Connection.Connection.old ? "unix:/var/run/php5-fpm.sock" : "unix:/run/php/php7.0-fpm.sock" );
			Connection.Connection.PutFile ( session, "default", "/etc/nginx/sites-available/default" );
			Connection.Connection.Exec ( session, "service nginx restart" );
		}

		/**
		 * static method to install php
		 * 
		 * @param Session session The opened session
		 * @param string webapp The installed webapp (Apache2 or Nginx)
		 * */
		private static void InstallPhp ( Session session, string webapp ) {
			
			if ( Connection.Connection.old ) {
				// PHP 7.0 path //
				if ( webapp == "Apache" ) {
					Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "PHP 5" + Languages.GetLang ( "for" ) + "Apache 2 ..." );

					Connection.Connection.Exec ( session, "apt-get -qq install php5 libapache2-mod-php5 php5-mysqlnd" );
					Connection.Connection.Exec ( session, "apt-get -qq install php5-apcu" );
					// why not being able to import big files? //
					Connection.Connection.Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php5/apache2/php.ini" );

					Connection.Connection.Exec ( session, "systemctl restart apache2" );
				} else {
					Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "PHP 5" + Languages.GetLang ( "for" ) + "Nginx ..." );

					Connection.Connection.Exec ( session, "apt-get -qq install php5 php5-fpm php5-mysql php5-cli php5-curl php5-gd php5-mcrypt" );

					Connection.Connection.Exec ( session, @"sed -i ""s/;cgi.fix_pathinfo=1/cgi.fix_pathinfo=0/"" /etc/php5/fpm/php.ini" );
					// why not being able to import big files? //
					Connection.Connection.Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php5/fpm/php.ini" );
					Connection.Connection.Exec ( session, "php5enmod mcrypt" );
					Connection.Connection.Exec ( session, @"sed -i -e 's/try_files $uri $uri\/ =404;/try_files $uri $uri\/ \/index.php;/' /etc/nginx/sites-available/default" );

					Connection.Connection.Exec ( session, "apt-get -qq install php5-apcu" );

					Connection.Connection.Exec ( session, "service php5-fpm restart" );
				}
			} else {
				// PHP 5 path //
				if ( webapp == "Apache" ) {
					Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "PHP 7.0" + Languages.GetLang ( "for" ) + "Apache 2 ..." );

					Connection.Connection.Exec ( session, "apt-get -qq install php7.0 libapache2-mod-php7.0 php7.0-mysql" );
					Connection.Connection.Exec ( session, "apt-get -qq install php7.0-opcache php-apcu" );
					// why not being able to import big files? //
					Connection.Connection.Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php/7.0/apache2/php.ini" );

					Connection.Connection.Exec ( session, "systemctl restart apache2" );

				} else {
					Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "PHP 7.0" + Languages.GetLang ( "for" ) + "Nginx ..." );

					Connection.Connection.Exec ( session, "apt-get -qq install php7.0 php7.0-fpm php7.0-mysql php7.0-mbstring php7.0-common php7.0-gd php7.0-mcrypt php-gettext php7.0-curl php7.0-cli php7.0-xml" );

					Connection.Connection.Exec ( session, @"sed -i ""s/;cgi.fix_pathinfo=1/cgi.fix_pathinfo=0/"" /etc/php/7.0/fpm/php.ini" );
					// why not being able to import big files? //
					Connection.Connection.Exec ( session, @"sed -i '/upload_max_filesize = 2M/c upload_max_filesize = 500M' /etc/php/7.0/fpm/php.ini" );
					Connection.Connection.Exec ( session, "apt-get -qq install php-apcu" );
					Connection.Connection.Exec ( session, @"sed -i -e 's/try_files $uri $uri\/ =404;/try_files $uri $uri\/ \/index.php;/' /etc/nginx/sites-available/default" );

					Connection.Connection.Exec ( session, "service php7.0-fpm reload" );
				}
			}
		}

		/**
		 * static method to install phpmyadmin
		 * 
		 * @param Session session
		 * @param string mysqlpassword The password of mysql-root
		 * @param string webapp The installed web-application (Apache2 or Nginx)
		 * @param bool hideperformance Hide the performance database in phpmyadmin
		 * @param bool hidemysql Hide the mysql database in phpmyadmin
		 * @param bool hidephpmyadmin Hide the phpmyadmin database in phpmyadmin (created for default user phpmyadmin)
		 * @param bool hideinformation Hide the information database in phpmyadmin
		 * @param bool hidesys Hide the sys database in phpmyadmin
		 * @param string url The string we attach to the IP to get to phpmyadmin (123.456.789/URL)
		 * */
		private static void InstallPhpMyAdmin ( Session session, string mysqlpassword, string webapp, bool hideperformance, bool hidemysql, bool hidephpmyadmin, bool hideinformation, bool hidesys, string url ) {
			Useful.Useful.Titel ( Languages.GetLang ( "installing" ) + "phpMyAdmin..." );

			// installing phpmyadmin without prompt //
			Connection.Connection.Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/dbconfig-install boolean true'" );
			Connection.Connection.Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/app-password-confirm password " + mysqlpassword + @"'" );
			Connection.Connection.Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/mysql/admin-pass password " + mysqlpassword + @"'" );
			Connection.Connection.Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/mysql/app-pass password " + mysqlpassword + @"'" );
			Connection.Connection.Exec ( session, @"debconf-set-selections <<< 'phpmyadmin phpmyadmin/reconfigure-webserver multiselect " + ( webapp == "Apache" ? "apache2" : "none" ) + "'" );
			Connection.Connection.Exec ( session, "apt-get install -y phpmyadmin" );

			if ( webapp == "Apache" ) {
				Console.WriteLine ( Languages.GetLang ( "bind" ) + "phpMyAdmin" + Languages.GetLang ( "on" ) + "Apache 2" );
				Connection.Connection.Exec ( session, "sed -i '$ a Include /etc/phpmyadmin/apache.conf' /etc/apache2/apache2.conf" );
				Connection.Connection.Exec ( session, "service apache2 restart" );
			} else if ( webapp == "Nginx" ) {
				Console.WriteLine ( Languages.GetLang ( "bind" ) + "phpMyAdmin" + Languages.GetLang ( "on" ) + "Nginx" );
				Connection.Connection.Exec ( session, "ln -s /usr/share/phpmyadmin /var/www/html" );
				Connection.Connection.Exec ( session, "cd /var/www/html" );
				Connection.Connection.Exec ( session, "mv phpmyadmin " + url );
				Connection.Connection.Exec ( session, "cd" );
			}

			
			int amount = 0;
			if ( hideperformance )
				amount++;
			if ( hidemysql )
				amount++;
			if ( hidephpmyadmin )
				amount++;
			if ( hideinformation )
				amount++;
			if ( hidesys )
				amount++;

			string hidingdb = "";
			if ( amount > 0 ) {
				Useful.Useful.Titel ( Languages.GetLang ( "hiding_dbs_in_phpmyadmin" ) );

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
				Connection.Connection.PutFile ( session, "config.inc.php", "/etc/phpmyadmin/config.inc.php" );
			}

			if ( webapp == "Apache" ) {
				if ( url != "phpmyadmin" ) {
					Connection.Connection.Exec ( session, @"sed -e 's/^Alias \/phpmyadmin/Alias \/" + url + @"/' -i /etc/phpmyadmin/apache.conf" );
				}
			}
		}
	}
}