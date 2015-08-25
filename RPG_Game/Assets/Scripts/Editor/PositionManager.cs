using UnityEngine;
using UnityEditor;

public class PositionManager : MonoBehaviour {
	[MenuItem("Assets/Create/PositionManager")]
	public static void CreateAsset()
	{
		ScriptingObjects positionManager = ScriptableObject.CreateInstance<ScriptingObjects>();
		AssetDatabase.CreateAsset(positionManager, "Assets/newPositionManager.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = positionManager;
	}

	public static PositionManager ReadPositionsFromAsset(string Name)
	{
		string path = "/";

		object o = Resources.Load(path + Name);
		PositionManager retrievedPositions = (PositionManager)o;
		return retrievedPositions;
	}
}
