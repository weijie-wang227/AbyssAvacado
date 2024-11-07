#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldGenerator), true)]
public class WorldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WorldGenerator generator = (WorldGenerator)target;

        DrawDefaultInspector();

        // Button to generate map
        if (GUILayout.Button("Generate"))
        {
            generator.LoadChunk();
        }
    }
}
#endif