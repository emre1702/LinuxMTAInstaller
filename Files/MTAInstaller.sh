 #!/bin/bash -x
 #==============================================================================#
 #                                mtasa-install                                 #
 #------------------------------------------------------------------------------#
 #  This shellscript installs MTA:SA on your server. You can configure it and   #
 #  modify it as desired, you can even improve it if you want.                  #
 #==============================================================================#
  
 NUM_VERSION=undef
 FUL_VERSION=undef
 ARCH_TYPE=""
  
 getServerVersion()
 {
     wget https://raw.githubusercontent.com/multitheftauto/mtasa-blue/master/Server/version.h # we need to find latest stable version here
     local MAJOR_VERSION="$(cat version.h | grep "#define MTASA_VERSION_MAJOR" | awk '{ print $3 }' | sed 's/\r//g')"
     local MINOR_VERSION="$(cat version.h | grep "#define MTASA_VERSION_MINOR" | awk '{ print $3 }' | sed 's/\r//g')"
     local MAINTENANCE_VERSION="$(cat version.h | grep "#define MTASA_VERSION_MAINTENANCE" | awk '{ print $3 }' | sed 's/\r//g')"
     NUM_VERSION="${MAJOR_VERSION}${MINOR_VERSION}${MAINTENANCE_VERSION}"
     FUL_VERSION="${MAJOR_VERSION}.${MINOR_VERSION}.${MAINTENANCE_VERSION}"
     rm -f version.h
 }
  
 getArchitecture()
 {
     if ((1<<32)); then
         ARCH_TYPE="_x64"
     fi
 }
  
 downloadFiles()
 {
     wget http://linux.mtasa.com/dl/${NUM_VERSION}/multitheftauto_linux${ARCH_TYPE}-${FUL_VERSION}.tar.gz
     wget http://linux.mtasa.com/dl/${NUM_VERSION}/baseconfig-${FUL_VERSION}.tar.gz
 }
  
 unpack()
 {
     tar -xf multitheftauto_linux${ARCH_TYPE}-${FUL_VERSION}.tar.gz
     tar -xf baseconfig-${FUL_VERSION}.tar.gz
 }
  
 moveConfig()
 {
     mv baseconfig/* multitheftauto_linux${ARCH_TYPE}-${FUL_VERSION}/mods/deathmatch
     rm -rf baseconfig
	 mv multitheftauto_linux${ARCH_TYPE}-${FUL_VERSION} MTA
     cd MTA
 }
  
 installResources()
 {
     mkdir mods/deathmatch/resources
     cd mods/deathmatch/resources
     wget http://mirror.mtasa.com/mtasa/resources/mtasa-resources-latest.zip
     unzip mtasa-resources-latest.zip
     cd ../../..
 }
  
 clean()
 {
     rm -f ../multitheftauto_linux${ARCH_TYPE}-${FUL_VERSION}.tar.gz
     rm -f ../baseconfig-${FUL_VERSION}.tar.gz
     rm -f mods/deathmatch/resources/mtasa-resources-latest.zip
 }
  
 main()
 {
     getServerVersion
     getArchitecture
     clean
     downloadFiles
     unpack
     moveConfig
     installResources
     clean
  
     echo "Installation ready!"
 }
  
 main # calling program entry point