using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//敵の状態管理スクリプト

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyStatus : MobStatus
{

    [SerializeField] protected GameObject dieEffectPrefab;

    private NavMeshAgent _agent;
    
    
    protected override void Start()
    {
        base.Start();

        MakeGauge();

        _agent = GetComponent<NavMeshAgent>();

    }

    protected virtual void MakeGauge()
    {
        //ライフゲージの表示を開始する
        LifeGaugeContainer.Instance.Add(this);
    }

    // Update is called once per frame
    private void Update()
    {
        //NavMeshAgentのvelocityで移動速度のベクトルが取得できる
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);

    }

    protected override void OnDie()
    {
        base.OnDie();

        enemyDieAction();

    }

    protected virtual void DeleteGauge()
    {
        //ライフゲージの表示を終了する
        LifeGaugeContainer.Instance.Remove(this);
    }

    protected virtual void enemyDieAction()
    {
        DeleteGauge();
        StartCoroutine(DestroyCoroutine());
    }


    //倒された時の消滅コルーチン
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        AudioManager.Instance.Play("打撃7", "SE");
        Instantiate(dieEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
