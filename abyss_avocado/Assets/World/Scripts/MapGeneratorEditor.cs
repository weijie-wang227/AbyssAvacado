using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator), true)]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator generator = (MapGenerator)target;

        DrawDefaultInspector();

        // Button to generate map
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }
    }
}