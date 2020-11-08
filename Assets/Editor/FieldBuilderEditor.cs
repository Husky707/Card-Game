using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldBuilder))]
public class FieldBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FieldBuilder builder = (FieldBuilder)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Rebuild"))
        {
            builder.Rebuild();
        }
        if(GUILayout.Button("Clear"))
        {
            builder.ClearField();
        }

        GUILayout.EndHorizontal();
    }

}
