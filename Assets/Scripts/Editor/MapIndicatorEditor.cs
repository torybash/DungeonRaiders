using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MapIndicator))]
//[CanEditMultipleObjects]
public class MapIndicatorEditor : Editor {

	


	public override void OnInspectorGUI() {
//		serializedObject.Update ();

		MapIndicator indicator = (MapIndicator)target;

		
		DrawDefaultInspector();

//		if (EditorGUILayout.
		if(GUILayout.Button("Update Paths"))
		{
			foreach (var item in indicator.lineObjs) GameObject.DestroyImmediate(item);
			indicator.lineObjs.Clear();

			foreach (MapIndicator other in indicator.indicatorList) {
				GameObject lineObj = Instantiate((GameObject)Resources.Load("MapPathPrefab"));

				lineObj.GetComponent<LineRenderer>().SetVertexCount(2);
				lineObj.GetComponent<LineRenderer>().SetPosition(0, indicator.transform.position);
				lineObj.GetComponent<LineRenderer>().SetPosition(1, other.transform.position);


				indicator.lineObjs.Add(lineObj);
			}

//			LineRenderer line = new LineRenderer();
//			line.SetVertexCount(2);
//			line.SetPosition(0, indicator.);
//			line.SetPosition(1, indicatorList[0].transform.position);
//			SphereCollider sc = gameObject.AddComponent("SphereCollider") as SphereCollider;

		}

//		serializedObject.ApplyModifiedProperties ();
	}
}
