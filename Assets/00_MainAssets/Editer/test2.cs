using UnityEditor;
using UnityEngine;

public static class Example2
{
    [MenuItem("Tools/Hoge")]
    private static void Hoge()
    {
        var go = Selection.activeGameObject;
        var count = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);

        Debug.Log(count);
    }
}