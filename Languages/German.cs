static partial class Languages {
	private static System.Collections.Generic.Dictionary<string, string> german = new System.Collections.Generic.Dictionary<string, string> {
		{ "updating_the_server", "Update den Server ..." },
		{ "update_successful",  "Update war erfolgreich." },
		{ "no_connection_to_server",  "Keine Verbindung zum Server!" },
		{ "donwload_of_succeeded", "Download von {0} war erfolgreich." },
		{ "upload_of_succeeded", "Upload von {0} war erfolgreich." },
		{ "creating_user", "Erstelle Benutzer ..." },
		{ "user_created", "Benutzer wurde erstellt." },
		{ "setting_up_auto-starter", "Richte Auto-Start ein ..." },
		{ "auto-starter_installed", "Auto-Start wurde installiert." },
		{ "installing", "Installiere " },
		{ "creating", "Erstelle " },
		{ "changing_server_name", "Ändere Server-Namen ..." },
		{ "changing_server_email", "Ändere Server-E-Mail-Adresse ..." },
		{ "changing_server_port", "Ändere Server-Port ..." },
		{ "changing_server_http_port", "Ändere Serve-HTTP-Port ..." },
		{ "changing_server_max_players", "Ändere max. Spieler ..." },
		{ "allowing_server_skin_modding", "Erlaube Skin-Modding ..." },
		{ "setting_server_password", "Stelle Server-Passwort ein ..." },
		{ "deactivating_bullet_sync", "Deaktiviere Bullet-Sync ..." },
		{ "changing_server_fps_limit",  "Ändere FPS-Limit ..." },
		{ "mta_got_configurated", "MTA wurde erfolgreich eingestellt!" },
		{ "creating_sh_installer", "Erstelle Shell-Installer ..." },
		{ "installing_needed_packages", "Installiere die benötigten Pakete ..." },
		{ "removing_old_MTA_installation", "Entferne alte MTA-Installation (falls vorhanden) ..." },
		{ "downloading_MTA_files", "Eade die MTA-Daten runter ..." },
		{ "was_installed", " wurde erfolgreich installiert" },
		{ "creating_database", "Erstelle Datenbank-Eintrag ..." },
		{ "creating_database_user", "Erstelle Datenbank-Benutzer ..." },
		{ "database_user_created", "Benutzer {0} für MySQL Datenbank erstellt." },
		{ "database_user_created_full_rights", "Benutzer {0} für MySQL Datenbank erstellt und ihm volle Rechte gegeben!" },
		{ "database_user_created_db_rights", "Benutzer {0} für MySQL Datenbank erstellt und ihm volle Rechte für die Datenbank {1} gegeben!" },
		{ "for", " für " },
		{ "bind", "Binde " },
		{ "on", " an " },
		{ "hiding_dbs_in_phpmyadmin", "Verstecke Datenbanken in phpMyAdmin ..." },
		{ "database_installed_successfully", "Datenbank wurde erfolgreich installiert! Sie ist zu erreichen unter: {0}/{1}" },
		{ "installing_firewall_in", "Installiere firewall.sh im /etc/ Ordner ..." },
		{ "setting_up_teamspeak3", "Richte Teamspeak3 ein ..." },
		{ "installing_teamspeak_autostarter", "Installiere Teamspeak3 Auto-Starter ..." },
		{ "remove_previous_installations", "Entferne vorherige Installationen (falls verfügbar) ..." },
		{ "configuring_teamspeak", "Konfiguriere Teamspeak3 ..." },
		{ "loading", "Lade ..." },
		{ "getting_serveradmin_password_and_token", "Suche nach serveradmin-Passwort und Token ..." },

		// FORMS //
		{ "StartWindow_info", @"Willkommen beim Installer für Linux.

Dieses Programm wurde von [TDS]Bonus erstellt, um ganz einfach MTA, Datenbank und Firma für Linux zu installieren.
Bisher wurde nur Ubuntu 16.04. getestet.

Hier werden keine Daten o.ä. gespeichert, das Programm ist sicher.
Der ganze Code befindet sich in meinem Github:
https://github.com/emre1702/LinuxMTAInstaller

Spenden sind gerne sehen:
www.paypal.me/1702" },
		{ "language", "Sprache:" },
		{ "mysql_database_system", "MySQL-Datenbank-System:" },
		{ "mysql_password", "MySQL-Passwort:" },
		{ "install", "Installieren" },
		{ "website_application", "Websiten-Applikation:" },
		{ "phpmyadmin_url",  "PhpMyAdmin-Link - z.B. 151.31.41.54/phpmyadmin" },
		{ "hide_dbs", "Verstecke Datenbanken:" },
		{ "create_new_user_for_db", "Neuen Benutzer für Datenbank erstellen" },
		{ "username", "Benutzer-Name:" },
		{ "password", "Passwort:" },
		{ "database_name",  "Datenbank-Name:" },
		{ "full_permissions", "Volle Rechte" },
		{ "back", "Zurück" },
		{ "permissions_only_local", "Rechte nur Lokal" },
		{ "database_installed_form", @"Herzlichen Glückwunsch, dein Server hat nun eine Datenbank.
Diese kannst du nun benutzen, um Daten einfach & flexibel zu speichern und zu laden.

Falls du dich mit Datenbanken nicht gut genug auskennst, dann solltest du da etwas recherchieren.
Es gibt viele sehr hilfreiche Funktionen, die man kennen sollte.

Falls eine alte URL noch funktioniert, lösche den Ordner mit der URL als Namen in /var/www/html!" },
		{ "firewall_installed_form", @"Die Firewall wurde installiert.

Dazu wurde eine Datei in /etc/ Namens ""firewall.sh"" reingetan.
Diese stellt iptables so ein, dass unbenötigte Ports geschlossen werden und benötigte geöffnet bleiben.

Wenn du bestimmte Ports öffnen musst, geh einfach in die Datei /etc/firewall.sh rein und tu sie da rein.
Bestimmte Einstellungen sind schon drin, jedoch deaktiviert.
Um sie zu aktivieren, musst du einfach nur das ""#"" Zeichen am Anfang der Zeile bei den IPTABLES Zeilen entfernen." },
		{ "login_with_root", "Melde dich in deinen Server MIT root an!" },
		{ "login", "Anmelden" },
		{ "mainwindow_info", @"Hier kannst du nun die einzelnen Komponente installieren.
Durch erneute Installation werden die vorherigen Installationen entfernt.

Firewall erstellt eine ganze Reihe von Firewall-Einstellungen und öffnet dabei die Ports.
Benutze den Button erst am Ende, falls du andere MTA Ports als Standard nutzt." },
		{ "database", "Datenbank" },
		{ "running_user", "Ausführender Benutzer:" },
		{ "server_name", "Server-Name:" },
		{ "max_players", "Max. Spieler:" },
		{ "server_password", "Server-Passwort:" },
		{ "fps_limit", "FPS-Limit:" },
		{ "port", "Port" },
		{ "skin_modding", "Skin-Modding:" },
		{ "bullet_sync", "Bullet-Sync:" },
		{ "ssh_port", "SSH-Port:" },
		{ "user_password", "Benutzer-Passwort:" },
		{ "auto_start", "Auto-Start:" },
		{ "install_will_delete_old", "ACHTUNG: \r\nFalls der Installer vorher schon mal benutzt wurde, \r\nwird der alte Ordner entfernt!" },
		{ "email_address", "E-Mail-Adresse:" },
		{ "mtawindow_end", @"Herzlichen Glückwunsch, MTA wurde installiert.

Der Ordner befindet sich nun in /home/REPLACEIT1/MTA.
Den Server kannst du leicht mit dem .sh Script in der Console starten:
/etc/mta.sh start
/etc/mta.sh stop

Wenn du die MTA Konsole nach dem Start haben willst, musst du:
1. dich auf deinem Server mit dem User mta anmelden
2. ""screen -R mtaserver"" in der Console eingeben (ohne Anführungszeichen)

Beim Schließen des Screens wird auch MTA geschlossen!


Viel Spaß!" },
		{ "serveradmin_password", "serveradmin-Passwort:" },
		{ "serveradmin_password_info", "Wenn \"serveradmin-Passwort\" leer bleibt, wird automatisch ein Passwort generiert!" },
		{ "teamspeak_installed_info", @"Glückwunsch!
Dein Teamspeak-Server wurde installiert und ist nun einsatzbereit.

Der Token für serveradmin ist:
REPLACEIT1

Das Passwort für den Benutzer serveradmin ist:
REPLACEIT2

Den Server startest du mit:
/etc/ts.sh start

stoppen mit:
/etc/ts.sh stop" },
	};
}
