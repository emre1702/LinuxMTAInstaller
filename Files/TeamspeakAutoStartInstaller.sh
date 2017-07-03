#!/bin/sh
### BEGIN INIT INFO
# chkconfig: 2345 99 01
# Provides: ts3server_startscript.sh
# Required-Start: $all
# Required-Stop:
# Default-Start: 2 3 4 5
# Default-Stop: 0 1 6
# Short-Description: Startet und stoppt TS
# Description: Startet und stoppt Teamspeak
### END INIT INFO

USER="REPLACEIT1"
DIR="/home/${USER}/Teamspeak"

case "$1" in 
start)
su $USER -c "${DIR}/ts3server_startscript.sh start inifile=ts3server.ini"
;;
stop)
su $USER -c "${DIR}/ts3server_startscript.sh stop inifile=ts3server.ini"
;;
*)
echo "Benutze: basename $0 {start|stop}" >&2
exit 1
;;
esac
exit 0