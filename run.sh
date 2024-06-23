#! /bin/sh
dotnet fable watch --verbose --run npx nodemon -e module.css --exec "fcm --outFile src/CssModules.fs" & npx vite