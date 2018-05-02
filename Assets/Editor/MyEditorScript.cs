using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MyEditorScript
{
	public static void PerformBuild ()
	{
		string[] scenes = {"Assets/_Scenes/Area 1.unity", 
		"Assets/_Scenes/Area 2.unity", 
		"Assets/_Scenes/Castle.unity", 
		"Assets/_Scenes/Area 3.unity" };
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = scenes;
		buildPlayerOptions.locationPathName = "Build/windows/HeartHome.exe";
		buildPlayerOptions.target = BuildTarget.StandaloneWindows;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

	public static void PerformOSXBuild ()
	{
		string[] scenes = {"Assets/_Scenes/Area 1.unity", 
		"Assets/_Scenes/Area 2.unity", 
		"Assets/_Scenes/Castle.unity", 
		"Assets/_Scenes/Area 3.unity" };
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = scenes;
		buildPlayerOptions.locationPathName = "Build/osx/HeartHome.app";
		buildPlayerOptions.target = BuildTarget.StandaloneOSX;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}
}