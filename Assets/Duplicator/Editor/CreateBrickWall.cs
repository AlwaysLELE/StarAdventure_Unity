using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateBrickWall : EditorWindow {

	int Columns = 10;
	int Rows = 10;
	float BrickOffset = 0.2f;
	public Object theNewObject;
	float MinX,MinY,MaxX,MaxY;
	float offsetx,offsety,offsetz;
	float offset;

	[MenuItem ("Duplicator/Create Brick Wall")]
	static void Init () 
	{
		CreateBrickWall window = (CreateBrickWall)EditorWindow.GetWindow (typeof (CreateBrickWall));
		window.Show();
	}
	void OnGUI () {
		GUILayout.Label ("Wall Options", EditorStyles.boldLabel);
		Columns = EditorGUILayout.IntField("Number Of Columns", Columns);
		Rows = EditorGUILayout.IntField("Number Of Rows", Rows);
		BrickOffset = EditorGUILayout.FloatField("Z offset Random Max", BrickOffset);

		if (GUILayout.Button ("Create Wall"))
		{
			CreateWall();
		}
	}
	void OnInspectorUpdate() {
		Repaint();
	}
	void CreateWall()
	{
		if (Selection.activeGameObject != null) 
		{
			if (Selection.gameObjects [0].GetComponent<MeshFilter> ().sharedMesh != null) {
				theNewObject = Selection.gameObjects [0];
				GameObject WallHandle = new GameObject ("Wall Parent");
				WallHandle.transform.position = Vector3.zero;
				WallHandle.transform.rotation = Quaternion.identity;
				Mesh mesh = Selection.gameObjects [0].GetComponent<MeshFilter> ().sharedMesh;
				Bounds bounds = mesh.bounds;
				Vector3 MinBounds = bounds.min;
				Vector3 MaxBounds = bounds.max;
				MinX = MinBounds.x * Selection.gameObjects [0].transform.localScale.x; 
				MinY = MinBounds.y * Selection.gameObjects [0].transform.localScale.y;
				//MinZ = MinBounds.z * Selection.gameObjects [0].transform.localScale.z;
				MaxX = MaxBounds.x * Selection.gameObjects [0].transform.localScale.x;
				MaxY = MaxBounds.y * Selection.gameObjects [0].transform.localScale.y;
				//MaxZ = MaxBounds.y * Selection.gameObjects [0].transform.localScale.z;

				for(int i=0; i<Rows;i++)
				{ 
					for(int j=0;j<Columns;j++)
					{ 
						float offset=(MaxX - MinX)/2.0f;
						if(i%2==0)
						{
							offsetx=(MaxX - MinX)*j;
							offsety=(MaxY - MinY)*i;
						}
						else
						{
							offsetx=((MaxX - MinX)*j)+offset;
							offsety=(MaxY - MinY)*i;
						}
						offsetz=Random.Range(0.0f,BrickOffset);
						GameObject OBJ = Instantiate (theNewObject, new Vector3 (offsetx, offsety, offsetz), Quaternion.identity) as GameObject;
						OBJ.transform.SetParent (WallHandle.transform);
					}
				}
			}
			else 
			{
				Debug.LogWarning ("You Must Select at least one Object with a mesh filter component");
			}
		}
		else 
		{
			Debug.LogWarning ("You Must Select at least one mesh Object");
		}
	}
}
