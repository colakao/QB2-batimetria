using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraNavigationMouse))]
public class CameraPositionEditor : Editor
{
	private int _cameraIndex;
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CameraNavigationMouse camScript = (CameraNavigationMouse)target;
		if (GUILayout.Button("Go to view"))
		{
			_cameraIndex = EditorGUILayout.IntField("Camera Index", camScript.index);
			camScript.SetCameraTransforms(_cameraIndex);
		}
	}
}
