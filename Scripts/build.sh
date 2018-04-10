#! /bin/sh

echo "Attempting to build ${project} for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd)/ \
  -executeMethod "$(pwd)/Build/windows/${project}.exe"  \
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log
