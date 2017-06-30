#!/bin/bash
 
IPTABLES="/sbin/iptables"
 
# Logging options.
#------------------------------------------------------------------------------
LOG="LOG --log-level debug --log-tcp-sequence --log-tcp-options"
LOG="$LOG --log-ip-options"
 
# Defaults for rate limiting
#------------------------------------------------------------------------------
RLIMIT="-m limit --limit 3/s --limit-burst 30"
 
# Default policies.
#------------------------------------------------------------------------------
 
# Drop everything by default.
$IPTABLES -P INPUT DROP
$IPTABLES -P FORWARD DROP
$IPTABLES -P OUTPUT DROP
 
# Set the nat/mangle/raw tables' chains to ACCEPT
$IPTABLES -t nat -P PREROUTING ACCEPT
$IPTABLES -t nat -P OUTPUT ACCEPT
$IPTABLES -t nat -P POSTROUTING ACCEPT
 
$IPTABLES -t mangle -P PREROUTING ACCEPT
$IPTABLES -t mangle -P INPUT ACCEPT
$IPTABLES -t mangle -P FORWARD ACCEPT
$IPTABLES -t mangle -P OUTPUT ACCEPT
$IPTABLES -t mangle -P POSTROUTING ACCEPT
 
# Cleanup.
#------------------------------------------------------------------------------
 
# Delete all
$IPTABLES -F
$IPTABLES -t nat -F
$IPTABLES -t mangle -F
 
# Delete all
$IPTABLES -X
$IPTABLES -t nat -X
$IPTABLES -t mangle -X
 
# Zero all packets and counters.
$IPTABLES -Z
$IPTABLES -t nat -Z
$IPTABLES -t mangle -Z
 
 
# Custom user-defined chains.
#------------------------------------------------------------------------------
 
# LOG packets, then ACCEPT.
$IPTABLES -N ACCEPTLOG
$IPTABLES -A ACCEPTLOG -j $LOG $RLIMIT --log-prefix "ACCEPT "
$IPTABLES -A ACCEPTLOG -j ACCEPT
 
# LOG packets, then DROP.
$IPTABLES -N DROPLOG
$IPTABLES -A DROPLOG -j $LOG $RLIMIT --log-prefix "DROP "
$IPTABLES -A DROPLOG -j DROP
 
# LOG packets, then REJECT.
# TCP packets are rejected with a TCP reset.
$IPTABLES -N REJECTLOG
$IPTABLES -A REJECTLOG -j $LOG $RLIMIT --log-prefix "REJECT "
$IPTABLES -A REJECTLOG -p tcp -j REJECT --reject-with tcp-reset
$IPTABLES -A REJECTLOG -j REJECT
 
# Only allows RELATED ICMP types
# (destination-unreachable, time-exceeded, and parameter-problem).
# TODO: Rate-limit this traffic?
# TODO: Allow fragmentation-needed?
# TODO: Test.
$IPTABLES -N RELATED_ICMP
$IPTABLES -A RELATED_ICMP -p icmp --icmp-type destination-unreachable -j ACCEPT
$IPTABLES -A RELATED_ICMP -p icmp --icmp-type time-exceeded -j ACCEPT
$IPTABLES -A RELATED_ICMP -p icmp --icmp-type parameter-problem -j ACCEPT
$IPTABLES -A RELATED_ICMP -j DROPLOG
 
# Make It Even Harder To Multi-PING
$IPTABLES  -A INPUT -p icmp -m limit --limit 1/s --limit-burst 2 -j ACCEPT
$IPTABLES  -A OUTPUT -p icmp -j ACCEPT
 
# Only allow the minimally required/recommended parts of ICMP. Block the rest.
#------------------------------------------------------------------------------
 
# Allow all ESTABLISHED ICMP traffic.
$IPTABLES -A INPUT -p icmp -m state --state ESTABLISHED -j ACCEPT $RLIMIT
$IPTABLES -A OUTPUT -p icmp -m state --state ESTABLISHED -j ACCEPT $RLIMIT
 
# Allow some parts of the RELATED ICMP traffic, block the rest.
$IPTABLES -A INPUT -p icmp -m state --state RELATED -j RELATED_ICMP $RLIMIT
$IPTABLES -A OUTPUT -p icmp -m state --state RELATED -j RELATED_ICMP $RLIMIT
 
# Allow incoming ICMP echo requests (ping), but only rate-limited.
$IPTABLES -A INPUT -p icmp --icmp-type echo-request -j ACCEPT $RLIMIT
 
# Allow outgoing ICMP echo requests (ping), but only rate-limited.
$IPTABLES -A OUTPUT -p icmp --icmp-type echo-request -j ACCEPT $RLIMIT
 
# Drop any other ICMP traffic.
$IPTABLES -A INPUT -p icmp -j DROPLOG
$IPTABLES -A OUTPUT -p icmp -j DROPLOG
$IPTABLES -A FORWARD -p icmp -j DROPLOG
 
# Selectively allow certain special types of traffic.
#------------------------------------------------------------------------------
 
# Allow loopback interface to do anything.
$IPTABLES -A INPUT -i lo -j ACCEPT
$IPTABLES -A OUTPUT -o lo -j ACCEPT
 
# Allow incoming connections related to existing allowed connections.
$IPTABLES -A INPUT -m state --state ESTABLISHED,RELATED -j ACCEPT
 
# Allow outgoing connections EXCEPT invalid
$IPTABLES -A OUTPUT -m state --state ESTABLISHED,RELATED -j ACCEPT
 
# Miscellaneous.
#------------------------------------------------------------------------------
 
# Explicitly drop invalid incoming traffic
$IPTABLES -A INPUT -m state --state INVALID -j DROP
 
# Drop invalid outgoing traffic, too.
$IPTABLES -A OUTPUT -m state --state INVALID -j DROP
 
# If we would use NAT, INVALID packets would pass - BLOCK them anyways
$IPTABLES -A FORWARD -m state --state INVALID -j DROP

# Drop some countries
# $IPTABLES -A INPUT -m geoip --src-cc CN,CA,MY,TH,JP,ES,KP,KR,UA,TW -j DROP
 
# Selectively allow certain outbound connections, block the rest.
#------------------------------------------------------------------------------
 
# Erlaube ausgehende DNS Anfragen. Few things will work without this.
$IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 53 -j ACCEPT
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 53 -j ACCEPT
 
# Erlaube ausgehende HTTP Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 80 -j ACCEPT
 
# Erlaube ausgehende HTTPS Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 443 -j ACCEPT
 
# Erlaube ausgehende SMTP Anfragen.
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 25 -j ACCEPT
 
# Erlaube ausgehende SMTPS Anfragen.
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 465 -j ACCEPT
 
# Erlaube ausgehende "submission" (RFC 2476) Anfragen.
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 587 -j ACCEPT
 
# Erlaube ausgehende POP3S Anfragen.
#$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 995 -j ACCEPT
 
# Erlaube ausgehende SSH Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 22 -j ACCEPT
 
# Erlaube ausgehende FTP Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 21 -j ACCEPT
 
# Erlaube ausgehende NTP Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 123 -j ACCEPT
 
# Erlaube ausgehende WHOIS Anfragen.
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 43 -j ACCEPT
 
# Erlaube ausgehende CVS Anfragen.
#$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 2401 -j ACCEPT
 
# Erlaube ausgehende MySQL Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 3306 -j ACCEPT
 
# Erlaube ausgehende SVN Anfragen.
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 3690 -j ACCEPT
 
# Erlaube ausgehende Mumble Anfragen.
#$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 64738 -j ACCEPT
#$IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 64738 -j ACCEPT

# Erlaube ausgehende MTA Anfragen.
$IPTABLES -A OUTPUT -m state --state NEW -p udp --dport REPLACEIT1 -j ACCEPT
$IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport REPLACEIT2 -j ACCEPT
$IPTABLES -A OUTPUT -m state --state NEW -p udp --dport REPLACEIT3 -j ACCEPT

# Erlaube ausgehende TS Anfragen.
# $IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 2010 -j ACCEPT
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 2008 -j ACCEPT

# Erlaube Sinusbot für Discord.
# $IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 50000:65000 -j ACCEPT

# Erlaube Sinusbot auf andere TS zu verbinden.
# $IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 4330 -j ACCEPT
# $IPTABLES -A OUTPUT -m state --state NEW -p udp --dport 9987 -j ACCEPT
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 30033 -j ACCEPT
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 10011 -j ACCEPT
# $IPTABLES -A OUTPUT -m state --state NEW -p tcp --dport 41144 -j ACCEPT

 
# Selectively allow certain inbound connections, block the rest.
#------------------------------------------------------------------------------
 
# Erlaube eingehende DNS Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p udp --dport 53 -j ACCEPT
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 53 -j ACCEPT
 
# Erlaube eingehende HTTP Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 80 -j ACCEPT
 
# Erlaube eingehende HTTPS Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 443 -j ACCEPT
 
# Erlaube eingehende POP3 Anfragen.
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 110 -j ACCEPT
 
# Erlaube eingehende IMAP4 Anfragen.
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 143 -j ACCEPT
 
# Erlaube eingehende POP3S Anfragen.
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 995 -j ACCEPT
 
# Erlaube eingehende SMTP Anfragen.
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 25 -j ACCEPT
 
# Erlaube eingehende SSH Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 22 -j ACCEPT
 
# Erlaube eingehende FTP Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 21 -j ACCEPT
##$IPTABLES -A INPUT -p tcp --destination-port 65000:65534 -j ACCEPT
 
# Erlaube eingehende Mumble-Anfragen.
#$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 64738 -j ACCEPT
#$IPTABLES -A INPUT -m state --state NEW -p udp --dport 64738 -j ACCEPT
 
# Erlaube eingehende MySQL Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 3306 -j ACCEPT

# Erlaube eingehende MTA Anfragen.
$IPTABLES -A INPUT -m state --state NEW -p udp --dport 22003 -j ACCEPT
$IPTABLES -A INPUT -m state --state NEW -p tcp --dport 22005 -j ACCEPT
$IPTABLES -A INPUT -m state --state NEW -p udp --dport 22126 -j ACCEPT

# Erlaube eingehende TS Anfragen.
# $IPTABLES -A INPUT -m state --state NEW -p udp --dport 9987 -j ACCEPT
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 30033 -j ACCEPT
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 10011 -j ACCEPT
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 41144 -j ACCEPT

# Erlaube eingehende Sinusbot Anfragen.
# $IPTABLES -A INPUT -m state --state NEW -p tcp --dport 8087 -j ACCEPT

 
# Explicitly log and reject everything else.
#------------------------------------------------------------------------------
# Use REJECT instead of REJECTLOG if you don't need/want logging.
$IPTABLES -A INPUT -j REJECT
$IPTABLES -A OUTPUT -j REJECT
$IPTABLES -A FORWARD -j REJECT

 
# Exit gracefully.
#------------------------------------------------------------------------------
 
    exit 0