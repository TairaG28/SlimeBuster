using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerEnter = new TriggerEvent();
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }


    //Is TriggerがONで他のColliderと重なっているときは、このメソッドが常にコールされる
    private void OnTriggerStay(Collider other)
    {
        //onTriggerStayで指定された処理を実行する
        onTriggerStay.Invoke(other);
    }


    //UnityEventを継承したクラスに[Serializable]属性を付与することで
    //Inspectorウインドウ上で表示できるようになる
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}
