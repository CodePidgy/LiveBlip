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
	rm -r Decode/bin/*
	rm -r LiveBlip/bin/*
	rm -r TrackerService/bin/*
elif [ $app = "decode" ]; then
	rm -r Decode/bin/*
elif [ $app = "liveblip" ]; then
	rm -r LiveBlip/bin/*
elif [ $app = "trackerservice" ]; then
	rm -r TrackerService/bin/*
else
	echo "Invalid app: $app" >&2

	exit 1
fi
