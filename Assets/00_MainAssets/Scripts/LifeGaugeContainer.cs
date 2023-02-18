using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get
        {
            return _instance;
        }
    }

    private static LifeGaugeContainer _instance;

    //���C�t�Q�[�W�\���Ώۂ�Mob�𔽉f���Ă���J����
    [SerializeField] private Camera mainCamera;
    //���C�t�Q�[�W��Prehab
    [SerializeField] private LifeGauge lifeGaugePrefab;

    private RectTransform rectTransform;
    //�A�N�e�B�u�ȃQ�[����ێ�����R���e�i
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap
        = new Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        //�V�[����ɂP�������݂����Ȃ��X�N���v�g�̂��߁A���̂悤�ȋ^���V���O���g�������藧��
        if (null != _instance) throw new Exception("LifeBarContainer instance already exists.");
        _instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    //���C�t�Q�[�W��ǉ�����
    public void Add(MobStatus status)
    {
        var lifeGauge = Instantiate(lifeGaugePrefab, transform);
        lifeGauge.InitialiZe(rectTransform, mainCamera, status);
        _statusLifeBarMap.Add(status, lifeGauge);
    }

    //���C�t�Q�[�W��j������
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        _statusLifeBarMap.Remove(status);
    }



}
