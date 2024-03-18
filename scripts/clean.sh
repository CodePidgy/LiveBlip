#!/bin/bash

declare -l app

while getopts "a:" opt; do
	case $opt in
		a)
			app=$OPTARG
			;;
		\?)
			echo "Invalid option: $OPTARG" >&2
			;;
	esac
done

if [ -z "$app" ]; then
	rm -r Decode/bin/Release/*
	rm -r Decode/bin/Debug/*
	rm -r LiveBlip/bin/Release/*
	rm -r LiveBlip/bin/Debug/*
	rm -r TrackerService/bin/Release/*
	rm -r TrackerService/bin/Debug/*
elif [ $app = "decode" ]; then
	rm -r Decode/bin/Release/*
	rm -r Decode/bin/Debug/*
elif [ $app = "liveblip" ]; then
	rm -r LiveBlip/bin/Release/*
	rm -r LiveBlip/bin/Debug/*
elif [ $app = "trackerservice" ]; then
	rm -r TrackerService/bin/Release/*
	rm -r TrackerService/bin/Debug/*
else
	echo "Invalid app: $app" >&2

	exit 1
fi
