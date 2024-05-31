using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectsRandomizer : EditorWindow {
	bool ScaleMultiplier = false;	
	bool ScaleUniform = false;
	public static ObjectsRandomizer theWindow;
	Vector2 scaleVector = Vector2.zero;
	float x = 0.0f;
	float y = 0.0f;
	float z = 0.0f;
	[MenuItem ("Duplicator/Randomizer")]
	static void Init () 
	{
		theWindow= (ObjectsRandomizer)EditorWindow.GetWindow (typeof (ObjectsRandomizer));
		theWindow.minSize = new Vector2 (300.0f, 170.0f);
		theWindow.Show();
	}

	public enum WhatToRandomize 
	{ 
		Translation = 0, 
		Rotation = 1,
		Scale =2
	}
	public WhatToRandomize RandType;
	public enum SelectionType 
	{ 
		ChildrenOfSelection = 0,
		Selection = 1 

	}
	public SelectionType SelType;
	public enum RandomizationSPace 
	{ 
		Local = 0,
		World = 1, 
	}
	public RandomizationSPace ChosenSpace;
	public Vector3 minRand = Vector3.zero;
	public Vector3 MaxRand = new Vector3 (10.0f, 10.0f, 10.0f);

	void OnGUI () 
	{
		GUILayout.Label ("Object Randomizer", EditorStyles.boldLabel);
		SelType =(SelectionType)EditorGUILayout.EnumPopup ("Operate On", SelType);
		RandType =(WhatToRandomize)EditorGUILayout.EnumPopup ("Randomize: ", RandType);
		ChosenSpace =(RandomizationSPace)EditorGUILayout.EnumPopup ("Randomization Space", ChosenSpace);
		minRand = EditorGUILayout.Vector3Field("Min Values", minRand);
		MaxRand = EditorGUILayout.Vector3Field("Max Values", MaxRand);
		if (RandType == WhatToRandomize.Scale) {
			ScaleMultiplier = EditorGUILayout.Toggle ("Use Scale As Multiplier", ScaleMultiplier);
			ScaleUniform = EditorGUILayout.Toggle ("Force Uniform Scale", ScaleUniform);
			scaleVector.Set (300.0f, 210.0f);
			ChangeScale (scaleVector);
		} else {
			scaleVector.Set(300.0f, 170.0f);
			ChangeScale (scaleVector);
		}
		if (GUILayout.Button ("Randomize"))
		{
			RandomizeVals();
		}
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	static void ChangeScale(Vector2 minscale)
	{
		theWindow.minSize = minscale;
	}

	void RandomizeVals()
	{
		if (Selection.activeGameObject != null) {
			if (SelType == SelectionType.Selection) {
				for (int i = 0; i < Selection.gameObjects.Length; i++) {
					x = Random.Range (minRand.x, MaxRand.x);
					y = Random.Range (minRand.y, MaxRand.y);
					z = Random.Range (minRand.z, MaxRand.z);
					Vector3 newValues = new Vector3 (x, y, z);
					if (RandType == WhatToRandomize.Translation) {
						if (ChosenSpace == RandomizationSPace.World) {
							Selection.gameObjects [i].transform.Translate (newValues, Space.World);
						} else {
							Selection.gameObjects [i].transform.Translate (newValues, Space.Self);
						}
					} else if (RandType == WhatToRandomize.Rotation) {
						if (ChosenSpace == RandomizationSPace.World) {
							Selection.gameObjects [i].transform.Rotate (newValues, Space.World);
						} else {
							Selection.gameObjects [i].transform.Rotate (newValues, Space.Self);
						}
					} else {
						if (!ScaleMultiplier) {
							if (!ScaleUniform) {
								Selection.gameObjects [i].transform.localScale += newValues;
							} else {
								Vector3 newUniform = new Vector3 (newValues.x, newValues.x, newValues.x);
								Selection.gameObjects [i].transform.localScale += newUniform;
							}
						} else {
							if (!ScaleUniform) {
								Selection.gameObjects [i].transform.localScale = new Vector3 (newValues.x * Selection.gameObjects [i].transform.localScale.x, newValues.y * Selection.gameObjects [i].transform.localScale.y, newValues.z * Selection.gameObjects [i].transform.localScale.z);
							} else {
								Selection.gameObjects [i].transform.localScale = new Vector3 (newValues.x * Selection.gameObjects [i].transform.localScale.x, newValues.x * Selection.gameObjects [i].transform.localScale.y, newValues.x* Selection.gameObjects [i].transform.localScale.z);

							}
						}

					}
				}

			} else 
				
			{
				for (int i = 0; i < Selection.gameObjects.Length; i++) {
					GameObject[] selectionObjects = new GameObject[Selection.gameObjects[i].transform.childCount];
					for (int c = 0; c <Selection.gameObjects[i].transform.childCount;c++)
					{
						selectionObjects[c] = Selection.gameObjects[i].transform.GetChild(c).gameObject;
							x = Random.Range (minRand.x, MaxRand.x);
							y = Random.Range (minRand.y, MaxRand.y);
							z = Random.Range (minRand.z, MaxRand.z);
							Vector3 newValues = new Vector3 (x, y, z);
							if (RandType == WhatToRandomize.Translation) {
								if (ChosenSpace == RandomizationSPace.World) {
								selectionObjects[c].transform.Translate (newValues, Space.World);
								} else {
								selectionObjects[c].transform.Translate (newValues, Space.Self);
								}
							} else if (RandType == WhatToRandomize.Rotation) {
								if (ChosenSpace == RandomizationSPace.World) {
								selectionObjects[c].transform.Rotate (newValues, Space.World);
								} else {
								selectionObjects[c].transform.Rotate (newValues, Space.Self);
								}
							} else {
							if (!ScaleMultiplier) {
								if (!ScaleUniform) {
									selectionObjects [c].transform.localScale += newValues;
								} else {
									Vector3 newUniform = new Vector3 (newValues.x, newValues.x, newValues.x);
									selectionObjects [c].transform.localScale += newUniform;
								}
							} else {
								if (!ScaleUniform) {
									selectionObjects [c].transform.localScale = new Vector3 (newValues.x * Selection.gameObjects [i].transform.localScale.x, newValues.y * Selection.gameObjects [i].transform.localScale.y, newValues.z * Selection.gameObjects [i].transform.localScale.z);
								} else {
									selectionObjects [c].transform.localScale = new Vector3 (newValues.x * Selection.gameObjects [i].transform.localScale.x, newValues.x * Selection.gameObjects [i].transform.localScale.y, newValues.x * Selection.gameObjects [i].transform.localScale.z);
								}
							}
							}
					}	
				}
			}	

		} else {
			Debug.LogWarning ("You Must Select at least one Game Object");
		}
	}

}
