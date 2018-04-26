#! /bin/sh
echo 'Attempting to zip builds'
zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/
zip -r $(pwd)/Build/osx.zip $(pwd)/Build/osx/
