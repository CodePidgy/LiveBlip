#!/bin/bash

declare -l app
build=0
clean=0
dev=0
silent=0

while getopts "a:bcds" opt; do
	case $opt in
		a)
			app=$OPTARG
			;;
		b)
			build=1
			;;
		d)
			dev=1
			;;
		c)
			clean=1
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

# build is set
if [ $build -eq 1 ]; then
	# dev is not set
	if [ $dev -eq 0 ]; then
		scripts/build.sh -a "$app"
	# dev is set
	else
		scripts/build.sh -a "$app" -d
	fi
fi

# type is trackerservice
if [ $app = "trackerservice" ]; then
	cd "TrackerService"

	# dev is not set
	if [ $dev -eq 0 ]; then
		if [ $silent -eq 0 ]; then
			echo "Stopping..."
			ssh linode "systemctl stop trackerservice"
		fi

		echo "Removing..."
		ssh linode "rm -r ~/trackerservice/*"

		echo "Copying..."
		rsync -avzP -e "ssh -i ~/.ssh/linode" bin/Release/net8.0/linux-x64/publish/* root@213.219.36.37:~/trackerservice/ --info=progress2

		if [ $silent -eq 0 ]; then
			echo "Enabling..."
			ssh linode "systemctl start trackerservice"
		fi
	# dev is set
	else
		if [ $silent -eq 0 ]; then
			echo "Stopping..."
			ssh linode "systemctl stop trackerservice-dev"
		fi

		echo "Removing..."
		ssh linode "rm -r ~/trackerservice-dev/*"

		echo "Copying..."
		rsync -avzP -e "ssh -i ~/.ssh/linode" bin/Debug/net8.0/linux-x64/publish/* root@213.219.36.37:~/trackerservice-dev/ --info=progress2

		if [ $silent -eq 0 ]; then
			echo "Enabling..."
			ssh linode "systemctl start trackerservice-dev"
		fi
	fi
# type is liveblip
elif [ $app = "liveblip" ]; then
	cd "LiveBlip"

	# dev is not set
	if [ $dev -eq 0 ]; then
		if [ $silent -eq 0 ]; then
			echo "Stopping..."
			ssh linode "systemctl stop liveblip"
		fi

		echo "Removing..."
		ssh linode "rm -r /var/www/liveblip/*"

		echo "Copying..."
		rsync -avzP -e "ssh -i ~/.ssh/linode" bin/Release/net8.0/linux-x64/publish/* root@213.219.36.37:/var/www/liveblip/ --info=progress2

		if [ $silent -eq 0 ]; then
			echo "Enabling..."
			ssh linode "systemctl start liveblip"
		fi
	# dev is set
	else
		if [ $silent -eq 0 ]; then
			echo "Stopping..."
			ssh linode "systemctl stop liveblip-dev"
		fi

		echo "Removing..."
		ssh linode "rm -r /var/www/liveblip-dev/*"

		echo "Copying..."
		rsync -avzP -e "ssh -i ~/.ssh/linode" bin/Debug/net8.0/linux-x64/publish/* root@213.219.36.37:/var/www/liveblip-dev/ --info=progress2

		if [ $silent -eq 0 ]; then
			echo "Enabling..."
			ssh linode "systemctl start liveblip-dev"
		fi
	fi
# unknown type
else
	echo "Invalid app: $app" >&2

	exit 1
fi

# clean is set
if [ $clean -eq 1 ]; then
	echo "Cleaning..."

	# dev is not set
	if [ $dev -eq 0 ]; then
		rm -r bin/Release/*
	# dev is set
	else
		rm -r bin/Debug/*
	fi
fi
