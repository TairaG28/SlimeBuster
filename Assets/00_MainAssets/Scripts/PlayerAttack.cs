using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class　PlayerAttack : MobAttack 
{

    public PlayerController playerController;
    //攻撃開始時に呼ばれる
    public void OnAttackStart01()
    {
        PlayerAttackSE();
        base.OnAttackStart();

    }
    public void OnAttackStart02()
    {

        PlayerAttackSE();
        base.OnAttackStart();



    }

    public void OnAttackStart03()
    {
        /*Debug.Log("kougeki03");*/
        AudioManager.Instance.Play("「そこです！」", "SE");
        AudioManager.Instance.Play("swing", "SE");
        base.OnAttackStart();



    }

    public void OnAttackStart04()
    {
        /*Debug.Log("kougeki04");*/
        PlayerAttackSE();
        base.OnAttackStart();



    }

    public void OnAttackStart05()
    {
        /*Debug.Log("kougeki05");*/
        AudioManager.Instance.Play("「当たって！」", "SE");
        AudioManager.Instance.Play("swing", "SE");
        base.OnAttackStart();


    }


    //attackColliderが攻撃対処にHitしたときに呼ばれる
    public override void OnHitAttack(Collider collider)
    {

        /*Debug.Log("kougeki_OK01");*/
        
        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;

        /*Debug.Log("kougeki_OK");*/

        AudioManager.Instance.Play("打撃8", "SE");
        targetMob.Damege(1);

    }

    public override void OnAttackFinished()
    {
        base.OnAttackFinished();
        playerController.AddComboTime();
    }

    //HIT時の攻撃強制終了
    public void OnAttackInterruptioned()
    {
        base.OnAttackFinished();
        
    }

    private void PlayerAttackSE()
    {
        AudioManager.Instance.Play("「えいっ！」", "SE");
        AudioManager.Instance.Play("swing", "SE");
    }

 

}
