﻿using UnityEngine;
using UnityEditor;
using System.IO;

public static class CustomAssetUtility {

	public static void CreateAsset<T>() where T : ScriptableObject{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);

		if(path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)),"");
		}

		CreateAsset<T>(path);
	}

	public static void CreateAsset<T>(string path) where T : ScriptableObject{
		T asset = ScriptableObject.CreateInstance<T>();

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New" + typeof(T).ToString() + ".asset");
		AssetDatabase.CreateAsset(asset,assetPathAndName);
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}
