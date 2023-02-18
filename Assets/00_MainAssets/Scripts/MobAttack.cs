using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�U������N���X
[RequireComponent(typeof(MobStatus))]

public class MobAttack : MonoBehaviour
{
    //�U����̃N�[���_�E���i�b�j
    [SerializeField] private float attackCooldown = 0.5f;

    [SerializeField] private Collider attackCollider;

    [SerializeField] protected Animator animator;

    [SerializeField] protected FlagManager _flagManager;

    private MobStatus _status;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
    }


    //�U���\�ȏ�Ԃł���΁A�U�����s���B
    public void AttackIfPossible()
    {
        //�X�e�[�^�X�ƏՓ˂����I�u�W�F�N�g�ōU���ۂ𔻒f����
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();

    }


    //�U���Ώۂ��U���͈͂ɓ��������ɌĂ΂��
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }


    //�U���J�n���ɌĂ΂��
    public virtual void OnAttackStart()
    {
        attackCollider.enabled = true;

        /*        if (swingSound != null)
                {
                    //�����U�鉹�̍Đ��Bpitch�i�Đ����x�j�������_�����ω������A���񏭂�����������o��悤�ɂ��Ă���
                    swingSound.pitch = Random.Range(0.7f, 1.3f);
                    swingSound.Play();

                }*/
    }

    //attackCollider���U���Ώ���Hit�����Ƃ��ɌĂ΂��
    public virtual void OnHitAttack(Collider collider)
    {

        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;

        targetMob.Damege(1);
    }

    //�U���I�����ɌĂ΂��
    public virtual void OnAttackFinished()
    {
        /*Debug.Log("�U���C��");*/
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
            AudioManager.Instance.Play("�S�u�����̖���1", "SE");

        }
        
    }

}
