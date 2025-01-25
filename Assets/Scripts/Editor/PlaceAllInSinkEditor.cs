using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlaceAllInSink))]
public class PlaceAllInSinkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

            PlaceAllInSink t = (PlaceAllInSink)target;

        if (GUILayout.Button("Place on Sink"))
        {
            t.PlaceAll();
        }

    }
}
