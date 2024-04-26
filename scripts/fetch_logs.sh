#!/bin/bash

dev=0
silent=0

while getopts "a:bcds" opt; do
	case $opt in
		d)
			dev=1
			;;
		\?)
			echo "Invalid option: $OPTARG" >&2
			;;
	esac
done


cd "TrackerService"

# dev is not set
if [ $dev -eq 0 ]; then
	rsync -avzP -e "ssh -i ~/.ssh/linode" root@213.219.36.37:~/trackerservice/logs/* logs/ --info=progress2
# dev is set
else
	rsync -avzP -e "ssh -i ~/.ssh/linode" root@213.219.36.37:~/trackerservice-dev/logs/* logs/ --info=progress2
fi


