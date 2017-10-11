using UnityEngine;
using System.Collections;
using UnityEditor;

public class BallScriptable 
{
	[MenuItem("Assets/Create/BallScriptable")]
	public static void CreateMyAsset()
	{
		BallParams asset = ScriptableObject.CreateInstance<BallParams>();

		AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
}