#! /bin/sh

echo 'Attempting to zip builds'
zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/
