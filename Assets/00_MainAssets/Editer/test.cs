using UnityEditor;
using UnityEngine;

public class Example
{
    [MenuItem("Example/Log Missing Script Count")]
    private static void LogMissingScriptCount()
    {

        var missingCount = 0;

        //for���Ŏ��s
        foreach (GameObject go in Selection.gameObjects)
        {
           /* Debug.Log(go.name);*/

            //�����~�b�V���O���P�ȏ゠��Ύ��s
            if(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go) > 0)
            {
                missingCount++;
            }
        }

        Debug.Log(Selection.gameObjects.Length);
        Debug.Log(missingCount + "������"); 
    }

    [MenuItem("Example/Remove Missing Scripts")]

    private static void RemoveMissingScripts()
    {
        var missingCount = 0;

        //for���Ŏ��s
        foreach (GameObject go in Selection.gameObjects)
        {
           /* Debug.Log(go.name);
*/
            //�����~�b�V���O���P�ȏ゠��΁A�폜
            if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go) > 0)
            {

                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);

                missingCount++;
            }
        }
        Debug.Log(Selection.gameObjects.Length);
        Debug.Log(missingCount + "���폜");

    }
}