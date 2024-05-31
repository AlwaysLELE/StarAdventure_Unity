using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DuplicateSpecial : EditorWindow {

	int Quantity = 10;
	public Object theNewObject;
	Vector3 Translate = Vector3.zero;
	Vector3 Rotate = Vector3.zero;
	Vector3 Scale = Vector3.zero;
	Vector3 originalTranslate=Vector3.zero;
	Vector3 originalRotate = Vector3.zero;
	Vector3 originalScale = Vector3.zero;
	Vector3 TempTranslate = Vector3.zero;
	Vector3 TempRotate = Vector3.zero;
	Vector3 TempScale = Vector3.zero;
	bool ScaleMultiplier = false;
	Vector3 nextItemTranslates,nextItemScale;

	public enum DuplicationSpace 
	{ 
		World = 0, 
		Local = 1
	}

	public DuplicationSpace ChosenSpace;
	[MenuItem("Duplicator/Duplicate Special")]
	static void Init () 
	{
		DuplicateSpecial window = (DuplicateSpecial)EditorWindow.GetWindow (typeof (DuplicateSpecial));
		window.Show();
	}

	void OnGUI () {
		GUILayout.Label ("Duplicate Options", EditorStyles.boldLabel);
		Translate = EditorGUILayout.Vector3Field("Translate", Translate);
		Rotate = EditorGUILayout.Vector3Field("Rotate", Rotate);
		Scale = EditorGUILayout.Vector3Field("Scale", Scale);
		EditorGUILayout.Separator ();
		ScaleMultiplier = EditorGUILayout.Toggle ("Use Scale As Multiplier", ScaleMultiplier);
		ChosenSpace =(DuplicationSpace)EditorGUILayout.EnumPopup ("Duplication Space", ChosenSpace);
		Quantity = EditorGUILayout.IntSlider ("Quantity", Quantity,1,1000);
		EditorGUILayout.Separator ();
		if (GUILayout.Button ("Duplicate"))
		{
			Duplicate();
		}
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	void Duplicate()
	{
		if (Selection.activeGameObject != null) 
		{	
			int last = 0;
			for (int i=0; i<Selection.transforms.Length; i++) {
				if (Selection.transforms[i] ==  Selection.activeTransform) {
					last = i;
					break;
				}
			}

			GameObject DuplicateSpecialParent = new GameObject ("DuplicateSpecialParent");
			DuplicateSpecialParent.transform.position = Selection.transforms [last].position;
			DuplicateSpecialParent.transform.rotation = Quaternion.identity;
			originalTranslate = Selection.transforms [last].position;
			originalRotate = Selection.transforms [last].eulerAngles;
			originalScale = Selection.transforms [last].localScale;
		
			TempRotate = originalRotate + Rotate;
			if (!ScaleMultiplier) {
				TempScale = originalScale + Scale;
			} else {
				TempScale = new Vector3 (Scale.x * originalScale.x, Scale.y * originalScale.y, Scale.z * originalScale.z);
			}
			nextItemTranslates = originalTranslate;
			for (int i = 0; i < Quantity; i++) 
			{
				int RItemSeletion = Random.Range (0, Selection.gameObjects.Length);
				theNewObject = Selection.gameObjects [RItemSeletion];
				GameObject OBJ = Instantiate (theNewObject) as GameObject;
				OBJ.transform.position = nextItemTranslates;
				OBJ.transform.rotation = Quaternion.identity;
				switch (ChosenSpace) 
				{
				case DuplicationSpace.World:
					OBJ.transform.Rotate (TempRotate.x,TempRotate.y,TempRotate.z,Space.World);
					OBJ.transform.Translate (Translate,Space.World);
					break;
				case DuplicationSpace.Local:
					OBJ.transform.Rotate (TempRotate.x, TempRotate.y, TempRotate.z, Space.Self);
					OBJ.transform.Translate (Translate, Space.Self);
					break;
				}

				OBJ.transform.localScale = TempScale;
				OBJ.transform.SetParent (DuplicateSpecialParent.transform);
				TempTranslate += Translate;
				if (!ScaleMultiplier) {
					TempScale += Scale;
				} else {
					TempScale = new Vector3 (Scale.x * TempScale.x, Scale.y * TempScale.y, Scale.z * TempScale.z);
				}

				TempRotate += Rotate;
				nextItemTranslates = OBJ.transform.position;
			}
		}
		else 
		{
			Debug.LogWarning ("You Must Select at least one Game Object");
		}
	}

}
