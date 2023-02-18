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

    //ライフゲージ表示対象のMobを反映しているカメラ
    [SerializeField] private Camera mainCamera;
    //ライフゲージのPrehab
    [SerializeField] private LifeGauge lifeGaugePrefab;

    private RectTransform rectTransform;
    //アクティブなゲームを保持するコンテナ
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap
        = new Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        //シーン上に１つしか存在させないスクリプトのため、このような疑似シングルトンが成り立つ
        if (null != _instance) throw new Exception("LifeBarContainer instance already exists.");
        _instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    //ライフゲージを追加する
    public void Add(MobStatus status)
    {
        var lifeGauge = Instantiate(lifeGaugePrefab, transform);
        lifeGauge.InitialiZe(rectTransform, mainCamera, status);
        _statusLifeBarMap.Add(status, lifeGauge);
    }

    //ライフゲージを破棄する
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        _statusLifeBarMap.Remove(status);
    }



}
