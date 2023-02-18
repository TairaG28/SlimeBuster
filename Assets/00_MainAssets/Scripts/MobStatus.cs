using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Mob（動くオブジェクト、MovingObjectの略）の状態管理スクリプト
public abstract class MobStatus : MonoBehaviour
{

    [SerializeField] protected FlagManager _flagManager;

    //状態の管理
    protected enum StateEnum
    {
        //通常
        Nomal,
        //攻撃中
        Attack,
        //死亡
        Die,
        //デモ中
        Demo,
        //被弾中
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




    //移動可能かどうか
    public bool IsMovable => StateEnum.Nomal == _state;

    //攻撃可能かどうか
    public bool IsAttackable => StateEnum.Nomal == _state;

    public bool IsAttacking => StateEnum.Attack == _state;

    public bool IsHItting => StateEnum.Hit == _state;

    public bool IsDemoing => StateEnum.Demo == _state;

    //ライフ最大値を返す
    public float LifeMax => lifeMax;

    //ライフ値を返す
    public float Life => _life;

    //ライフ最大値
    [SerializeField] private float lifeMax = 10;
    protected Animator _animator;

    //Mob状態
    protected StateEnum _state = StateEnum.Nomal;

    //現在のライフ値（ＨＰ）
    private float _life;


    protected virtual void Start()
    {
        //初期状態はライフ満タン
        _life = lifeMax;
        _animator = GetComponentInChildren<Animator>();


    }


    //キャラクターが倒れたときの処理を記述する
    protected virtual void OnDie()
    {

    }

    //指定値のダメージを受ける
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
        //各キャラで指定
    }


    public virtual void DamegeActionEnd()
    {
        //各キャラで指定
    }


    //可能であれば攻撃中の状態に遷移する
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;
        /*Debug.Log("攻撃開始");*/
        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");

    }

    //可能であればNomalの状態に遷移する
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Nomal;
    }








}
