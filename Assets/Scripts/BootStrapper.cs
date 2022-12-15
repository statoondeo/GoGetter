using UnityEngine;

public class BootStrapper
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void OnRuntimeMethodLoad()
	{
		// TODO
		Application.targetFrameRate = 60;

		// Gestion du jeu
		GameObject.Instantiate(Resources.Load("GameManager"));
	}
}