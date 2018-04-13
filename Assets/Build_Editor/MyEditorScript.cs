using UnityEditor;
class MyEditorScript
{
	static void PerformBuild ()
	{
		string[] scenes = { "Assets/_Scenes/Area 1.unity" };
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = scenes;
		buildPlayerOptions.locationPathName = "winBuild";
		buildPlayerOptions.target = BuildTarget.StandaloneWindows;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}
}