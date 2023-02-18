using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobLifeGauge : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private MobStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _status.Life/_status.LifeMax;

        if(_slider.value <= 0)
        {
            /*   Destroy(this.gameObject);*/
            gameObject.SetActive(false);
        }
    }
}
