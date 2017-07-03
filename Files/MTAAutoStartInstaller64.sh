#!/bin/sh
### BEGIN INIT INFO
# chkconfig: 2345 99 01
# Provides: mta-server64
# Required-Start: $all
# Required-Stop:
# Default-Start: 2 3 4 5
# Default-Stop: 0 1 6
# Short-Description: starts and stops MTA
# Description: starts and stops MTA
### END INIT INFO

USER="REPLACEIT1"
DIR="/home/${USER}/MTA"

case "$1" in 
start)
su $USER -c "screen -dmS mtaserver ${DIR}/mta-server64 start"
;;
stop)
su $USER -c "screen -S mtaserver -X quit"
;;
*)
echo "Benutze: basename $0 {start|stop}" >&2
exit 1
;;
esac
exit 0