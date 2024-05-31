using UnityEngine;
using System.Collections;
using UnityEditor;

public class PointAtIt : EditorWindow {

	[MenuItem ("Duplicator/Point Objects")]
	static void Init () 
	{
		PointAtIt window = (PointAtIt)EditorWindow.GetWindow (typeof (PointAtIt));
		window.minSize = new Vector2 (285.0f, 85.0f);
		window.Show();
	}
	public Transform theNewObject;
	public enum SelectionType 
	{ 
		ChildrenOfSelection = 0,
		Selection = 1

	}
	public SelectionType SelType;

	void OnGUI () 
	{
		GUILayout.Label ("Objects Pointing forward Axis (Z) (Permanent)", EditorStyles.boldLabel);
		SelType =(SelectionType)EditorGUILayout.EnumPopup ("Operate On", SelType);
		theNewObject = EditorGUILayout.ObjectField ("Object to Point At", theNewObject, typeof(Transform), true) as Transform;
		if (GUILayout.Button ("Point Objects"))
		{
			PointObjects();
		}
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	void PointObjects()
	{
		if (Selection.activeGameObject != null) {
			if (SelType == SelectionType.Selection) {
				GameObject[] selectionObjects = new GameObject[Selection.gameObjects.Length];
				selectionObjects = Selection.gameObjects;
				for (int s = 0; s < selectionObjects.Length; s++) {
					Selection.gameObjects [s].transform.LookAt (theNewObject.position);
				}
			} else {
				for (int i = 0; i < Selection.gameObjects.Length; i++) {
					for (int c = 0; c < Selection.gameObjects [i].transform.childCount; c++) {
						Selection.gameObjects [i].transform.GetChild (c).gameObject.transform.LookAt (theNewObject.position);
					}
				}
			}
		}
		else 
		{
			Debug.LogWarning ("You Must Select at least one Game Object");
		}
	}
}
