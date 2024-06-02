#!/bin/sh
dotnet fable --run npx vite build
git add dist && git commit -m "Publish $(date +"%T")"
git push origin `git subtree split --prefix dist main`:gh-pages --force
git reset HEAD~
rm -rf dist