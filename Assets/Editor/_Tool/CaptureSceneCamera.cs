using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CaptureSceneCamera 
{
    [MenuItem("Example/capture")]
    /*[ContextMenu("capture")]*/
    private static void CaptureSceneCameraView()
    {
        // 保存するパス
        var filePath = string.Format("{0}/image.png", Application.dataPath);
        // シーンカメラのテクスチャを取得する
        var renderTextureRef = UnityEditor.SceneView.lastActiveSceneView.camera.activeTexture;
        // Texture2Dに書き込む
        Texture2D tex = new Texture2D(renderTextureRef.width, renderTextureRef.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTextureRef;
        tex.ReadPixels(new Rect(0, 0, renderTextureRef.width, renderTextureRef.height), 0, 0);
        tex.Apply();

        // PNGに変換
        byte[] bytes = tex.EncodeToPNG();
        // 保存する
        Debug.Log(filePath);
        File.WriteAllBytes(filePath, bytes);
    }
}
