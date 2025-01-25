using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

[CustomEditor(typeof(PlaceInSink))]
public class PlaceInSinkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlaceInSink t = (PlaceInSink)target;

        if (GUILayout.Button("Place on Sink"))
        {
            t.Place();
        }

        if (GUILayout.Button("Reset"))
        {
            t.ResetRotation();
        }

        // if changing values of prefabs.
        if (GUI.changed)
        {
            //EditorUtility.SetDirty((Wander)target);
        }
    }
}
