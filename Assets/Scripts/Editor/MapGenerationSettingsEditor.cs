using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerationSettings))]
public class MapGenerationSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var settings = (MapGenerationSettings)target;
        var mapGameObj = FindFirstObjectByType<Map>();
        if (GUILayout.Button("Regenerate"))
        {
            Debug.Log(SceneView.currentDrawingSceneView.camera.transform.position);
            //mapGameObj.Regenerate(new Bounds();
        }
    }
}
