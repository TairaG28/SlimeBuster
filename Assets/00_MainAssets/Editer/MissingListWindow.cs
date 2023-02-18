using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class MissingListWindow : EditorWindow
{
    private static string[] extensions = { ".prefab", ".mat", ".controller", ".cs", ".shader", ".mask", ".asset" };

    private static List<AssetParameterData> missingList = new List<AssetParameterData>();
    private Vector2 scrollPos;

    /// <summary>  
    /// Missing������A�Z�b�g���������Ă��̃��X�g��\������  
    /// </summary>  
    [MenuItem("Assets/MissingList")]
    private static void ShowMissingList()
    {
        // Missing������A�Z�b�g������  
        Search();

        // �E�B���h�E��\��  
        var window = GetWindow<MissingListWindow>();
        window.minSize = new Vector2(900, 300);
    }

    /// <summary>  
    /// Missing������A�Z�b�g������  
    /// </summary>  
    private static void Search()
    {
        // �S�ẴA�Z�b�g�̃t�@�C���p�X���擾  
        string[] allPaths = AssetDatabase.GetAllAssetPaths();
        int length = allPaths.Length;

        for (int i = 0; i < length; i++)
        {
            // �v���O���X�o�[��\��  
            EditorUtility.DisplayProgressBar("Search Missing", string.Format("{0}/{1}", i + 1, length), (float)i / length);

            // Missing��Ԃ̃v���p�e�B������  
            if (extensions.Contains(Path.GetExtension(allPaths[i])))
            {
                SearchMissing(allPaths[i]);
            }
        }

        // �v���O���X�o�[������  
        EditorUtility.ClearProgressBar();
    }

    /// <summary>  
    /// �w��A�Z�b�g��Missing�̃v���p�e�B������΁A�����missingList�ɒǉ�����  
    /// </summary>  
    /// <param name="path">Path.</param>  
    private static void SearchMissing(string path)
    {
        // �w��p�X�̃A�Z�b�g��S�Ď擾  
        IEnumerable<UnityEngine.Object> assets = AssetDatabase.LoadAllAssetsAtPath(path);

        // �e�A�Z�b�g�ɂ��āAMissing�̃v���p�e�B�����邩�`�F�b�N  
        foreach (UnityEngine.Object obj in assets)
        {
            if (obj == null)
            {
                continue;
            }
            if (obj.name == "Deprecated EditorExtensionImpl")
            {
                continue;
            }

            // SerializedObject��ʂ��ăA�Z�b�g�̃v���p�e�B���擾����  
            SerializedObject sobj = new SerializedObject(obj);
            SerializedProperty property = sobj.GetIterator();

            while (property.Next(true))
            {
                // �v���p�e�B�̎�ނ��I�u�W�F�N�g�i�A�Z�b�g�j�ւ̎Q�ƂŁA  
                // ���̎Q�Ƃ�null�Ȃ̂ɂ�������炸�A�Q�Ɛ�C���X�^���XID��0�łȂ����̂�Missing��ԁI  
                if (property.propertyType == SerializedPropertyType.ObjectReference &&
                    property.objectReferenceValue == null &&
                    property.objectReferenceInstanceIDValue != 0)
                {

                    // Missing��Ԃ̃v���p�e�B���X�g�ɒǉ�����  
                    missingList.Add(new AssetParameterData()
                    {
                        obj = obj,
                        path = path,
                        property = property
                    });
                }
            }
        }
    }

    /// <summary>  
    /// Missing�̃��X�g��\��  
    /// </summary>  
    private void OnGUI()
    {
        // �񌩏o��  
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Asset", GUILayout.Width(200));
        EditorGUILayout.LabelField("Property", GUILayout.Width(200));
        EditorGUILayout.LabelField("Path");
        EditorGUILayout.EndHorizontal();

        // ���X�g�\��  
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (AssetParameterData data in missingList)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(data.obj, data.obj.GetType(), true, GUILayout.Width(200));
            EditorGUILayout.TextField(data.property.name, GUILayout.Width(200));
            EditorGUILayout.TextField(data.path);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }
}