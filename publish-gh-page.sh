#!/bin/sh
dotnet fable --run npx vite build
git add dist && git commit -m "Publish $(date +"%T")"
git subtree push --prefix dist origin gh-pages
git reset HEAD~
rm -rf dist