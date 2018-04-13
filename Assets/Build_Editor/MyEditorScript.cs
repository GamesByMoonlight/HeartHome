using UnityEditor;
public class MyEditorScript
{
	public static void PerformBuild ()
	{
		string[] scenes = { "Assets/_Scenes/Area 1.unity" };
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = scenes;
		buildPlayerOptions.locationPathName = "HeartHome.exe";
		buildPlayerOptions.target = BuildTarget.StandaloneWindows;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}
}