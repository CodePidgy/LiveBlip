# LiveBlip

## Development

### Building

Build the app binaries

`scripts/build.sh`

Arguments:  
`-a`: App to build, options are `liveblip` and `trackerservice`  
`-d`: Build the app in development mode

### Publishing

Publish the app binaries to the server

`scripts/publish.sh`

Arguments:  
`-a`: App to publish, options are `liveblip` and `trackerservice`  
`-b`: Build the app before publishing  
`-c`: Clean the binaries directory after publishing  
`-d`: Publish the app in development mode  

### Cleaning

Clean the binaries directory

`scripts/clean.sh`

Arguments:  
`-a`: App to clean, options are `liveblip` and `trackerservice`, will default to both if left blank

## Git

### Commit Verbs

- Add = Create a capability e.g. feature, test, dependency  
- Cut = Remove a capability e.g. feature, test, dependency  
- Fix = Fix an issue e.g. bug, typo, accident, misstatement  
- Bump = Increase the version of something e.g. dependency  
- Make = Change the build process, tooling, or infra  
- Start = Begin doing something; e.g. create a feature flag  
- Stop = End doing something; e.g. remove a feature flag  
- Refactor = A code change that MUST be just a refactoring  
- Reformat = Refactor of formatting, e.g. omit whitespace  
- Optimise = Refactor of performance, e.g. speed up code  
- Document = Refactor of documentation, e.g. help files  

### The 7 Rules

- Separate subject from body with a blank line  
- Limit the subject line to 50 characters  
- Capitalise the subject line  
- Do not end the subject line with a period  
- Use the imperative mood in the subject line  
- Wrap the body at 72 characters  
- Use the body to explain what and why vs. how  
