#!/bin/bash

cd "TrackerService"

rsync -avzP -e "ssh" root@213.219.36.37:~/trackerservice/logs/* logs/ --info=progress2


