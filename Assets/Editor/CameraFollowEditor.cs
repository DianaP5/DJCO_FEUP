﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		CameraFollow cf = (CameraFollow)target;
		if (GUILayout.Button("Set min cam position")) {
			cf.SetMinCamPosition ();
		}
		if (GUILayout.Button("Set max cam position")) {
			cf.SetMaxCamPosition ();
		}
	}
}
