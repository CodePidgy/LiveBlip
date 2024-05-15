#!/bin/bash

declare -l app
dev=0
silent=0

while getopts "a:bcds" opt; do
	case $opt in
		a)
			app=$OPTARG
			;;
		s)
			silent=1
			;;
		\?)
			echo "Invalid option: $OPTARG" >&2
			;;
	esac
done

# type is not set
if [ -z "$app" ]; then
	echo "Must specify '-a'" >&2

	exit 1
fi

scripts/clean.sh -a "$app"

# type is trackerservice
if [ $app = "trackerservice" ]; then
	cd "TrackerService"

	if [ $silent -eq 0 ]; then
		echo "Stopping..."
		ssh root@213.219.36.37 "systemctl stop trackerservice"
	fi

	echo "Building..."
	dotnet publish -c Release -r linux-x64

	echo "Removing..."
	ssh root@213.219.36.37 "rm -r ~/trackerservice/*"

	echo "Copying..."
	rsync -avzP -e "ssh" bin/Release/net8.0/linux-x64/publish/* root@213.219.36.37:~/trackerservice/ --info=progress2

	if [ $silent -eq 0 ]; then
		echo "Enabling..."
		ssh root@213.219.36.37 "systemctl start trackerservice"
	fi
# type is liveblip
elif [ $app = "liveblip" ]; then
	cd "LiveBlip"

	if [ $silent -eq 0 ]; then
		echo "Stopping..."
		ssh root@213.219.36.37 "systemctl stop liveblip"
	fi

	echo "Building..."
	dotnet publish -c Release -r linux-x64

	echo "Removing..."
	ssh root@213.219.36.37 "rm -r ~/liveblip/*"

	echo "Copying..."
	rsync -avzP -e "ssh" bin/Release/net8.0/linux-x64/publish/* root@213.219.36.37:~/liveblip/ --info=progress2

	if [ $silent -eq 0 ]; then
		echo "Enabling..."
		ssh root@213.219.36.37 "systemctl start liveblip"
	fi
# unknown type
else
	echo "Invalid app: $app" >&2

	exit 1
fi
