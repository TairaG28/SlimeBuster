using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class�@PlayerAttack : MobAttack 
{

    public PlayerController playerController;
    //�U���J�n���ɌĂ΂��
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
        AudioManager.Instance.Play("�u�����ł��I�v", "SE");
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
        AudioManager.Instance.Play("�u�������āI�v", "SE");
        AudioManager.Instance.Play("swing", "SE");
        base.OnAttackStart();


    }


    //attackCollider���U���Ώ���Hit�����Ƃ��ɌĂ΂��
    public override void OnHitAttack(Collider collider)
    {

        /*Debug.Log("kougeki_OK01");*/
        
        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;

        /*Debug.Log("kougeki_OK");*/

        AudioManager.Instance.Play("�Ō�8", "SE");
        targetMob.Damege(1);

    }

    public override void OnAttackFinished()
    {
        base.OnAttackFinished();
        playerController.AddComboTime();
    }

    //HIT���̍U�������I��
    public void OnAttackInterruptioned()
    {
        base.OnAttackFinished();
        
    }

    private void PlayerAttackSE()
    {
        AudioManager.Instance.Play("�u�������I�v", "SE");
        AudioManager.Instance.Play("swing", "SE");
    }

 

}
