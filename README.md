# LiveBlip

A website that allows for the tracking of select GPS devices

## Development

### Pushing

Publish the app binaries to the server

`scripts/push.sh`

Arguments:  
`-s`: Skip restarting the services after publishing

### Cleaning

Clean the binaries directory

`scripts/clean.sh`

Arguments:  
`-a`: App to clean, options are `liveblip` and `trackerservice`, will default to both if left blank

## Git

### Commit Verbs

- Add = Create a capability e.g. feature, test, dependency  
- Remove = Remove a capability e.g. feature, test, dependency  
- Fix = Fix an issue e.g. bug, typo, accident, misstatement  
- Improve = Improve the look or feel of something e.g. visual, user experience
- Bump = Increase the version of something e.g. dependency  
- Make = Change the build process, tooling, or infra  
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
