#!/bin/bash

declare -l app
dev=0

while getopts "a:d" opt; do
	case $opt in
		a)
			app=$OPTARG
			;;
		d)
			dev=1
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

# type is console
if [ $app = "trackerservice" ]; then
	cd "TrackerService"
elif [ $app = "liveblip" ]; then
	cd "LiveBlip"
fi

echo "Building..."

# dev is not set
if [ $dev -eq 0 ]; then
	rm -r bin/Release/*

	dotnet publish -c Release -r linux-x64 --sc true
# dev is set
else
	rm -r bin/Debug/*

	dotnet publish -c Debug -r linux-x64 --sc true
fi
