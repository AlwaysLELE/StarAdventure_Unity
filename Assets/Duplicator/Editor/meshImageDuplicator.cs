using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class meshImageDuplicator : EditorWindow {

	public Object theNewObject;
	Texture2D BlackAndWhiteImage;
	float offset = 1.0f;
	MeshFilter duplicateOn;
	Mesh ObjectMesh;
	Vector3[] originalVertices;
	Vector3 []normalVerts;
	Vector2[] uvs;
	private Vector3 newNormal;
	int density = 50;
	public enum DuplicationType 
	{ 
		Vertex = 0, 
		Pixel = 1
	}
	public DuplicationType DupType;
	string Information = "";
	[MenuItem ("Duplicator/Mesh Image Duplicator")]
	static void Init () 
	{
		meshImageDuplicator window = (meshImageDuplicator)EditorWindow.GetWindow (typeof (meshImageDuplicator));
		window.Show();
	}

	void OnGUI () 
	{
		GUILayout.Label ("Image Duplicator Options", EditorStyles.boldLabel);
		BlackAndWhiteImage = EditorGUILayout.ObjectField ("Image", BlackAndWhiteImage, typeof(Texture2D), true) as Texture2D;
		DupType =(DuplicationType)EditorGUILayout.EnumPopup ("Duplicate Per", DupType);
		duplicateOn = EditorGUILayout.ObjectField ("Dupicate on Mesh", duplicateOn, typeof(MeshFilter), true) as MeshFilter;
		offset = EditorGUILayout.FloatField("Offset From Surface", offset);
	
		if (DupType == DuplicationType.Pixel) {
			density = EditorGUILayout.IntField ("Samples Per Textre (odd)", density);
			GUILayout.Label (Information);
		}

		if (GUILayout.Button ("Duplicate"))
		{
			if (DupType == DuplicationType.Vertex) 
			{
				DuplicateImage ();
			}
			else
			{
				DuplicatePerPixel();
			}
		}
	}

	void OnInspectorUpdate() {
		if (density < 1) {
			density = 1;
		}
		if (density % 2 == 0) {
			density += 1;
		}
		Information = "" + density * density+" Samples From The Image";
		Repaint();
	}

	void DuplicateImage ()
	{
		if (Selection.activeGameObject != null) {	
			ObjectMesh = duplicateOn.sharedMesh;

			uvs = ObjectMesh.uv;
			originalVertices = ObjectMesh.vertices;
			normalVerts = ObjectMesh.normals;
			GameObject DuplicateSpecialParent = new GameObject ("DuplicateImageParent");
			DuplicateSpecialParent.transform.position = Vector3.zero;
			DuplicateSpecialParent.transform.rotation = Quaternion.identity;
			for (int i = 0; i < originalVertices.Length; i++) {
				int u = Mathf.FloorToInt (uvs [i].x * BlackAndWhiteImage.width);
				int v = Mathf.FloorToInt (uvs [i].y * BlackAndWhiteImage.height);
				float HeightValue = BlackAndWhiteImage.GetPixel (u, v).grayscale;

				if (HeightValue > 0) {
					int RItemSeletion = Random.Range (0, Selection.gameObjects.Length);
					theNewObject = Selection.gameObjects [RItemSeletion];
					GameObject OBJ = Instantiate (theNewObject) as GameObject;
					Vector3 worldPt = duplicateOn.transform.TransformPoint (originalVertices [i]);
					OBJ.transform.position = worldPt;
					newNormal = duplicateOn.transform.rotation * normalVerts [i];
					OBJ.transform.rotation = Quaternion.FromToRotation(Vector3.up,newNormal);
					OBJ.transform.Translate (0, offset, 0, Space.Self);
					OBJ.transform.parent = DuplicateSpecialParent.transform;
				}
			}
		}
		else 
		{
			Debug.LogWarning ("You Must Select at least one Game Object");
		}
	}

	void DuplicatePerPixel()
	{
		if (Selection.activeGameObject != null) {	
			GameObject DuplicateSpecialParent = new GameObject ("DuplicateImageParent");
			ObjectMesh = duplicateOn.sharedMesh;
			uvs = ObjectMesh.uv;
			for(int u = 0; u < density; u++)
			{		
				for (int v = 0; v < density; v++)
				{
					Vector2 point = new Vector2 ((float)u / (float)density, (float)v / (float)density);
					GetMappedPoints (ObjectMesh, point,DuplicateSpecialParent);
				}
			}
		}
	}

	public static Vector3 GetBarycentric (Vector2 v1,Vector2 v2,Vector2 v3,Vector2 p)
	{
		Vector3 B = new Vector3();
		B.x = ((v2.y - v3.y)*(p.x-v3.x) + (v3.x - v2.x)*(p.y - v3.y)) /
			((v2.y-v3.y)*(v1.x-v3.x) + (v3.x-v2.x)*(v1.y -v3.y));
		B.y = ((v3.y - v1.y)*(p.x-v3.x) + (v1.x - v3.x)*(p.y - v3.y)) /
			((v3.y-v1.y)*(v2.x-v3.x) + (v1.x-v3.x)*(v2.y -v3.y));
		B.z = 1 - B.x - B.y;
		return B;
	}

	public static bool InTriangle(Vector3 barycentric)
	{
		return (barycentric.x >= 0.0f) && (barycentric.x <= 1.0f)
			&& (barycentric.y >= 0.0f) && (barycentric.y <= 1.0f)
			&& (barycentric.z >= 0.0f);
	}

	void GetMappedPoints( Mesh aMesh, Vector2 aUVPos,GameObject parentObject)
	{
		Vector3[] verts = aMesh.vertices;
		Vector2[] uvs = aMesh.uv;
		int[] indices = aMesh.triangles;
		normalVerts = ObjectMesh.normals;
		parentObject.transform.position = Vector3.zero;
		parentObject.transform.rotation = Quaternion.identity;
		for(int i = 0; i < indices.Length; i += 3)
		{
			int i1 = indices[i];
			int i2 = indices[i+1];
			int i3 = indices[i+2];
			Vector3 bary = GetBarycentric(uvs[i1],uvs[i2],uvs[i3],aUVPos);
			if (InTriangle(bary))
			{
				Vector3 localP = bary.x * verts[i1] + bary.y * verts[i2] + bary.z * verts[i3];
				int u = Mathf.FloorToInt (aUVPos.x * BlackAndWhiteImage.width );
				int v = Mathf.FloorToInt (aUVPos.y * BlackAndWhiteImage.height);
				float HeightValue = BlackAndWhiteImage.GetPixel (u, v).grayscale;
				if (HeightValue > 0) {
					int RItemSeletion = Random.Range (0, Selection.gameObjects.Length);
					theNewObject = Selection.gameObjects [RItemSeletion];
					GameObject OBJ = Instantiate (theNewObject) as GameObject;
					Vector3 worldPt = duplicateOn.transform.TransformPoint (localP);
					OBJ.transform.position = worldPt;
					newNormal = duplicateOn.transform.rotation * ((normalVerts [i1] + normalVerts [i2] + normalVerts [i3]) / 3.0f);
					OBJ.transform.rotation = Quaternion.FromToRotation (Vector3.up, newNormal);
					OBJ.transform.Translate (0, offset, 0, Space.Self);
					OBJ.transform.parent = parentObject.transform;
				}
			}
		}

	}
}