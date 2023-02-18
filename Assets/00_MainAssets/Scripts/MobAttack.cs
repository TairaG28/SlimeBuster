using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//攻撃制御クラス
[RequireComponent(typeof(MobStatus))]

public class MobAttack : MonoBehaviour
{
    //攻撃後のクールダウン（秒）
    [SerializeField] private float attackCooldown = 0.5f;

    [SerializeField] private Collider attackCollider;

    [SerializeField] protected Animator animator;

    [SerializeField] protected FlagManager _flagManager;

    private MobStatus _status;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
    }


    //攻撃可能な状態であれば、攻撃を行う。
    public void AttackIfPossible()
    {
        //ステータスと衝突したオブジェクトで攻撃可否を判断する
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();

    }


    //攻撃対象が攻撃範囲に入った時に呼ばれる
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }


    //攻撃開始時に呼ばれる
    public virtual void OnAttackStart()
    {
        attackCollider.enabled = true;

        /*        if (swingSound != null)
                {
                    //武器を振る音の再生。pitch（再生速度）をランダムも変化させ、毎回少し違った音が出るようにしている
                    swingSound.pitch = Random.Range(0.7f, 1.3f);
                    swingSound.Play();

                }*/
    }

    //attackColliderが攻撃対処にHitしたときに呼ばれる
    public virtual void OnHitAttack(Collider collider)
    {

        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;

        targetMob.Damege(1);
    }

    //攻撃終了時に呼ばれる
    public virtual void OnAttackFinished()
    {
        /*Debug.Log("攻撃修了");*/
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();

    }

    public void PlayAttackSE()
    {
        if (!_flagManager.IsLose)
        {
            AudioManager.Instance.Play("ゴブリンの鳴き声1", "SE");

        }
        
    }

}
