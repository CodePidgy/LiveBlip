# LiveBlip

## Building

Build the app binaries

`scripts/build.sh`

Arguments:  
`-a`: App to build, options are `liveblip` and `trackerservice`  
`-d`: Build the app in development mode

## Publishing

Publish the app binaries to the server

`scripts/publish.sh`

Arguments:  
`-a`: App to publish, options are `liveblip` and `trackerservice`  
`-b`: Build the app before publishing  
`-c`: Clean the binaries directory after publishing  
`-d`: Publish the app in development mode  

## Cleaning

Clean the binaries directory

`scripts/clean.sh`

Arguments:  
`-a`: App to clean, options are `liveblip` and `trackerservice`, will default to both if left blank
