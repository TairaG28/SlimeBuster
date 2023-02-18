using UnityEditor;
using UnityEngine;

public class Example
{
    [MenuItem("Example/Log Missing Script Count")]
    private static void LogMissingScriptCount()
    {

        var missingCount = 0;

        //for文で実行
        foreach (GameObject go in Selection.gameObjects)
        {
           /* Debug.Log(go.name);*/

            //もしミッシングが１つ以上あれば実行
            if(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go) > 0)
            {
                missingCount++;
            }
        }

        Debug.Log(Selection.gameObjects.Length);
        Debug.Log(missingCount + "件発見"); 
    }

    [MenuItem("Example/Remove Missing Scripts")]

    private static void RemoveMissingScripts()
    {
        var missingCount = 0;

        //for文で実行
        foreach (GameObject go in Selection.gameObjects)
        {
           /* Debug.Log(go.name);
*/
            //もしミッシングが１つ以上あれば、削除
            if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go) > 0)
            {

                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);

                missingCount++;
            }
        }
        Debug.Log(Selection.gameObjects.Length);
        Debug.Log(missingCount + "件削除");

    }
}