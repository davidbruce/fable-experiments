#!/bin/sh
dotnet fable --run npx vite build
git subtree push --prefix dist origin gh-pages