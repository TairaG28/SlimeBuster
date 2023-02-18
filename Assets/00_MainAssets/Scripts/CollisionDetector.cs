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


    //Is Trigger��ON�ő���Collider�Əd�Ȃ��Ă���Ƃ��́A���̃��\�b�h����ɃR�[�������
    private void OnTriggerStay(Collider other)
    {
        //onTriggerStay�Ŏw�肳�ꂽ���������s����
        onTriggerStay.Invoke(other);
    }


    //UnityEvent���p�������N���X��[Serializable]������t�^���邱�Ƃ�
    //Inspector�E�C���h�E��ŕ\���ł���悤�ɂȂ�
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}
