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
        // �ۑ�����p�X
        var filePath = string.Format("{0}/image.png", Application.dataPath);
        // �V�[���J�����̃e�N�X�`�����擾����
        var renderTextureRef = UnityEditor.SceneView.lastActiveSceneView.camera.activeTexture;
        // Texture2D�ɏ�������
        Texture2D tex = new Texture2D(renderTextureRef.width, renderTextureRef.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTextureRef;
        tex.ReadPixels(new Rect(0, 0, renderTextureRef.width, renderTextureRef.height), 0, 0);
        tex.Apply();

        // PNG�ɕϊ�
        byte[] bytes = tex.EncodeToPNG();
        // �ۑ�����
        Debug.Log(filePath);
        File.WriteAllBytes(filePath, bytes);
    }
}
