#! /bin/sh

# BASE_URL=http://netstorage.unity3d.com/unity
# HASH=fc1d3344e6ea
# VERSION=2017.3.1f1

# download() {
#   file=$1
#   url="$BASE_URL/$HASH/$package"

#   echo "Downloading from $url: "
#   curl -o `basename "$package"` "$url"
# }

# install() {
#   package=$1
#   download "$package"

#   echo "Installing "`basename "$package"`
#   sudo installer -dumplog -package `basename "$package"` -target /
# }

# # See $BASE_URL/$HASH/unity-$VERSION-$PLATFORM.ini for complete list
# # of available packages, where PLATFORM is `osx` or `win`

# install "MacEditorInstaller/Unity-$VERSION.pkg"
# install "MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-$VERSION.pkg"
# install "MacEditorTargetInstaller/UnitySetup-Mac-Support-for-Editor-$VERSION.pkg"
# install "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$VERSION.pkg"



#! /bin/sh

# Download Unity3D installer into the container
#  The below link will need to change depending on the version, this one is for 5.5.1
#  Refer to https://unity3d.com/get-unity/download/archive and find the link pointed to by Mac "Unity Editor"
echo 'Downloading Unity 2017.3.1f1 pkg:'
curl --retry 5 -o Unity.pkg http://netstorage.unity3d.com/unity/fc1d3344e6ea/MacEditorInstaller/Unity-2017.3.1f1.pkg
if [ $? -ne 0 ]; then { echo "Download failed"; exit $?; } fi

# In Unity 5 they split up build platform support into modules which are installed separately
# By default, only Mac OSX support is included in the original editor package; Windows, Linux, iOS, Android, and others are separate
# In this example we download Windows support. Refer to http://unity.grimdork.net/ to see what form the URLs should take
echo 'Downloading Unity 2017.3.1f1 Windows Build Support pkg:'
curl --retry 5 -o Unity_win.pkg http://netstorage.unity3d.com/unity/fc1d3344e6ea/MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-2017.3.1f1.pkg
if [ $? -ne 0 ]; then { echo "Download failed"; exit $?; } fi

# Run installer(s)
echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
echo 'Installing Unity_win.pkg'
sudo installer -dumplog -package Unity_win.pkg -target /

