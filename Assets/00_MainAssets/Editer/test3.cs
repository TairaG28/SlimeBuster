
using UnityEngine;
using UnityEditor;
 
public class TestSelectedObject : Editor
{
    [MenuItem("MyTools/Test Selected Object")]
    static void Create()
    {
        Debug.Log(Selection.gameObjects.Length);

        foreach (GameObject go in Selection.gameObjects)
        {
            Debug.Log(go.name);
        }
    }
}