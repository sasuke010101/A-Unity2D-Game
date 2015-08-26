using UnityEngine;
using System.Collections.Generic;

public class GameState {

	public static Player currennPlayer = ScriptableObject.CreateInstance<Player>();
	public static bool PlayerReturningHome;
	public static Dictionary<string, Vector3> LastScenePositions = new Dictionary<string, Vector3>();

	public static Vector3 GetLastScenePosition(string sceneName)
	{
		if(GameState.LastScenePositions.ContainsKey(sceneName))
		{
			var lastPos = GameState.LastScenePositions[sceneName];
			return lastPos;
		}else
		{
			return Vector3.zero;
		}
	}

	public static void SetLastScenePosition(string sceneName, Vector3 position)
	{
		if(GameState.LastScenePositions.ContainsKey(sceneName))
		{
			GameState.LastScenePositions[sceneName] = position;
		}
		else
		{
			GameState.LastScenePositions.Add(sceneName, position);
		}
	}
}
