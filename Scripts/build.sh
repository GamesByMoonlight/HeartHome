#! /bin/sh

echo "Attempting to build"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -silent-crashes \
  -batchmode \
  -nographics \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd)/ \
  -executeMethod MyEditorScript.PerformBuild  \
  -quit

# echo 'Build Log'
# cat $(pwd)/unity.log
# echo 'End Build Log'


