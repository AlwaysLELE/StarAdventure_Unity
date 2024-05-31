using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImageDuplicator : EditorWindow {

	public Object theNewObject;
	Texture2D BlackAndWhiteImage;
	int Dup = 5;
	float Axis1MaxDistance = 10.0f;
	float Axis2MaxDistance = 10.0f;

	[MenuItem ("Duplicator/Image Duplicator")]
	static void Init () 
	{
		ImageDuplicator window = (ImageDuplicator)EditorWindow.GetWindow (typeof (ImageDuplicator));
		window.Show();
	}
	void OnGUI () 
	{
		GUILayout.Label ("Image Duplicator Options", EditorStyles.boldLabel);
		BlackAndWhiteImage = EditorGUILayout.ObjectField ("Image", BlackAndWhiteImage, typeof(Texture2D), true) as Texture2D;
		Dup = EditorGUILayout.IntField("Duplicated every(pixel)", Dup);
		Axis1MaxDistance = EditorGUILayout.FloatField("Axis 1 Max", Axis1MaxDistance);
		Axis2MaxDistance = EditorGUILayout.FloatField("Axis 2 Max", Axis2MaxDistance);

		if (GUILayout.Button ("Duplicate"))
		{
			DuplicateImage ();
		}
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	void DuplicateImage ()
	{
		if (Selection.activeGameObject != null) {	
			GameObject DuplicateSpecialParent = new GameObject ("DuplicateImageParent");
			DuplicateSpecialParent.transform.position = Vector3.zero;
			DuplicateSpecialParent.transform.rotation = Quaternion.identity;
			for (int i = 0; i < BlackAndWhiteImage.width; i++) {
				for (int j = 0; j < BlackAndWhiteImage.height; j++) {
					if (i % Dup== 0 && j% Dup == 0) {
						float normalizedX = ((float)i / (float)BlackAndWhiteImage.width) * Axis1MaxDistance;
						float normalizedZ = ((float)j / (float)BlackAndWhiteImage.height) * Axis2MaxDistance;
						float HeightValue = BlackAndWhiteImage.GetPixel (i, j).grayscale;
						if (HeightValue > 0) {
							int RItemSeletion = Random.Range (0, Selection.gameObjects.Length);
							theNewObject = Selection.gameObjects [RItemSeletion];
							GameObject OBJ = Instantiate (theNewObject) as GameObject;
							OBJ.transform.position = new Vector3 (normalizedX, 0, normalizedZ);
							OBJ.transform.parent = DuplicateSpecialParent.transform;
						}
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
