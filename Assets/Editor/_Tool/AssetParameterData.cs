using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetParameterData
{
    public UnityEngine.Object obj { get; set; }            //!< �A�Z�b�g��Object����  
    public string path { get; set; }                    //!< �A�Z�b�g�̃p�X  
    public SerializedProperty property { get; set; }    //!< �v���p�e�B  
}