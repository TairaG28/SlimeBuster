using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Mob�i�����I�u�W�F�N�g�AMovingObject�̗��j�̏�ԊǗ��X�N���v�g
public abstract class MobStatus : MonoBehaviour
{

    [SerializeField] protected FlagManager _flagManager;

    //��Ԃ̊Ǘ�
    protected enum StateEnum
    {
        //�ʏ�
        Nomal,
        //�U����
        Attack,
        //���S
        Die,
        //�f����
        Demo,
        //��e��
        Hit
    }


    public string GetMyState()
    {
        string myState = "";

        switch (_state)
        {
            case StateEnum.Nomal:
                myState = "Nomal";
                break;

            case StateEnum.Attack:
                myState = "Attack";
                break;

            case StateEnum.Die:
                myState = "Die";
                break;

            case StateEnum.Demo:
                myState = "Demo";
                break;

            case StateEnum.Hit:
                myState = "Hit";
                break;
        }

        return myState;
    }




    //�ړ��\���ǂ���
    public bool IsMovable => StateEnum.Nomal == _state;

    //�U���\���ǂ���
    public bool IsAttackable => StateEnum.Nomal == _state;

    public bool IsAttacking => StateEnum.Attack == _state;

    public bool IsHItting => StateEnum.Hit == _state;

    public bool IsDemoing => StateEnum.Demo == _state;

    //���C�t�ő�l��Ԃ�
    public float LifeMax => lifeMax;

    //���C�t�l��Ԃ�
    public float Life => _life;

    //���C�t�ő�l
    [SerializeField] private float lifeMax = 10;
    protected Animator _animator;

    //Mob���
    protected StateEnum _state = StateEnum.Nomal;

    //���݂̃��C�t�l�i�g�o�j
    private float _life;


    protected virtual void Start()
    {
        //������Ԃ̓��C�t���^��
        _life = lifeMax;
        _animator = GetComponentInChildren<Animator>();


    }


    //�L�����N�^�[���|�ꂽ�Ƃ��̏������L�q����
    protected virtual void OnDie()
    {

    }

    //�w��l�̃_���[�W���󂯂�
    public void Damege(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;

        if (_life > 0)
        {
            DamegeAction();
            return;
        }

        _state = StateEnum.Die;   
        _animator.SetTrigger("Die");
        OnDie();
    }

    protected virtual void DamegeAction()
    {
        //�e�L�����Ŏw��
    }


    public virtual void DamegeActionEnd()
    {
        //�e�L�����Ŏw��
    }


    //�\�ł���΍U�����̏�ԂɑJ�ڂ���
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;
        /*Debug.Log("�U���J�n");*/
        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");

    }

    //�\�ł����Nomal�̏�ԂɑJ�ڂ���
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Nomal;
    }








}
