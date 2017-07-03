static partial class Languages {
	private static System.Collections.Generic.Dictionary<string, string> english = new System.Collections.Generic.Dictionary<string, string> {
		{ "updating_the_server", "Updating the server ..." },
		{ "update_successful",  "Update has been successful." },
		{ "no_connection_to_server",  "No connection to the server!" },
		{ "donwload_of_succeeded", "Download of {0} succeeded." },
		{ "upload_of_succeeded", "Upload of {0} succeeded." },
		{ "creating_user", "Creating user ..." },
		{ "user_created", "User has been created." },
		{ "setting_up_auto-starter", "Setting up auto-start ..." },
		{ "auto-starter_installed", "Auto-start has been installed!" },
		{ "installing", "Installing " },
		{ "creating", "Creating " },
		{ "changing_server_name", "Changing server-name ..." },
		{ "changing_server_email", "Changing server-e-mail-address ..." },
		{ "changing_server_port", "Changing server-port ..." },
		{ "changing_server_http_port", "Changing server-http-port ..." },
		{ "changing_server_max_players", "Changing max. players ..." },
		{ "allowing_server_skin_modding", "Allowing skin-modding ..." },
		{ "setting_server_password", "Setting server-password ..." },
		{ "deactivating_bullet_sync", "Deactivating bullet-sync ..." },
		{ "changing_server_fps_limit",  "Changing FPS-limit ..." },
		{ "mta_got_configurated", "MTA got configurated successfully!" },
		{ "creating_sh_installer", "Creating Shell-installer ..." },
		{ "installing_needed_packages", "Installing needed packages ..." },
		{ "removing_old_MTA_files", "Removing old MTA-installations (if available) ..." },
		{ "downloading_MTA_files", "Downloading MTA files ..." },
		{ "was_installed", " was installed successfully!" },
		{ "creating_database", "Creating database ..." },
		{ "creating_database_user", "Creating database user ..." },
		{ "database_user_created", "User {0} for MySQL database has been created." },
		{ "database_user_created_full_rights", "User {0} for MySQL database has been created and given full rights!" },
		{ "database_user_created_db_rights", "User {0} for MySQL database has been created and given full rights for database {1}!" },
		{ "for", " for " },
		{ "bind", "Bind " },
		{ "on", " on " },
		{ "hiding_dbs_in_phpmyadmin", "Hiding databases in phpMyAdmin ..." },
		{ "database_installed_successfully", "Database has been installed successfully! It's available to use at: {0}/{1}" },
		{ "installing_firewall_in", "Installing firewall.sh in /etc/ folder ..." },
		{ "setting_up_teamspeak3", "Setting up Teamspeak3" },
		{ "installing_teamspeak_autostarter", "Installing Teamspeak3 auto-starter ..." },
		{ "remove_previous_installations", "Remove previous installations (if exists) ..." },
		{ "configuring_teamspeak", "Configuring Teamspeak3 ..." },
		{ "loading", "Loading ..." },
		{ "getting_serveradmin_password_and_token", "Getting serveradmin-password and token ..." },

		// FORMS //
		{ "StartWindow_info", @"Welcome to the installer for linux.

This programm got created by [TDS]Bonus to make the installation of MTA, database, firewall and Teamspeak for linux as easy as possible.
It got only tested with Ubuntu 16.04. so far, but it should work for other Linux-versions, too.

The programm doesn't save any data, it's safe, don't worry.
Because it's open source, you can find the code in GitHub:
https://github.com/emre1702/LinuxMTAInstaller

Donates are welcome:
www.paypal.me/1702" },
		{ "language", "Language:" },
		{ "mysql_database_system", "MySQL-database-system:" },
		{ "mysql_password", "MySQL-password:" },
		{ "install", "Install" },
		{ "website_application", "Website-application:" },
		{ "phpmyadmin_url",  "PhpMyAdmin-URL - e.g. 151.31.41.54/phpmyadmin" },
		{ "hide_dbs", "Hide databases:" },
		{ "create_new_user_for_db", "Create new user for database" },
		{ "username", "Username:" },
		{ "password", "Password:" },
		{ "database_name",  "Database-name:" },
		{ "full_permissions", "Full permissions" },
		{ "back", "Back" },
		{ "permissions_only_local", "Permissions only local" },
		{ "database_installed_form", @"Congrats, your server got a database.
You can use it to save, load and manage data.

If you don't know how to use the database (MySQL) you should try to learn it.
There are many very helpful possibilities you should learn about.

If an URL still works delete the folder with the name in /var/www/html!" },
		{ "firewall_installed_form", @"The firewall has been installed.

To do that a file with the name ""firewall.sh"" got created in /etc/ folder on your server.
This file - a shell script - configurates IPTables and closes ports you don't use. 

When you want to open specific ports just open /etc/firewall.sh and put them in (like did with the other ports).
Some settings are already in but are deactivated - you just need to remove the ""#"" character at the IPTABLES lines to activate them." },
		{ "login_with_root", "Login to your server WITH root!" },
		{ "login", "Login" },
		{ "mainwindow_info", @"Here you can install specific components.
Installing a already installed component removes the old version.

Firewall creates some firewall-rules and closes not-used ports.
Use it at the end if you want to use another ports for MTA instead of the default ports." },
		{ "database", "Database" },
		{ "running_user", "Running user:" },
		{ "server_name", "Server-name:" },
		{ "max_players", "Max. players:" },
		{ "server_password", "Server-password:" },
		{ "fps_limit", "FPS-limit:" },
		{ "port", "port" },
		{ "skin_modding", "Skin-modding:" },
		{ "bullet_sync", "Bullet-sync:" },
		{ "ssh_port", "SSH-port:" },
		{ "user_password", "User-password:" },
		{ "auto_start", "Auto-start:" },
		{ "install_will_delete_old", "CARE: \r\nIf you already installed MTA the old folder will be removed!" },
		{ "email_address", "E-mail-address:" },
		{ "mtawindow_end", @"Congrats, MTA got installed.

The folder is in /home/REPLACEIT1/MTA.
You can start and stop the server by using the commands:
/etc/mta.sh start
/etc/mta.sh stop

If you want to have the MTA console you only have to:
1. login with the mta user we just created
2. use ""screen -R mtaserver"" in console

By closing screen (e.g. with CTRL+C) you will also close MTA!

Have fun!" },
		{ "serveradmin_password", "serveradmin-password:" },
		{ "serveradmin_password_info", "If \"serveradmin-password:\" stays blank the password will stay generated by the server!" },
		{ "teamspeak_installed_info", @"Congrats!
Your teamspeak-server has been installed and can get started now.

The token for serveradmin is:
REPLACEIT1

The password for serveradmin is:
REPLACEIT2

You can start the server with:
/etc/ts.sh start

and stop it with:
/etc/ts.sh stop" },
	};
}
