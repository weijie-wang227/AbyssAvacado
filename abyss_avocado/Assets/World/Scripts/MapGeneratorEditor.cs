using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChunkGenerator), true)]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChunkGenerator generator = (ChunkGenerator)target;

        DrawDefaultInspector();

        // Button to generate map
        if (GUILayout.Button("Generate"))
        {
            //generator.Generate();
        }
    }
}