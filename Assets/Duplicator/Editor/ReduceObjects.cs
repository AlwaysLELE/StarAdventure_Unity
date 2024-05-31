using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReduceObjects : EditorWindow
{
	[MenuItem ("Duplicator/Reduce Objects")]
	static void Init () 
	{
		ReduceObjects window = (ReduceObjects)EditorWindow.GetWindow (typeof (ReduceObjects));
		window.Show();
	}

	int percentage = 20;
	public enum ReductionType 
	{ 
		Hide = 0, 
		Destroy = 1
	}
	public ReductionType RedType;

	public enum SelectionType 
	{ 
		ChildrenOfSelection = 0,
		Selection = 1

	}
	public SelectionType SelType;

	void OnGUI () 
	{
		GUILayout.Label ("Object Reduction (Permanent)", EditorStyles.boldLabel);
		RedType =(ReductionType)EditorGUILayout.EnumPopup ("Reduce Method", RedType);
		SelType =(SelectionType)EditorGUILayout.EnumPopup ("Operate On", SelType);
		percentage = EditorGUILayout.IntSlider ("Reduce Percentage",percentage, 1, 100);
		if (GUILayout.Button ("Reduce"))
		{
			Reduce ();
		}
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	void Reduce()
	{
		if (Selection.activeGameObject != null) {
			if (SelType == SelectionType.Selection) {
				int ReductionAmount = Mathf.FloorToInt ((float)percentage * (float)Selection.gameObjects.Length / 100.0f);
				GameObject[] selectionObjects = new GameObject[Selection.gameObjects.Length];
				selectionObjects = Selection.gameObjects;
				for (int s = 0; s < selectionObjects.Length; s++) {
					GameObject tempObject = selectionObjects [s];
					int r = Random.Range (s, selectionObjects.Length);
					selectionObjects [s] = selectionObjects [r];
					selectionObjects [r] = tempObject;
				}
				for (int x = 0; x < ReductionAmount; x++) {
					if (RedType == ReductionType.Hide) {
						selectionObjects [x].SetActive (false);
					} else {
						DestroyImmediate (selectionObjects [x]);
					}
				}
				Debug.Log ("Reduced: " + ReductionAmount + " Game Objects");
			} else 
			{
				for (int i = 0; i < Selection.gameObjects.Length; i++) {
					int ReductionAmount = Mathf.FloorToInt ((float)percentage * (float)Selection.gameObjects[i].transform.childCount / 100.0f);
					GameObject[] selectionObjects = new GameObject[Selection.gameObjects[i].transform.childCount];
					for (int c = 0; c <Selection.gameObjects[i].transform.childCount;c++)
					{
						selectionObjects[c] = Selection.gameObjects[i].transform.GetChild(c).gameObject;
					}
					for (int s = 0; s < selectionObjects.Length; s++) {
						GameObject tempObject = selectionObjects [s];
						int r = Random.Range (s, selectionObjects.Length);
						selectionObjects [s] = selectionObjects [r];
						selectionObjects [r] = tempObject;
					}
					for (int x = 0; x < ReductionAmount; x++) {
						if (RedType == ReductionType.Hide) {
							selectionObjects [x].SetActive (false);
						} else {
							DestroyImmediate (selectionObjects [x]);
						}
					}
					Debug.Log ("Reduced: " + ReductionAmount + " from Parent "+ Selection.gameObjects[i].name);
				}
			}
		}
		else 
		{
			Debug.LogWarning ("You Must Select at least one Game Object");
		}
	}
}

